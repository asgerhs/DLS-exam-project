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

            if (dto.StudentIds != null || dto.StudentIds.Count > 0)
            {
                List<StudentDTO> students = new List<StudentDTO>();
                // Test Når muligt igen.
                // dto.StudentIds.ForEach(sid => students.Add(await StudentClient.GetStudentByIdAsync(sid)));
                foreach (var sid in dto.StudentIds)
                {
                    students.Add(await StudentClient.GetStudentByIdAsync(sid));
                }
                dto.Students = students;
            }

            if (dto.TeacherId != null)
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

            if (dto.StudentIds.Count > 0 || dto.StudentIds != null)
            {
                List<StudentDTO> students = new List<StudentDTO>();
                foreach (var sid in dto.StudentIds)
                {
                    students.Add(await StudentClient.GetStudentByIdAsync(sid));
                }
                dto.Students = students;
            }

            if (dto.TeacherId != null)
                dto.Teacher = await TeacherClient.GetTeacherByIdAsync(Convert.ToInt64(dto.TeacherId));

            return dto;
        }
    }
}