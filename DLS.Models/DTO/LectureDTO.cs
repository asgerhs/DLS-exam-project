namespace DLS.Models.DTO
{
    public class LectureDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
#nullable enable
        public CourseDTO? Course { get; set; }
        public string? RegistrationCode { get; set; }
        public TeacherDTO? Teacher { get; set; }
        public List<StudentDTO>? Students { get; set; }
        public long? CourseId { get; set; }
        public long? TeacherId { get; set; }
        public List<long>? StudentIds { get; set; }

#nullable disable
    }
}