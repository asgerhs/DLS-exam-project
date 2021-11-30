using Grpc.Core;
using DLS.ServiceNetwork.Protos;
using Google.Protobuf.WellKnownTypes;

namespace DLS.ServiceNetwork {
    public class NetworkService : NetworkProto.NetworkProtoBase
    {
        private readonly ILogger<NetworkService> _logger;
        public NetworkService(ILogger<NetworkService> logger)
        {
            _logger = logger;
        }

        public override Task<NetworkReply> CheckNetwork(Empty empty, ServerCallContext context)
        {
            List<String> authorizedNetworkIds2 = new List<string>();
            authorizedNetworkIds2.Add("3243d175-0075-4037-a23e-9f65734f0347");
            return Task.FromResult(new NetworkReply
            {
                Authorized = false
            });
        }
    }

}

