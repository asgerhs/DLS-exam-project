using Microsoft.AspNetCore.Mvc;
using DLS.Factory;
using DLS.Models.DTO;

namespace DLS.WebApi.Controllers
{

  [ApiController]
  [Route("/teachers")]
  public class TeacherController : ControllerBase
  {
      private readonly ILogger<TeacherController> _logger;
      public TeacherController(ILogger<TeacherController> logger)
      {
          _logger = logger;
      }

      [HttpGet("get/{id}")]
      public async Task<TeacherDTO> GetTeacher(long id)
      {
          return await TeacherFactory.getTeacherById(id);
      }

      [HttpGet("all")]
      public async Task<List<TeacherDTO>> GetTeachers()
      {
          return await TeacherFactory.getTeachers();
      }
      [HttpPost("add")]
      public async Task<TeacherDTO> AddTeacher(AddTeacherDTO teacher)
      {
          return await TeacherFactory.AddTeacher(teacher);
      }

  }
}