namespace DLS.Models.DTO
{
    public class AddLectureDTO
    {
        public string Name { get; set; }
#nullable enable
        public long? CourseId { get; set; }
        public long? TeacherId { get; set; }
#nullable disable
    }
}