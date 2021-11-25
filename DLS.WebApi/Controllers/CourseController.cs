using Microsoft.AspNetCore.Mvc;
using DLS.Factory;
using DLS.Models.DTO;

namespace DLS.WebApi.Controllers
{

  [ApiController]
  [Route("/courses")]
  public class CourseController : ControllerBase
  {
      private readonly ILogger<CourseController> _logger;
      public CourseController(ILogger<CourseController> logger)
      {
          _logger = logger;
      }

      [HttpGet("get/{id}")]
      public async Task<CourseDTO> GetCourse(long id)
      {
          return await CourseFactory.getCourseById(id);
      }

      [HttpGet("all")]
      public async Task<List<CourseDTO>> GetCourses()
      {
          return await CourseFactory.getCourses();
      }
      [HttpPost("add")]
      public async Task<CourseDTO> AddCourse(AddCourseDTO course)
      {
          return await CourseFactory.AddCourse(course);
      }

  }
}