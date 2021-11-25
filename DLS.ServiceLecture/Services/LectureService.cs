using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using DLS.EF.DatabaseContexts;
using DLS.Models.Models;
using DLS.Models.Managers;
using DLS.Models.DTO;
using DLS.ServiceLecture.Protos;

namespace DLS.ServiceLecture
{
    public class LectureService : LectureProto.LectureProtoBase
    {
        private readonly ILogger<LectureService> _logger;
        public LectureService(ILogger<LectureService> logger)
        {
            _logger = logger;
        }

        public override Task<LectureObj> GetLectureById(Int64Value id, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Lecture? s = dbContext.Lectures.Where(x => x.Id == id.Value).SingleOrDefault();
                return Task.FromResult(ProtoMapper<Lecture?, LectureObj>.Map(s));
            }
        }
        public override Task<AllLecturesReply> GetAllLectures(Empty empty, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                List<Lecture> lectures = dbContext.Lectures.ToList();
                AllLecturesReply reply = new AllLecturesReply{};
                lectures.ForEach(s => reply.Lectures.Add(
                    ProtoMapper<Lecture, LectureObj>.Map(s)));
                return Task.FromResult(reply);
            }
        }
        public override Task<LectureObj> AddLecture(LectureObj lecture, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Lecture s = ProtoMapper<LectureObj, Lecture>.Map(lecture);
                dbContext.Lectures.Add(s);
                dbContext.SaveChanges();
                return Task.FromResult(ProtoMapper<Lecture, LectureObj>.Map(s));
            }
        }
    }
}