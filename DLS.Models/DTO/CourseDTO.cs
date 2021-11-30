namespace DLS.Models.DTO
{
    public class CourseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        #nullable enable
        public TeacherDTO? Teacher { get; set; }
        public List<StudentDTO>? Students { get; set; }
        public long? TeacherId { get; set; }
        public List<long>? StudentIds { get; set; }

        #nullable disable
    }
}