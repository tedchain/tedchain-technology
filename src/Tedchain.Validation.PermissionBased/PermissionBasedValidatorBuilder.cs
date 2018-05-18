// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tedchain.Infrastructure;

namespace Tedchain.Validation.PermissionBased
{
    public class PermissionBasedValidatorBuilder : IComponentBuilder<PermissionBasedValidator>
    {
        private List<IPermissionsProvider> staticPermissionProviders = new List<IPermissionsProvider>();
        private KeyEncoder keyEncoder;

        public string Name { get; } = "PermissionBased";

        public PermissionBasedValidator Build(IServiceProvider serviceProvider)
        {
            List<IPermissionsProvider> providers = new List<IPermissionsProvider>(staticPermissionProviders);
            providers.Add(new DynamicPermissionLayout(serviceProvider.GetRequiredService<IStorageEngine>(), keyEncoder));

            return new PermissionBasedValidator(providers);
        }

        public Task Initialize(IServiceProvider serviceProvider, IConfigurationSection configuration)
        {
            byte versionByte = byte.Parse(configuration["version_byte"]);
            this.keyEncoder = new KeyEncoder(versionByte);

            P2pkhSubject[] adminAddresses = configuration
                .GetSection("admin_addresses")
                .GetChildren()
                .Select(key => key.Value)
                .Select(address => new P2pkhSubject(new[] { address }, 1, keyEncoder))
                .ToArray();

            List<Acl> pathPermissions = new List<Acl>()
            {
                // Admins have full rights
                new Acl(adminAddresses, LedgerPath.Parse("/"), true, StringPattern.MatchAll, PermissionSet.AllowAll)
            };
            
            if (bool.Parse(configuration["allow_third_party_assets"]))
                this.staticPermissionProviders.Add(new P2pkhIssuanceImplicitLayout(keyEncoder));

            if (bool.Parse(configuration["allow_p2pkh_accounts"]))
                this.staticPermissionProviders.Add(new P2pkhImplicitLayout(keyEncoder));

            this.staticPermissionProviders.Add(new StaticPermissionLayout(pathPermissions));
            
            return Task.FromResult(0);
        }
    }
}
