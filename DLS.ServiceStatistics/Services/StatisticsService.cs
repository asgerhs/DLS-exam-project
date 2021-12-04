using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using DLS.ServiceStatistics.Protos;
using Google.Protobuf.WellKnownTypes;
using DLS.EF.DatabaseContexts;
using DLS.Models.Managers;
using DLS.Models.Models;

namespace DLS.ServiceStatistics {
    public class StatisticsService : StatisticsProto.StatisticsProtoBase
    {
        private readonly ILogger<StatisticsService> _logger;
        public StatisticsService(ILogger<StatisticsService> logger)
        {
            _logger = logger;
        }

        public override Task<LectureStatObj> AllStudentsAttendedLecture(Int64Value lectureId, ServerCallContext context)
        {
            using(var dbContext = new SchoolContext()) {
                Lecture? lec = dbContext.Lectures.Where(x => x.Id == lectureId.Value)
                    .Include(s => s.Students)
                    .SingleOrDefault();
                LectureStatObj lobj = ProtoMapper<Lecture?, LectureStatObj>.Map(lec);
                foreach (var student in lec.Students) lobj.StudentIds.Add(student.Id);
                return Task.FromResult(lobj);
            }
        }

        public override Task<LectureStatObj> AllStudentsNotAttendedLecture(Int64Value lectureId, ServerCallContext context)
        {
            using(var dbContext = new SchoolContext()) {
                Lecture? lec = dbContext.Lectures
                    .Where(x => x.Id == lectureId.Value)
                    .Include(c => c.Course)
                    .Include(s => s.Students)
                    .SingleOrDefault();

                Course? cor = dbContext.Courses
                    .Where(course => course.Id == lec.Course.Id)
                    .Include(s => s.Students)
                    .SingleOrDefault();
                
                IEnumerable<Student?> studs = cor.Students.Except(lec.Students); 
                LectureStatObj lobj = ProtoMapper<Lecture?, LectureStatObj>.Map(lec);
                foreach (var student in studs) lobj.StudentIds.Add(student.Id);
                return Task.FromResult(lobj);
            }
        }

        public override Task<CourseStatObj> AvgStudentAttendanceCourse(Int64Value courseId, ServerCallContext context)
        {
            using(var dbContext = new SchoolContext()) {
                Course? cor = dbContext.Courses
                    .Where(course => course.Id == courseId.Value)
                    .SingleOrDefault();

                List<Lecture> lec = dbContext.Lectures
                    .Where(x => x.Course.Id == courseId.Value)
                    .ToList();

                CourseStatObj cobj = ProtoMapper<Course?, CourseStatObj>.Map(cor);

                foreach (Lecture lecture in lec) {
                    if (lecture.Course.Id == courseId.Value)
                        cobj.LectureIds.Add(lecture.Id);
                }

                return Task.FromResult(cobj);
            }
        }

        public override Task<AllLecturesStatReply> AvgStudentAttendanceAll(Empty empty, ServerCallContext context)
        {
            using(var dbContext = new SchoolContext()) {
                List<Lecture> lec = dbContext.Lectures.ToList();

                AllLecturesStatReply reply = new AllLecturesStatReply{};
                List<LectureStatObj> lectures = new List<LectureStatObj>();
                
                foreach(var l in lec) {
                    var a = ProtoMapper<Lecture, LectureStatObj>.Map(l);
                    a.LectureId = l.Id;
                    reply.LectureIds.Add(a);
                }
                
                return Task.FromResult(reply);
            }
        }

        public override Task<LectureSelfObj> GetAllAttendance(Int64Value studentId, ServerCallContext context)
        {
            using(var dbContext = new SchoolContext()) {
                Student? student = dbContext.Students
                    .Where(x => x.Id == studentId.Value)
                    .Include(x => x.Lectures)
                    .SingleOrDefault();
                List<long> lectureIds = new List<long>();
                LectureSelfObj lobj = new LectureSelfObj{};
                foreach(var lecture in student.Lectures) lobj.LectureId.Add(lecture.Id);
                return Task.FromResult(lobj);
            }
        }

        public override Task<CourseStatObj> GetCourseAttendance(Int64Message msg, ServerCallContext context)
        {
            using(var dbContext = new SchoolContext()) {
                Student? student = dbContext.Students
                    .Where(x => x.Id == msg.StudentId)
                    .Include(x => x.Courses.Where(x => x.Id == msg.CourseId))
                    .Include(x => x.Lectures.Where(x => x.Course.Id == msg.CourseId))
                    .SingleOrDefault();

                CourseStatObj cobj = new CourseStatObj{CourseId = msg.CourseId};
                foreach(Lecture lecture in student.Lectures) cobj.LectureIds.Add(lecture.Id);
                return Task.FromResult(cobj);
            }
        }

        public override Task<CourseStatObjs> GetAvgCourseAttendance(Int64Value studentId, ServerCallContext context)
        {
            using(var dbContext = new SchoolContext()) {
                Student? student = dbContext.Students
                    .Where(x => x.Id == studentId.Value)
                    .Include(x => x.Courses)
                    .Include(x => x.Lectures)
                    .SingleOrDefault();

                CourseStatObjs cobjs = new CourseStatObjs{};

                student.Courses.ForEach(c => {
                    CourseStatObj cobj = new CourseStatObj{CourseId = c.Id};
                    foreach(Lecture lecture in student.Lectures.Where(l => l.Course.Id == c.Id))
                        cobj.LectureIds.Add(lecture.Id);
                    cobjs.Courses.Add(cobj);
                });

                // foreach(Course c in student.Courses) {
                //     CourseStatObj cobj = new CourseStatObj{CourseId = c.Id};
                //     foreach(Lecture l in student.Lectures) {
                //         cobj.LectureIds.Add(l.Id);
                //     }
                //     cobjs.Courses.Add(cobj);
                // }
                
                return Task.FromResult(cobjs);
            }
        }
    }

}

