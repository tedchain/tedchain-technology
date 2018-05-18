// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NBitcoin;
using Tedchain.Infrastructure;

namespace Tedchain.Anchoring.Blockchain
{
    public class BlockchainAnchorRecorderBuilder : IComponentBuilder<BlockchainAnchorRecorder>
    {
        private Uri apiUrl;
        private Key key;
        private Network network;
        private long fees;

        public string Name { get; } = "Blockchain";

        public BlockchainAnchorRecorder Build(IServiceProvider serviceProvider)
        {
            if (key != null)
            {
                serviceProvider.GetRequiredService<ILogger>().LogInformation(
                    $"Blockchain anchoring configured to publish at address: {key.PubKey.GetAddress(network).ToString()}");

                return new BlockchainAnchorRecorder(apiUrl, key, network, fees);
            }
            else
            {
                return null;
            }
        }

        public Task Initialize(IServiceProvider serviceProvider, IConfigurationSection configuration)
        {
            string anchorKey = configuration["key"];

            if (!string.IsNullOrEmpty(anchorKey))
            {
                this.key = Key.Parse(anchorKey);
                this.network = Network.GetNetworks()
                    .First(item => item.GetVersionBytes(Base58Type.PUBKEY_ADDRESS)[0] == byte.Parse(configuration["network_byte"]));

                this.apiUrl = new Uri(configuration["bitcoin_api_url"]);
                this.fees = long.Parse(configuration["fees"]);
            }

            return Task.FromResult(0);
        }
    }
}
