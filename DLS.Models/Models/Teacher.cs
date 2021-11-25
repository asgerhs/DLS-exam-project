namespace DLS.Models.Models
{
    public class Teacher
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Course> Courses { get; set; }
    }
}