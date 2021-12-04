using Google.Protobuf.WellKnownTypes;
using DLS.Models.Managers;
using Grpc.Net.Client;
using DLS.Factory.Protos;
using DLS.Models.Models;
using DLS.Models.DTO;
using System;

namespace DLS.Factory.Clients
{
    public class LoginClient
    {
        public static async Task<UserDTO> LoginAsync(UserLoginDTO user)
        {
            using var channel = GrpcChannel.ForAddress("http://servicelogin:80");
            var client = new LoginProto.LoginProtoClient(channel);

            UserObj u = await client.LoginAsync(ProtoMapper<UserLoginDTO, UserObj>.Map(user));
            return ProtoMapper<UserObj, UserDTO>.Map(u);
        }

        public static async Task<BoolValue> CreateUserAsync(AddUserDTO user)
        {
            using var channel = GrpcChannel.ForAddress("http://servicelogin:80");
            var client = new LoginProto.LoginProtoClient(channel);

            return await client.CreateUserAsync(ProtoMapper<AddUserDTO, CreateUserObj>.Map(user));
        }
    }
}