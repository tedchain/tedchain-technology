// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Tedchain.Infrastructure;

namespace Tedchain.Validation.PermissionBased
{
    public class DenyAllValidatorBuilder : IComponentBuilder<PermissionBasedValidator>
    {
        private readonly PermissionBasedValidator validator;

        public DenyAllValidatorBuilder()
        {
            StaticPermissionLayout layout = new StaticPermissionLayout(new Acl[0]);
            this.validator = new PermissionBasedValidator(new[] { layout });
        }

        public string Name { get; } = "DenyAll";

        public PermissionBasedValidator Build(IServiceProvider serviceProvider)
        {
            return validator;
        }

        public Task Initialize(IServiceProvider serviceProvider, IConfigurationSection configuration) => Task.FromResult(0);
    }
}
