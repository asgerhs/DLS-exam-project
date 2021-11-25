namespace DLS.Models.DTO
{
    public class AddStudentDTO {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        #nullable enable
        public List<CourseDTO>? Courses { get; set; }
        public List<LectureDTO>? Lectures { get; set; }
        #nullable disable
    } 
}