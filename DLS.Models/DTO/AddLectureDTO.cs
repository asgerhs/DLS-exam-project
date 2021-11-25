namespace DLS.Models.DTO
{
    public class AddLectureDTO {
        public string Name { get; set; }
        #nullable enable
        public CourseDTO? Course { get; set; }
        public string? RegistrationCode { get; set; }
        public TeacherDTO? Teacher { get; set; }
        List<StudentDTO>? Students { get; set; }
        #nullable disable
    } 
}