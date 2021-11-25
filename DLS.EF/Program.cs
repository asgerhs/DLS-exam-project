using Microsoft.EntityFrameworkCore;
using DLS.Models.Models;
using DLS.EF.DatabaseContexts;

namespace DLS.EF
{
    public class Program
    {
        static List<Student> students = new List<Student>(){
            new Student() { Name = "Andreas", Email = "email", Age = 20, Gender = "Mand"},
            new Student() { Name = "Martin", Email = "email", Age = 20, Gender = "Mand"},
            new Student() { Name = "William", Email = "email", Age = 20, Gender = "Mand"},
            new Student() { Name = "Asger", Email = "email", Age = 20, Gender = "Mand"}
       
        };
        static List<Teacher> teachers = new List<Teacher>(){
            new Teacher() { Name = "Todorka", Email="tdi@mail.com"}
        };

        static List<Course> courses = new List<Course>(){
            new Course() { Name = "DLS"}
        };

        public static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                context.Database.Migrate();
                foreach (Student s in students)
                    context.Students.Add(s);
                foreach (Teacher t in teachers)
                    context.Teachers.Add(t);
                foreach (Course c in courses)
                    context.Courses.Add(c);
                context.SaveChanges();
            }
        }
    }
}