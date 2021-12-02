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

    }
}