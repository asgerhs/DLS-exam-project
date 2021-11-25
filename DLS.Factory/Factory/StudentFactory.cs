using DLS.Factory.Clients;
using DLS.Models.DTO;
using System;

namespace DLS.Factory
{

  public class StudentFactory
  {
    public static async Task<StudentDTO> getStudentById(long id)
    {
      return await StudentClient.GetStudentByIdAsync(id);
    }

    public static async Task<List<StudentDTO>> getStudents()
    {
      return await StudentClient.GetStudentsAsync();
    }

    public static async Task<StudentDTO> AddStudent(AddStudentDTO student)
    {
      return await StudentClient.AddStudentAsync(student);
    }
  }
}