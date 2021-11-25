namespace DLS.Models.DTO
{
    public class AddCourseDTO {
        public string Name { get; set; }
        #nullable enable
        public TeacherDTO? Teacher { get; set; }
        List<StudentDTO>? Students { get; set; }
        #nullable disable
    } 
}