namespace DLS.Models.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsTeacher { get; set; }
#nullable enable
        public Teacher? Teacher { get; set; }
        public Student? Student { get; set; }
#nullable disable

    }
}