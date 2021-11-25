namespace DLS.Models.DTO
{
    public class AddTeacherDTO {
        public string Name { get; set; }
        public string Email { get; set; }
        #nullable enable
        public List<CourseDTO>? Courses { get; set; }
        #nullable disable
    }
}