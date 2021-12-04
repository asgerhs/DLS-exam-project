using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using DLS.Factory.Clients;
using DLS.Models.Models;
using DLS.Models.DTO;

namespace DLS.Factory.Factory
{
    public class UserFactory
    {
        public static async Task<UserDTO> Login(UserLoginDTO user)
        {
            UserDTO dto = await LoginClient.LoginAsync(user);

            if(dto.IsTeacher == false)
                dto.Student = await StudentClient.GetStudentByIdAsync(dto.SchoolId);
            else
                dto.Teacher = await TeacherClient.GetTeacherByIdAsync(dto.SchoolId);
            
            return dto;
        }

        public static async Task<BoolValue> CreateUser(AddUserDTO user)
        {
            return await LoginClient.CreateUserAsync(user);
        }
    }
}