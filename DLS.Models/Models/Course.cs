namespace DLS.Models.Models
{
    public class Course
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        List<Student> Students { get; set; }
    }
}