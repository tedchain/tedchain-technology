// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Represents a builder class capable of instanciating an object of a given type.
    /// </summary>
    /// <typeparam name="T">The type of object that can be instanciated by the builder.</typeparam>
    public interface IComponentBuilder<out T>
    {
        /// <summary>
        /// Gets the name of the builder.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Initializes the builder.
        /// </summary>
        /// <param name="serviceProvider">The service provider for the current context.</param>
        /// <param name="configuration">The builder configuration.</param>
        /// <returns></returns>
        Task Initialize(IServiceProvider serviceProvider, IConfigurationSection configuration);

        /// <summary>
        /// Builds the target object.
        /// </summary>
        /// <param name="serviceProvider">The service provider for the current context.</param>
        /// <returns>The built object.</returns>
        T Build(IServiceProvider serviceProvider);
    }
}
