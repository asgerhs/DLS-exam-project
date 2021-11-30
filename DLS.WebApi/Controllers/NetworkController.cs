using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using DLS.Factory;

namespace DLS.WebApi.Controllers
{

  [ApiController]
  [Route("/network")]
  public class NetworkController : ControllerBase
  {
      private readonly ILogger<NetworkController> _logger;
      public NetworkController(ILogger<NetworkController> logger)
      {
          _logger = logger;
      }

      [HttpGet("check")]
      public async Task<bool> CheckNetwork()
      {
        var env = Environment.GetEnvironmentVariable("WEB_APP");
        var connectionLocalAddress = HttpContext.Connection.LocalIpAddress;
        var connectionLocalInterface = NetworkInterface.GetAllNetworkInterfaces()
            .Where(iface => iface.GetIPProperties().UnicastAddresses.Any(unicastInfo => unicastInfo.Address.Equals(connectionLocalAddress)))
            .SingleOrDefault();
        
        return await NetworkFactory.CheckNetwork(env);
        
      }


  }
}