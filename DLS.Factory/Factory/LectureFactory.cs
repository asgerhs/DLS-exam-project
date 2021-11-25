using DLS.Factory.Clients;
using DLS.Models.DTO;
using System;

namespace DLS.Factory
{
    public class LectureFactory
    {
        public static async Task<LectureDTO> getLectureById(long id)
        {
            return await LectureClient.GetLectureByIdAsync(id);
        }

        public static async Task<List<LectureDTO>> getLectures()
        {
            return await LectureClient.GetLecturesAsync();
        }

        public static async Task<LectureDTO> AddLecture(AddLectureDTO lecture)
        {
            return await LectureClient.AddLectureAsync(lecture);
        }
    }
}