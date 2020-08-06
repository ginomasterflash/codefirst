using MyModel;
using System.Threading.Tasks;

namespace Business.Layer.Student
{
    public interface IStudentService
    {
        int CreateStudent(StudentModel student);
        Task<StudentModel> GetStudentAsync(int studentId);
        void GeneratePdf();
        void Html2Pdf();
    }
}