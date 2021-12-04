using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DLS.Models.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
#nullable enable
        public TeacherDTO? Teacher { get; set; }
        public StudentDTO? Student { get; set; }
#nullable disable
        public bool IsTeacher { get; set; }
        public int SchoolId { get; set; }
    }
}