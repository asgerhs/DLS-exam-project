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
            List<Student> students = new List<Student>();
            Course c;
            CourseObj cobj;

            using (var dbContext = new SchoolContext())
            {
                c = ProtoMapper<CourseObj, Course>.Map(course);
                foreach (long id in course.StudentIds)
                {
                    Student? student = dbContext.Students.Where(x => x.Id == id).SingleOrDefault();
                    Console.WriteLine("This is " + student.Name + " With ID: " + student.Id);
                    if (student != null)
                        students.Add(student);
                }
                c.Teacher = dbContext.Teachers.Where(x => x.Id == course.TeacherId).SingleOrDefault();
                c.Students = students;
                dbContext.Courses.Add(c);
                dbContext.SaveChanges();

                cobj = ProtoMapper<Course, CourseObj>.Map(c);
                c.Students.ForEach(student => cobj.StudentIds.Add(student.Id));
                cobj.TeacherId = c.Teacher.Id;
                

            }
            using (var dbContext = new SchoolContext())
            {
                foreach (var student in students)
                {
                    student.Courses.Update(student);
                    if (student.Courses == null)
                    {
                        Console.WriteLine("Trying to add student through courses where student.courses is null");
                        student.Courses = new List<Course>();
                        student.Courses.Add(dbContext.Courses.Where(x => x.Id == c.Id).SingleOrDefault());
                    }
                    else
                    {
                        foreach (var item in student.Courses)
                        {
                            Console.WriteLine("List from the student: " + item.Id + item.Name);
                        }
                        Console.WriteLine("Trying to add student through courses where student.courses is NOT null");
                        student.Courses.Add(dbContext.Courses.Where(x => x.Id == c.Id).SingleOrDefault());
                    }
                }
                dbContext.SaveChanges();
            }
            return Task.FromResult(cobj);
        }

        // public void AddStudentsToCourse(List<long> studentIds, ServerCallContext context)
        // {
        //     using (var dbContext = new SchoolContext())
        //     {
        //         foreach (var id in studentIds)
        //         {
        //             Student student = dbContext.Students.Where(x => x.Id == id).SingleOrDefault();
        //             if (student.Courses == null)
        //             {
        //                 student.Courses = new List<Course>();
        //                 student.Courses.Add(c);
        //             }
        //             else
        //                 student.Courses.Add(c);
        //             dbContext.SaveChanges();
        //         }
        //     }
        // }
    }
}