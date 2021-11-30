using DLS.Factory.Clients;
using DLS.Models.DTO;
using System;

namespace DLS.Factory
{

    public class StudentFactory
    {
        public static async Task<StudentDTO> getStudentById(long id)
        {
            StudentDTO dto = await StudentClient.GetStudentByIdAsync(id);
            if (dto.CourseIds != null)
            {
                List<CourseDTO> courses = new List<CourseDTO>();
                foreach (var cid in dto.CourseIds)
                {
                    courses.Add(await CourseClient.GetCourseByIdAsync(cid));
                }
                dto.Courses = courses;
            }
            return dto;
        }

        public static async Task<List<StudentDTO>> getStudents()
        {
            return await StudentClient.GetStudentsAsync();
        }

        public static async Task<StudentDTO> AddStudent(AddStudentDTO student)
        {
            return await StudentClient.AddStudentAsync(student);
        }
    }
}