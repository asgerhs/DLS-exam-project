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
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DateTime, Timestamp>();
                    cfg.CreateMap<Timestamp, DateTime>();
                    cfg.CreateMap<Lecture, LectureObj>();
                    cfg.CreateMap<LectureObj, Lecture>();
                });
                IMapper iMapper = config.CreateMapper();

                Lecture? l = dbContext.Lectures.Where(x => x.Id == id.Value)
                .Include(x => x.Course)
                .Include(x => x.Teacher)
                .Include(x => x.Students)
                .SingleOrDefault();

                LectureObj lobj = iMapper.Map<Lecture?, LectureObj>(l);
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
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DateTime, Timestamp>();
                    cfg.CreateMap<Timestamp, DateTime>();
                    cfg.CreateMap<Lecture, LectureObj>();
                    cfg.CreateMap<LectureObj, Lecture>();
                });
                IMapper iMapper = config.CreateMapper();

                List<Lecture> lectures = dbContext.Lectures.Include(x => x.Course).ToList();
                AllLecturesReply reply = new AllLecturesReply { };
                foreach (var lecture in lectures)
                {
                    LectureObj lobj = iMapper.Map<Lecture, LectureObj>(lecture);
                    lobj.CourseId = lecture.Course.Id;
                    reply.Lectures.Add(lobj);
                }
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

        public override Task<LectureObj> GenerateCodeForLecture(Int64Value id, ServerCallContext context)
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

                Lecture? l = dbContext.Lectures.Where(x => x.Id == id.Value).SingleOrDefault();
                Random random = new Random();
                string code = id.Value + ":" + random.Next(1, 11) + random.Next(1, 11) + random.Next(1, 11) + random.Next(1, 11);
                l.RegistrationCode = code;

                dbContext.SaveChanges();

                LectureObj lobj = iMapper.Map<Lecture?, LectureObj>(l);
                lobj.RegistrationCode = code;

                return Task.FromResult(lobj);
            }
        }

        public override Task<LectureCode> RegisterToLecture(LectureCode lc, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Lecture l;
                Student s;
                string[] lectureId = lc.RegistrationCode.Split(":");
                Console.WriteLine("LectureId: " + lectureId[0]);
                Console.WriteLine("Email: " + lc.StudentEmail);
                try
                {
                    l = dbContext.Lectures.Where(x => (x.Id == Convert.ToInt64(lectureId[0])) && (x.RegistrationCode == lc.RegistrationCode)).SingleOrDefault();
                }
                catch
                {
                    lc.Response = "Invalid lecture code";
                    return Task.FromResult(lc);
                }

                try
                {
                    s = dbContext.Students.Where(x => x.Email == lc.StudentEmail).SingleOrDefault();
                }
                catch
                {
                    lc.Response = "Invalid student email";
                    return Task.FromResult(lc);
                }

                Console.WriteLine("Lecture: " + l);
                Console.WriteLine("Student: " + s);
                lc.Response = "Success.";
                if (l == null)
                {
                    lc.Response = "Invalid lecture code";
                    return Task.FromResult(lc);
                }

                if (l.Students == null)
                {
                    List<Student> students = new List<Student>();
                    l.Students = students;
                    l.Students.Add(s);
                }
                else
                    l.Students.Add(s);

                dbContext.SaveChanges();


                return Task.FromResult(lc);
            }
        }

        public override Task<AllLecturesReply> GetLecturesByCourse(Int64Value courseId, ServerCallContext context)
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

                List<Lecture> lectures = dbContext.Lectures.Where(x => x.Course.Id == courseId.Value)
                .Include(x => x.Course)
                .ToList();

                AllLecturesReply reply = new AllLecturesReply { };
                foreach (var lecture in lectures)
                {
                    LectureObj lobj = iMapper.Map<Lecture, LectureObj>(lecture);
                    lobj.CourseId = lecture.Course.Id;
                    reply.Lectures.Add(lobj);
                }
                return Task.FromResult(reply);
            }
        }
    }
}