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
            List<CourseDTO> courses = new List<CourseDTO>();
            List<LectureDTO> lectures = new List<LectureDTO>();
            if (dto.CourseIds != null)
            {
                foreach (var cid in dto.CourseIds)
                {
                    courses.Add(await CourseClient.GetCourseByIdAsync(cid));
                }
                foreach (var tid in courses)
                {
                    tid.Teacher = await TeacherClient.GetTeacherByIdAsync(Convert.ToInt64(tid.TeacherId));
                }
                dto.Courses = courses;
            }
            if(dto.LectureIds != null)
            {
                foreach (var lid in dto.LectureIds)
                {
                    lectures.Add(await LectureClient.GetLectureByIdAsync(lid));
                }
                foreach (var item in lectures)
                {
                    item.Teacher = await TeacherClient.GetTeacherByIdAsync(Convert.ToInt64(item.TeacherId));
                    item.Course = await CourseClient.GetCourseByIdAsync(Convert.ToInt64(item.CourseId));
                }
                
                dto.Lectures = lectures;
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