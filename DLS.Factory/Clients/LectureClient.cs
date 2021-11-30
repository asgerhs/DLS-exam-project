using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using DLS.Models.DTO;
using DLS.Models.Managers;
using DLS.Factory.Protos;
using System;  
using System.Net;  
using System.Net.NetworkInformation;  

namespace DLS.Factory
{
    public class LectureClient
    {
        public static async Task<LectureDTO> GetLectureByIdAsync(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://servicelecture:80");
            var client = new LectureProto.LectureProtoClient(channel);

            LectureObj reply = await client.GetLectureByIdAsync(new Int64Value() { Value = id });
            return ProtoMapper<LectureObj, LectureDTO>.Map(reply);
        }

        public static async Task<List<LectureDTO>> GetLecturesAsync()
        {
            using var channel = GrpcChannel.ForAddress("http://servicelecture:80");
            var client = new LectureProto.LectureProtoClient(channel);

            AllLecturesReply reply = await client.GetAllLecturesAsync(new Empty());
            List<LectureDTO> MappedList = new List<LectureDTO>();

            foreach (var lecture in reply.Lectures)
            {
                MappedList.Add(ProtoMapper<LectureObj, LectureDTO>.Map(lecture));
            }
            
            return MappedList;
        }
        public static async Task<LectureDTO> AddLectureAsync(AddLectureDTO lecture)
        {
            using var channel = GrpcChannel.ForAddress("http://servicelecture:80");
            var client = new LectureProto.LectureProtoClient(channel);

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                Console.WriteLine(adapter.Description);
                Console.WriteLine("  DNS suffix .............................. : {0}",
                    properties.DnsSuffix);
                Console.WriteLine("  DNS enabled ............................. : {0}",
                    properties.IsDnsEnabled);
                Console.WriteLine("  Dynamically configured DNS .............. : {0}",
                    properties.IsDynamicDnsEnabled);
            }
            Console.WriteLine();

            LectureObj s = await client.AddLectureAsync(ProtoMapper<AddLectureDTO, LectureObj>.Map(lecture));
            
            return ProtoMapper<LectureObj, LectureDTO>.Map(s);
        }
    }
}