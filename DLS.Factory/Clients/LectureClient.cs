using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using DLS.Models.DTO;
using DLS.Models.Managers;
using DLS.Factory.Protos;
using System;
using System.Net;
using System.Net.NetworkInformation;
using AutoMapper;

namespace DLS.Factory
{
    public class LectureClient
    {
        public static async Task<LectureDTO> GetLectureByIdAsync(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://servicelecture:80");
            var client = new LectureProto.LectureProtoClient(channel);

            MapperConfiguration config = new MapperConfiguration(cfg =>
               {
                   cfg.CreateMap<LectureObj, LectureDTO>();
                   cfg.CreateMap<DateTime, Timestamp>();
                   cfg.CreateMap<Timestamp, DateTime>();
               });
            IMapper iMapper = config.CreateMapper();

            LectureObj reply = await client.GetLectureByIdAsync(new Int64Value() { Value = id });
            return iMapper.Map<LectureObj, LectureDTO>(reply);
        }

        public static async Task<List<LectureDTO>> GetLecturesAsync()
        {
            using var channel = GrpcChannel.ForAddress("http://servicelecture:80");
            var client = new LectureProto.LectureProtoClient(channel);

             MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LectureObj, LectureDTO>();
                    cfg.CreateMap<DateTime, Timestamp>();
                    cfg.CreateMap<Timestamp, DateTime>();
                });
            IMapper iMapper = config.CreateMapper();

            AllLecturesReply reply = await client.GetAllLecturesAsync(new Empty());
            List<LectureDTO> MappedList = new List<LectureDTO>();

            foreach (var lecture in reply.Lectures)
            {
                MappedList.Add(iMapper.Map<LectureObj, LectureDTO>(lecture));
            }

            return MappedList;
        }
        public static async Task<LectureDTO> AddLectureAsync(AddLectureDTO lecture)
        {
            using var channel = GrpcChannel.ForAddress("http://servicelecture:80");
            var client = new LectureProto.LectureProtoClient(channel);

            MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AddLectureDTO, LectureObj>();
                    cfg.CreateMap<LectureObj, LectureDTO>();
                    cfg.CreateMap<DateTime, Timestamp>();
                    cfg.CreateMap<Timestamp, DateTime>();
                });
            IMapper iMapper = config.CreateMapper();

            LectureObj s = await client.AddLectureAsync(iMapper.Map<AddLectureDTO, LectureObj>(lecture));
            s.Date = Timestamp.FromDateTime(DateTime.Today.ToUniversalTime());

            LectureDTO dto = iMapper.Map<LectureObj, LectureDTO>(s);
            dto.Date = s.Date.ToDateTime();
            return dto;
        }

        public static async Task<LectureDTO> GenerateCodeForLectureAsync(long lectureId)
        {
            using var channel = GrpcChannel.ForAddress("http://servicelecture:80");
            var client = new LectureProto.LectureProtoClient(channel);

            LectureObj reply = await client.GenerateCodeForLectureAsync(new Int64Value() { Value = lectureId });

            MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LectureObj, LectureDTO>();
                    cfg.CreateMap<DateTime, Timestamp>();
                    cfg.CreateMap<Timestamp, DateTime>();
                });
            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<LectureObj, LectureDTO>(reply);

        }

        public static async Task<string> RegisterToLecture(string lectureCode, string studentEmail)
        {
            using var channel = GrpcChannel.ForAddress("http://servicelecture:80");
            var client = new LectureProto.LectureProtoClient(channel);
            LectureCode lc = new LectureCode();
            lc.RegistrationCode = lectureCode;
            lc.StudentEmail = studentEmail;
            LectureCode response = await client.RegisterToLectureAsync(lc);

            return response.Response;
        }
    }
}