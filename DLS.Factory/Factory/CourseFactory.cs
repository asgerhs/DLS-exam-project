using DLS.Factory.Clients;
using DLS.Models.DTO;
using System;

namespace DLS.Factory
{
    public class CourseFactory
    {
        public static async Task<CourseDTO> getCourseById(long id)
        {
            CourseDTO dto = await CourseClient.GetCourseByIdAsync(id);
            List<StudentDTO> students = new List<StudentDTO>();
            foreach (var sid in dto.StudentIds)
            {
                students.Add(await StudentClient.GetStudentByIdAsync(sid));
            }
            dto.Students = students;
            dto.Teacher = await TeacherClient.GetTeacherByIdAsync(Convert.ToInt64(dto.TeacherId));
            return dto;
        }

        public static async Task<List<CourseDTO>> getCourses()
        {
            return await CourseClient.GetCoursesAsync();
        }

        public static async Task<CourseDTO> AddCourse(AddCourseDTO course)
        {
            CourseDTO dto = await CourseClient.AddCourseAsync(course);

            List<StudentDTO> students = new List<StudentDTO>();
            foreach (var sid in dto.StudentIds)
            {
                students.Add(await StudentClient.GetStudentByIdAsync(sid));
            }
            dto.Students = students;

            if (dto.TeacherId != null)
                dto.Teacher = await TeacherClient.GetTeacherByIdAsync(Convert.ToInt64(dto.TeacherId));

            return dto;
        }
    }
}