using Google.Protobuf.WellKnownTypes;
using DLS.Factory.Clients;
using DLS.Models.DTO;

namespace DLS.Factory
{
  public class TeacherFactory
  {
    public static async Task<TeacherDTO> getTeacherById(long id)
    {
      return await TeacherClient.GetTeacherByIdAsync(id);
    }

    public static async Task<List<TeacherDTO>> getTeachers()
    {
      return await TeacherClient.GetTeachersAsync();
    }

    public static async Task<TeacherDTO> AddTeacher(AddTeacherDTO teacher)
    {
      return await TeacherClient.AddTeacherAsync(teacher);
    }
  }
}