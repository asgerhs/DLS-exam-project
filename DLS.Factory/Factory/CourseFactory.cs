using DLS.Factory.Clients;
using DLS.Models.DTO;
using System;

namespace DLS.Factory
{
    public class CourseFactory
    {
        public static async Task<CourseDTO> getCourseById(long id)
        {
            return await CourseClient.GetCourseByIdAsync(id);
        }

        public static async Task<List<CourseDTO>> getCourses()
        {
            return await CourseClient.GetCoursesAsync();
        }

        public static async Task<CourseDTO> AddCourse(AddCourseDTO course)
        {
            return await CourseClient.AddCourseAsync(course);
        }
    }
}