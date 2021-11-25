using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using DLS.Models.DTO;
using DLS.Models.Managers;
using DLS.Factory.Protos;

namespace DLS.Factory.Clients
{
    public class CourseClient
    {
        public static async Task<CourseDTO> GetCourseByIdAsync(long id)
        {
            using var channel = GrpcChannel.ForAddress("http://servicecourse:80");
            var client = new CourseProto.CourseProtoClient(channel);

            CourseObj reply = await client.GetCourseByIdAsync(new Int64Value() { Value = id });
            return ProtoMapper<CourseObj, CourseDTO>.Map(reply);
        }

        public static async Task<List<CourseDTO>> GetCoursesAsync()
        {
            using var channel = GrpcChannel.ForAddress("http://servicecourse:80");
            var client = new CourseProto.CourseProtoClient(channel);

            AllCoursesReply reply = await client.GetAllCoursesAsync(new Empty());
            List<CourseDTO> MappedList = new List<CourseDTO>();

            foreach (var course in reply.Courses)
            {
                MappedList.Add(ProtoMapper<CourseObj, CourseDTO>.Map(course));
            }
            
            return MappedList;
        }
        public static async Task<CourseDTO> AddCourseAsync(AddCourseDTO course)
        {
            using var channel = GrpcChannel.ForAddress("http://servicecourse:80");
            var client = new CourseProto.CourseProtoClient(channel);

            CourseObj s = await client.AddCourseAsync(ProtoMapper<AddCourseDTO, CourseObj>.Map(course));
            
            return ProtoMapper<CourseObj, CourseDTO>.Map(s);
        }
    }
}