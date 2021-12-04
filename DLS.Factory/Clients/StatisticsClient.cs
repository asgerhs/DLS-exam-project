using Grpc.Net.Client;
using DLS.Models.Managers;
using DLS.Factory.Protos;
using DLS.Models.DTO;
using Google.Protobuf.WellKnownTypes;
using System;  
using System.Net.NetworkInformation;

namespace DLS.Factory.Clients
{
    public class StatisticsClient
    {
        public static async Task<int> AllStudentsAttendedLecture(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://servicestatistics:80");
            var client = new StatisticsProto.StatisticsProtoClient(channel);

            LectureStatObj reply = await client.AllStudentsAttendedLectureAsync(new Int64Value() { Value = id });
            return reply.StudentIds.Count();
        }

        public static async Task<int> AllStudentsNotAttendedLecture(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://servicestatistics:80");
            var client = new StatisticsProto.StatisticsProtoClient(channel);

            LectureStatObj reply = await client.AllStudentsNotAttendedLectureAsync(new Int64Value() { Value = id });
            return reply.StudentIds.Count();
        }

        public static async Task<double> AvgStudentAttendanceCourse(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://servicestatistics:80");
            var client = new StatisticsProto.StatisticsProtoClient(channel);

            CourseStatObj reply = await client.AvgStudentAttendanceCourseAsync(new Int64Value() { Value = id });
            
            double lectureCount = reply.LectureIds.Count();
            double allLecturesAttendance = 0.0;

            foreach(var a in reply.LectureIds) 
                allLecturesAttendance += await AllStudentsAttendedLecture(a);
            
            double avg = allLecturesAttendance/lectureCount;
            Console.WriteLine("Lecture count: " + lectureCount.ToString());
            Console.WriteLine("Lecture attendance: " + allLecturesAttendance.ToString());
            Console.WriteLine("Avg: " + avg.ToString());
            return avg;
        }

        public static async Task<double> AvgStudentAttendanceAll()
        {
            using var channel = GrpcChannel.ForAddress("http://servicestatistics:80");
            var client = new StatisticsProto.StatisticsProtoClient(channel);

            AllLecturesStatReply reply = await client.AvgStudentAttendanceAllAsync(new Empty());
            
            double allLecturesAttendance = 0.0;
            double lectureCount = reply.LectureIds.Count();
            foreach(var lecture in reply.LectureIds)
                allLecturesAttendance += await AllStudentsAttendedLecture(lecture.LectureId);
            
            double avg = allLecturesAttendance/lectureCount;
            Console.WriteLine("Lecture count: " + lectureCount.ToString());
            Console.WriteLine("Lecture attendance: " + allLecturesAttendance.ToString());
            Console.WriteLine("Avg: " + avg.ToString());
            return avg;
        }

        public static async Task<int> GetAllAttendance(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://servicestatistics:80");
            var client = new StatisticsProto.StatisticsProtoClient(channel);

            LectureSelfObj reply = await client.GetAllAttendanceAsync(new Int64Value() { Value = id });

            return reply.LectureId.Count();
        }

        public static async Task<int> GetCourseAttendance(long studentId, long courseId)
        {
            using var channel = GrpcChannel.ForAddress("http://servicestatistics:80");
            var client = new StatisticsProto.StatisticsProtoClient(channel);

            CourseStatObj reply = await client.GetCourseAttendanceAsync(new Int64Message() { CourseId = courseId, StudentId = studentId});

            return reply.LectureIds.Count();
        }

        public static async Task<double> GetAvgCourseAttendance(long studentId)
        {
            using var channel = GrpcChannel.ForAddress("http://servicestatistics:80");
            var client = new StatisticsProto.StatisticsProtoClient(channel);

            CourseStatObjs reply = await client.GetAvgCourseAttendanceAsync(new Int64Value() { Value = studentId });

            double lectureCount = 0.0;

            foreach(var some in reply.Courses) {
                lectureCount += some.LectureIds.Count();
            }
            double courseCount = reply.Courses.Count();

            Console.WriteLine("Lecture count: " + lectureCount.ToString());
            Console.WriteLine("Course count: " + courseCount.ToString());
            Console.WriteLine("Avg: " + (lectureCount/courseCount).ToString());

            return lectureCount/courseCount;
        }
    }
}