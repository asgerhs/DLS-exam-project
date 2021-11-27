using System;
namespace DLS.Models.DTO
{
    public class AddCourseDTO {
        public string Name { get; set; }
        #nullable enable
        public long? TeacherId { get; set; }
        public List<long>? StudentIds { get; set; }
        #nullable disable
    } 
}