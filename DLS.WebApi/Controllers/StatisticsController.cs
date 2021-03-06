using Microsoft.AspNetCore.Mvc;
using DLS.Factory;
using DLS.Models.DTO;

namespace DLS.WebApi.Controllers
{

  [ApiController]
  [Route("/statistics")]
//   [DisableCors]
  public class StatisticsController : ControllerBase
  {
      private readonly ILogger<StatisticsController> _logger;
      public StatisticsController(ILogger<StatisticsController> logger)
      {
          _logger = logger;
      }

      [HttpGet("studentsAttendedLecture/{id}")]
      public async Task<int> AllStudentsAttendedLecture(long id)
      {
          return await StatisticsFactory.AllStudentsAttendedLecture(id);
      }

      [HttpGet("studentsNotAttendedLecture/{id}")]
      public async Task<int> AllStudentsNotAttendedLecture(long id)
      {
          return await StatisticsFactory.AllStudentsNotAttendedLecture(id);
      }

      [HttpGet("AvgStudentAttendanceCourse/{id}")]
      public async Task<double> AvgStudentAttendanceCourse(long id)
      {
          return await StatisticsFactory.AvgStudentAttendanceCourse(id);
      }

      [HttpGet("AvgStudentAttendanceAll")]
      public async Task<double> AvgStudentAttendanceAll()
      {
          return await StatisticsFactory.AvgStudentAttendanceAll();
      }

      [HttpGet("GetAllAttendance/{id}")]
      public async Task<int> GetAllAttendance(long id)
      {
          return await StatisticsFactory.GetAllAttendance(id);
      }

      [HttpGet("GetCourseAttendance/{studentId}/{courseId}")]
      public async Task<int> GetCourseAttendance(long studentId, long courseId)
      {
          return await StatisticsFactory.GetCourseAttendance(studentId, courseId);
      }

      [HttpGet("GetAvgCourseAttendance/{studentId}")]
      public async Task<double> GetAvgCourseAttendance(long studentId)
      {
          return await StatisticsFactory.GetAvgCourseAttendance(studentId);
      }

  }
}