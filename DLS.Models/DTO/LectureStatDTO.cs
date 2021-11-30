namespace DLS.Models.DTO
{
    public class LectureStatDTO
    {
        public long Id { get; set; }
        List<StudentDTO> Students { get; set; }
    }
}