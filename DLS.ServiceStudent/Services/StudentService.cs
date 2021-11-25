using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using DLS.EF.DatabaseContexts;
using DLS.Models.Models;
using DLS.Models.Managers;
using DLS.Models.DTO;
using DLS.ServiceStudent.Protos;

namespace DLS.ServiceStudent
{
    public class StudentService : StudentProto.StudentProtoBase
    {
        private readonly ILogger<StudentService> _logger;
        public StudentService(ILogger<StudentService> logger)
        {
            _logger = logger;
        }

        public override Task<StudentObj> GetStudentById(Int64Value id, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Student s = dbContext.Students.Where(x => x.Id == id.Value).SingleOrDefault();
                return Task.FromResult(ProtoMapper<Student, StudentObj>.Map(s));
            }
        }
        public override Task<AllStudentsReply> GetAllStudents(Empty empty, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                List<Student> students = dbContext.Students.ToList();
                AllStudentsReply reply = new AllStudentsReply{};
                students.ForEach(s => reply.Students.Add(
                    ProtoMapper<Student, StudentObj>.Map(s)));
                return Task.FromResult(reply);
            }
        }
        public override Task<StudentObj> AddStudent(StudentObj student, ServerCallContext context)
        {
            using (var dbContext = new SchoolContext())
            {
                Student s = ProtoMapper<StudentObj, Student>.Map(student);
                dbContext.Students.Add(s);
                dbContext.SaveChanges();
                return Task.FromResult(ProtoMapper<Student, StudentObj>.Map(s));
            }
        }
    }
}
