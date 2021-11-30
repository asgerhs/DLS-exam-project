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

    }
}