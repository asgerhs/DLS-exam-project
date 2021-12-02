using System;
using Microsoft.EntityFrameworkCore;
using DLS.Models.Models;

namespace DLS.EF.DatabaseContexts
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=mssql;Database=Master;User Id=SA;Password=P@ssword123");
        }
    }
}
