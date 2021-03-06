using DLS.Factory.Clients;
using DLS.Models.DTO;
using System;

namespace DLS.Factory
{
    public class LectureFactory
    {
        public static async Task<LectureDTO> getLectureById(long id)
        {
            LectureDTO dto = await LectureClient.GetLectureByIdAsync(id);
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

            if (dto.CourseId != null)
                dto.Course = await CourseClient.GetCourseByIdAsync(Convert.ToInt64(dto.CourseId));
            return dto;
        }

        public static async Task<List<LectureDTO>> getLectures()
        {
            List<LectureDTO> dto = await LectureClient.GetLecturesAsync();
            foreach (LectureDTO lecture in dto)
            {
                if (lecture.CourseId != null)
                    lecture.Course = await CourseClient.GetCourseByIdAsync(Convert.ToInt64(lecture.CourseId));
            }

            return dto;
        }

        public static async Task<LectureDTO> AddLecture(AddLectureDTO lecture)
        {
            LectureDTO dto = await LectureClient.AddLectureAsync(lecture);

            dto.Date = dto.Date.Date;

            if (dto.TeacherId != null)
                dto.Teacher = await TeacherClient.GetTeacherByIdAsync(Convert.ToInt64(dto.TeacherId));

            if (dto.CourseId != null)
                dto.Course = await CourseClient.GetCourseByIdAsync(Convert.ToInt64(dto.CourseId));

            return dto;
        }

        public static async Task<String> GenerateCodeForLecture(long lectureId)
        {
            LectureDTO dto = await LectureClient.GenerateCodeForLectureAsync(lectureId);
            return dto.RegistrationCode;
        }

        public static async Task<String> RegisterToLecture(string lectureCode, string studentEmail)
        {
            return await LectureClient.RegisterToLectureAsync(lectureCode, studentEmail);
        }

        public static async Task<List<LectureDTO>> GetLecturesByCourse(long courseId)
        {
            List<LectureDTO> dto = await LectureClient.GetLecturesByCourseAsync(courseId);
            foreach (LectureDTO lecture in dto)
            {
                if (lecture.CourseId != null)
                    lecture.Course = await CourseClient.GetCourseByIdAsync(Convert.ToInt64(lecture.CourseId));
            }

            return dto;
        }

        public static void TimeOutLecture(string registrationCode)
        {
            LectureClient.TimeOutLecture(registrationCode);
        }
    }
}