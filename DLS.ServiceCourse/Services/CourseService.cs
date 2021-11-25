using DLS.ServiceCourse.Protos;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Grpc.Core;
using DLS.EF.DatabaseContexts;
using DLS.Models.Models;
using DLS.Models.Managers;
using DLS.Models.DTO;

namespace DLS.ServiceCourse
{
    public class CourseService : CourseProto.CourseProtoBase
    {
        private readonly ILogger<CourseService> _logger;
        public CourseService(ILogger<CourseService> logger)
        {
            _logger = logger;
        }

        public override Task<CourseObj> GetCourseById(Int64Value id, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Course? s = dbContext.Courses.Where(x => x.Id == id.Value).SingleOrDefault();
                return Task.FromResult(ProtoMapper<Course?, CourseObj>.Map(s));
            }
        }
        public override Task<AllCoursesReply> GetAllCourses(Empty empty, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                List<Course> courses = dbContext.Courses.ToList();
                AllCoursesReply reply = new AllCoursesReply{};
                courses.ForEach(s => reply.Courses.Add(
                    ProtoMapper<Course, CourseObj>.Map(s)));
                return Task.FromResult(reply);
            }
        }
        public override Task<CourseObj> AddCourse(CourseObj course, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Course s = ProtoMapper<CourseObj, Course>.Map(course);
                dbContext.Courses.Add(s);
                dbContext.SaveChanges();
                return Task.FromResult(ProtoMapper<Course, CourseObj>.Map(s));
            }
        }
    }
}