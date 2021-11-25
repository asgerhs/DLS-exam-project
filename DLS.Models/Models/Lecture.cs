namespace DLS.Models.Models
{
    public class Lecture
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Course Course { get; set; }
        public string RegistrationCode { get; set; }
        public Teacher Teacher { get; set; }
        List<Student> Students { get; set; }
    }
}