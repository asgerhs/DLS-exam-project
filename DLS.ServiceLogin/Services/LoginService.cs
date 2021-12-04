using System;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using DLS.EF.DatabaseContexts;
using DLS.Models.Models;
using DLS.Models.Managers;
using DLS.Models.DTO;
using DLS.ServiceLogin.Protos;
using Microsoft.EntityFrameworkCore;

namespace DLS.ServiceLogin
{
    public class LoginService : LoginProto.LoginProtoBase
    {
        private readonly ILogger<LoginService> _logger;
        public LoginService(ILogger<LoginService> logger)
        {
            _logger = logger;
        }

        public override Task<UserObj> Login(UserObj user, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                User? u = dbContext.Users.Where(x => x.Username == user.Username && x.Password == user.Password)
                .Include(x => x.Teacher)
                .Include(x => x.Student)
                .SingleOrDefault();

                UserObj? uobj = ProtoMapper<User, UserObj>.Map(u);

                if (u != null && u.IsTeacher == false)
                    uobj.SchoolId = u.Student.Id;
                else
                    uobj.SchoolId = u.Teacher.Id;

                return Task.FromResult(uobj);
            }
        }

        public override Task<BoolValue> CreateUser(CreateUserObj user, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Student s = ProtoMapper<CreateUserObj, Student>.Map(user);
                s.Email = user.Username;
                User u = new User() { Username = user.Username, Password = user.Password, IsTeacher = false, Student = s};
                dbContext.Students.Add(s);
                dbContext.Users.Add(u);
                dbContext.SaveChanges();

                BoolValue boolValue = new BoolValue();
                boolValue.Value = true;
                return Task.FromResult(boolValue);
            }

        }

    }
}