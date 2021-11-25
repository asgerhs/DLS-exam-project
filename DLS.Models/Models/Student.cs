namespace DLS.Models.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public List<Course> Courses { get; set; }
        public List<Lecture> Lectures { get; set; }
    }
}