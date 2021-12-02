using System.Linq;
using Google.Protobuf.WellKnownTypes;
using DLS.Factory.Clients;
using DLS.Models.DTO;

namespace DLS.Factory
{
    public class TeacherFactory
    {
        public static async Task<TeacherDTO> getTeacherById(long id)
        {
            TeacherDTO dto = await TeacherClient.GetTeacherByIdAsync(id);
            List<CourseDTO> courses = new List<CourseDTO>();
            Console.WriteLine("COURSE ID COUNT: " + dto.CourseIds.Count);
            if (dto.CourseIds.Count > 0 || dto.CourseIds != null)
            {
                foreach (var course in dto.CourseIds)
                {
                    courses.Add(await CourseClient.GetCourseByIdAsync(course));
                }
                dto.Courses = courses;
            }
            return dto;
        }

        public static async Task<List<TeacherDTO>> getTeachers()
        {
            return await TeacherClient.GetTeachersAsync();
        }

        public static async Task<TeacherDTO> AddTeacher(AddTeacherDTO teacher)
        {
            return await TeacherClient.AddTeacherAsync(teacher);
        }
    }
}