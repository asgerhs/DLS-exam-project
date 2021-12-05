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
            return Task.FromResult(new NetworkReply
            {
                Authorized = false
            });
        }
    }

}

