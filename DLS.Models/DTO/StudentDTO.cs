namespace DLS.Models.DTO
{
    public class StudentDTO {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        #nullable enable
        public List<CourseDTO>? Courses { get; set; }
        public List<LectureDTO>? Lectures { get; set; }
        public List<long>? CourseIds { get; set; }
        public List<long>? LectureIds { get; set; }
        #nullable disable
    }
}