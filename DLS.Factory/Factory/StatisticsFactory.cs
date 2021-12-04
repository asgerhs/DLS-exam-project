using DLS.Factory.Clients;
using DLS.Models.DTO;
using System;
using System.Net.NetworkInformation;

namespace DLS.Factory
{
    public class StatisticsFactory
    {
        public static async Task<int> AllStudentsAttendedLecture(long id)
        {
            return await StatisticsClient.AllStudentsAttendedLecture(id);
        }

        public static async Task<int> AllStudentsNotAttendedLecture(long id)
        {
            return await StatisticsClient.AllStudentsNotAttendedLecture(id);
        }

        public static async Task<double> AvgStudentAttendanceCourse(long id)
        {
            return await StatisticsClient.AvgStudentAttendanceCourse(id);
        }

        public static async Task<double> AvgStudentAttendanceAll()
        {
            return await StatisticsClient.AvgStudentAttendanceAll();
        }
        public static async Task<int> GetAllAttendance(long id)
        {
            return await StatisticsClient.GetAllAttendance(id);
        }
        public static async Task<int> GetCourseAttendance(long studentId, long courseId)
        {
            return await StatisticsClient.GetCourseAttendance(studentId, courseId);
        }
        public static async Task<double> GetAvgCourseAttendance(long studentId)
        {
            return await StatisticsClient.GetAvgCourseAttendance(studentId);
        }

    }
}