// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Tedchain.Infrastructure;

namespace Tedchain.Server.Models
{
    public class DependencyResolver<T>
        where T : class
    {
        private readonly IComponentBuilder<T> builder;
        private readonly Task initialize;

        public DependencyResolver(IServiceProvider serviceProvider, string basePath, IConfigurationSection configuration)
        {
            IList<Assembly> assemblies = LoadAllAssemblies(basePath);

            this.builder = FindBuilder(assemblies, configuration);

            if (this.builder != null)
                initialize = this.builder.Initialize(serviceProvider, configuration);
        }

        private IComponentBuilder<T> FindBuilder(IList<Assembly> assemblies, IConfigurationSection configuration)
        {
            return (from assembly in assemblies
                    from type in assembly.GetTypes()
                    where typeof(IComponentBuilder<T>).IsAssignableFrom(type)
                    let instance = (IComponentBuilder<T>)type.GetConstructor(Type.EmptyTypes).Invoke(new object[0])
                    where instance.Name.Equals(configuration["provider"], StringComparison.OrdinalIgnoreCase)
                    select instance)
                    .FirstOrDefault();
        }

        public static async Task<Func<IServiceProvider, T>> Create(IServiceProvider serviceProvider, string configurationPath)
        {
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IHostingEnvironment application = serviceProvider.GetRequiredService<IHostingEnvironment>();
            IConfigurationSection rootSection = configuration.GetSection(configurationPath);

            try
            {
                DependencyResolver<T> resolver = new DependencyResolver<T>(serviceProvider, new FileInfo(Assembly.GetEntryAssembly().Location).Directory.FullName, rootSection);

                if (resolver.builder == null)
                    serviceProvider.GetRequiredService<ILogger>().LogWarning($"Unable to find a provider for {typeof(T).FullName} from the '{configurationPath}' configuration section.");

                return await resolver.Build();
            }
            catch (Exception exception)
            {
                serviceProvider.GetRequiredService<ILogger>().LogError($"Error while creating {typeof(T).FullName} from the '{configurationPath}' configuration section:\n {exception}");
                throw;
            }
        }

        public async Task<Func<IServiceProvider, T>> Build()
        {
            if (builder == null)
            {
                return _ => null;
            }
            else
            {
                await initialize;
                return builder.Build;
            }
        }

        private static IList<Assembly> LoadAllAssemblies(string projectPath)
        {
            return DependencyContext.Default.RuntimeLibraries
                .Where(library => library.Name.StartsWith("Tedchain.", StringComparison.OrdinalIgnoreCase))
                .Select(library => Assembly.Load(new AssemblyName(library.Name)))
                .ToList();
        }
    }
}
