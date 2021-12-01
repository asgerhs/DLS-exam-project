using System;
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
                Course? s = dbContext.Courses.Where(x => x.Id == id.Value)
                    .Include(x => x.Students)
                    .Include(x => x.Teacher)
                    .SingleOrDefault();
                CourseObj cobj = ProtoMapper<Course?, CourseObj>.Map(s);
                foreach (var student in s.Students)
                {
                    cobj.StudentIds.Add(student.Id);
                }
                cobj.TeacherId = s.Teacher.Id;

                return Task.FromResult(cobj);
            }
        }
        public override Task<AllCoursesReply> GetAllCourses(Empty empty, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                List<Course> courses = dbContext.Courses.ToList();
                AllCoursesReply reply = new AllCoursesReply { };
                courses.ForEach(s => reply.Courses.Add(
                    ProtoMapper<Course, CourseObj>.Map(s)));
                return Task.FromResult(reply);
            }
        }
        public override Task<CourseObj> AddCourse(CourseObj course, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Course c = ProtoMapper<CourseObj, Course>.Map(course);
                List<Student> students = new List<Student>();
                foreach (long id in course.StudentIds)
                {
                    Student? student = dbContext.Students.Where(x => x.Id == id).SingleOrDefault();
                    if (student != null)
                        students.Add(student);
                }
                c.Teacher = dbContext.Teachers.Where(x => x.Id == course.TeacherId).SingleOrDefault();
                c.Students = students;
                dbContext.Courses.Add(c);
                dbContext.SaveChanges();
                CourseObj cobj = ProtoMapper<Course, CourseObj>.Map(c);
                c.Students.ForEach(student => cobj.StudentIds.Add(student.Id));
                cobj.TeacherId = c.Teacher.Id;

                return Task.FromResult(cobj);

            }

        }
    }
}