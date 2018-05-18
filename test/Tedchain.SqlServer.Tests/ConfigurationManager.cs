// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using Microsoft.Extensions.Configuration;

namespace Tedchain.SqlServer.Tests
{
    public class ConfigurationManager
    {
        public static string GetSetting(string key)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("tests.json").Build();

            return configuration[key];
        }
    }
}
