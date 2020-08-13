using MyModel;
using System.Threading.Tasks;

namespace Business.Layer.Student
{
    public interface IStudentService
    {
        int CreateStudent(StudentModel student);
        Task<StudentModel> GetStudentAsync(int studentId);
        void GeneratePdf();
        string Html2Pdf();
        void HtmlToPdf1();
        void HtmlToPdf2();
        void ConvertHtmlToPdf();
    }
}