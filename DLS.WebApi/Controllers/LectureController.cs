using Microsoft.AspNetCore.Mvc;
using DLS.Factory;
using DLS.Models.DTO;

namespace DLS.WebApi.Controllers
{

    [ApiController]
    [Route("/lectures")]
    public class LectureController : ControllerBase
    {
        private readonly ILogger<LectureController> _logger;
        public LectureController(ILogger<LectureController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get/{id}")]
        public async Task<LectureDTO> GetLecture(long id)
        {
            return await LectureFactory.getLectureById(id);
        }

        [HttpGet("all")]
        public async Task<List<LectureDTO>> GetLectures()
        {
            return await LectureFactory.getLectures();
        }

        [HttpPost("add")]
        public async Task<LectureDTO> AddLecture(AddLectureDTO lecture)
        {
            return await LectureFactory.AddLecture(lecture);
        }

        [HttpPost("addRegistrationCode/{lectureId}")]
        public async Task<String> GenerateCodeForLecture(long lectureId)
        {
            return await LectureFactory.GenerateCodeForLecture(lectureId);
        }

        [HttpPost("registerToLecture/{lectureCode}/{studentEmail}")]
        public async Task<String> RegisterToLecture(string lectureCode, string studentEmail)
        {
            return await LectureFactory.RegisterToLecture(lectureCode, studentEmail);
        }

        [HttpGet("getLecturesByCourse/{courseId}")]
        public async Task<List<LectureDTO>> GetLecturesByCourse(long courseId)
        {
            return await LectureFactory.GetLecturesByCourse(courseId);
        }

        [HttpPost("timeout/{registrationCode}")]
        public void TimeOutLecture(string registrationCode){
            LectureFactory.TimeOutLecture(registrationCode);
        }

    }
}