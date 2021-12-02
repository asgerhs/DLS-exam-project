using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DLS.ServiceLogin.Services
{
    public class LoginService : LoginProto.LoginProtoBase
    {
        private readonly ILogger<LoginService> _logger;
        public LoginService(ILogger<LoginService> logger)
        {
            _logger = logger;
        }
        
    }
}