namespace DLS.Models.DTO
{
    public class TeacherDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
#nullable enable
        public List<CourseDTO>? Courses { get; set; }
        public List<long>? CourseIds { get; set; }
        public string? Token { get; set; }
#nullable disable
    }
}