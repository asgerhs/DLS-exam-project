using Microsoft.AspNetCore.Mvc;
using DLS.Factory;
using DLS.Models.DTO;

namespace DLS.WebApi.Controllers
{

  [ApiController]
  [Route("/students")]
  public class StudentController : ControllerBase
  {
      private readonly ILogger<StudentController> _logger;
      public StudentController(ILogger<StudentController> logger)
      {
          _logger = logger;
      }

      [HttpGet("get/{id}")]
      public async Task<StudentDTO> GetStudent(long id)
      {
          return await StudentFactory.getStudentById(id);
      }

      [HttpGet("all")]
      public async Task<List<StudentDTO>> GetStudents()
      {
          return await StudentFactory.getStudents();
      }
      [HttpPost("add")]
      public async Task<StudentDTO> AddStudent(AddStudentDTO student)
      {
          return await StudentFactory.AddStudent(student);
      }

  }
}