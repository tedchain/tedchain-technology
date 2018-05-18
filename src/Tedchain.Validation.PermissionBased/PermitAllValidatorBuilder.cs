// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Tedchain.Infrastructure;

namespace Tedchain.Validation.PermissionBased
{
    public class PermitAllValidatorBuilder : IComponentBuilder<PermissionBasedValidator>
    {
        private readonly PermissionBasedValidator validator;

        public PermitAllValidatorBuilder()
        {
            P2pkhSubject subject = new P2pkhSubject(new string[0], 0, new KeyEncoder(0));
            List<Acl> permissions = new List<Acl>()
            {
                new Acl(new IPermissionSubject[] { subject }, LedgerPath.Parse("/"), true, StringPattern.MatchAll, PermissionSet.AllowAll)
            };

            StaticPermissionLayout layout = new StaticPermissionLayout(permissions);

            this.validator = new PermissionBasedValidator(new[] { layout });
        }

        public string Name { get; } = "PermitAll";

        public PermissionBasedValidator Build(IServiceProvider serviceProvider)
        {
            return validator;
        }

        public Task Initialize(IServiceProvider serviceProvider, IConfigurationSection configuration) => Task.FromResult(0);
    }
}
