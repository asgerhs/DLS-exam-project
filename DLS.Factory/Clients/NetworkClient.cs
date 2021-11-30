using Grpc.Net.Client;
using DLS.Models.Managers;
using DLS.Factory.Protos;
using Google.Protobuf.WellKnownTypes;
using System;  
using System.Net.NetworkInformation;

namespace DLS.Factory.Clients
{
    public class NetworkClient
    {
        public static async Task<bool> CheckNetworkAsync(string id)
        {
            Console.WriteLine(id);
            using var channel = GrpcChannel.ForAddress("http://servicenetwork:80");
            var client = new NetworkProto.NetworkProtoClient(channel);
            
            NetworkRequest networkRequest = new NetworkRequest() {NetworkId = "wad", NetworkName = "Wow"};
            var reply = await client.CheckNetworkAsync(new Empty());
            return ProtoMapper<NetworkReply, bool>.Map(reply);
        }
    }
}