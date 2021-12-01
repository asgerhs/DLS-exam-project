using System;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using DLS.EF.DatabaseContexts;
using DLS.Models.Models;
using DLS.Models.Managers;
using DLS.Models.DTO;
using DLS.ServiceLecture.Protos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

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
                Lecture? l = dbContext.Lectures.Where(x => x.Id == id.Value)
                .Include(x => x.Course)
                .Include(x => x.Teacher)
                .Include(x => x.Students)                
                .SingleOrDefault();

                LectureObj lobj = ProtoMapper<Lecture?, LectureObj>.Map(l);
                lobj.CourseId = l.Course.Id;
                lobj.TeacherId = l.Teacher.Id;
                l.Students.ForEach(student => lobj.StudentIds.Add(student.Id));

                return Task.FromResult(lobj);
            }
        }
        public override Task<AllLecturesReply> GetAllLectures(Empty empty, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                List<Lecture> lectures = dbContext.Lectures.ToList();
                AllLecturesReply reply = new AllLecturesReply { };
                lectures.ForEach(l => reply.Lectures.Add(
                    ProtoMapper<Lecture, LectureObj>.Map(l)));
                return Task.FromResult(reply);
            }
        }
        public override Task<LectureObj> AddLecture(LectureObj lecture, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DateTime, Timestamp>();
                    cfg.CreateMap<Timestamp, DateTime>();
                    cfg.CreateMap<Lecture, LectureObj>();
                    cfg.CreateMap<LectureObj, Lecture>();
                });
                IMapper iMapper = config.CreateMapper();

                Lecture l = iMapper.Map<LectureObj, Lecture>(lecture);
                DateTime date = DateTime.Today;
                l.Date = date;
                l.Course = dbContext.Courses.Where(x => x.Id == lecture.CourseId).SingleOrDefault();
                l.Teacher = dbContext.Teachers.Where(x => x.Id == lecture.TeacherId).SingleOrDefault();

                dbContext.Lectures.Add(l);
                dbContext.SaveChanges();

                LectureObj lobj = iMapper.Map<Lecture, LectureObj>(l);

                lobj.Date = Timestamp.FromDateTime(l.Date.ToUniversalTime());
                lobj.CourseId = l.Course.Id;
                lobj.TeacherId = l.Teacher.Id;

                return Task.FromResult(lobj);
            }
        }
    }
}