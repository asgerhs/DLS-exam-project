namespace DLS.Models.DTO
{
    public class CourseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        #nullable enable
        public TeacherDTO? Teacher { get; set; }
        public List<StudentDTO>? Students { get; set; }
        #nullable disable
    }
}