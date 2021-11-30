using DLS.Factory.Clients;
using DLS.Models.DTO;
using System;
using System.Net.NetworkInformation;

namespace DLS.Factory
{
    public class NetworkFactory
    {
        public static async Task<bool> CheckNetwork(string id)
        {
            return await NetworkClient.CheckNetworkAsync(id);
        }

    }
}