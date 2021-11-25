namespace DLS.Models.DTO
{
    public class LectureDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CourseDTO Course { get; set; }
        public string RegistrationCode { get; set; }
        public TeacherDTO Teacher { get; set; }
        List<StudentDTO> Students { get; set; }
    }
}