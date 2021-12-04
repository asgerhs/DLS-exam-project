using Microsoft.AspNetCore.Mvc;
using DLS.Factory.Factory;
using DLS.Models.DTO;
using DLS.Models.Models;
using Google.Protobuf.WellKnownTypes;

namespace DLS.WebApi.Controllers
{
    [ApiController]
    [Route("/user")]
    public class LoginController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<UserDTO> Login(UserLoginDTO user)
        {
            return await UserFactory.Login(user);
        }

        [HttpPost("CreateUser")]
        public async Task<BoolValue> CreateUser(AddUserDTO user)
        {
            return await UserFactory.CreateUser(user);
        }
    }
}