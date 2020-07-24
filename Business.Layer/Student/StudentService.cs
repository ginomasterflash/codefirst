using EFCodeFirst;
using Microsoft.EntityFrameworkCore;
using MyModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Student
{
    public class StudentService : IStudentService
    {
        private readonly MyDbContext _myDbContext;

        public StudentService(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext ?? throw new ArgumentNullException(nameof(myDbContext));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int CreateStudent(StudentModel student)
        {
            EFCodeFirst.Student studentDb = new EFCodeFirst.Student()
            {
                FirstName = student.FirstName.Capitalize(),
                LastName = student.LastName.Capitalize(),
                EnrollmentDate = student.EnrollmentDate,
                State = State.Active,
                Address = new StudentAddress()
                {
                    Address1 = student.Address.Address1,
                    Address2 = student.Address.Address2,
                    City = student.Address.City,
                    Country = student.Address.Country,
                    Zipcode = student.Address.Zipcode,
                    State = student.Address.State
                },
                Owner = new Person()
                {
                    Email = "marco@mail.com",
                    Name = "Marco",
                    Surname = "Petruzzelli"
                },
                RenameTest = JsonConvert.SerializeObject(new Person()
                {
                    Email = "mario.rossi@gmail.com",
                    Name = "mario",
                    Surname = "rossi"
                })
            };

            _myDbContext.Students.Add(studentDb);

            _myDbContext.SaveChanges();

            return studentDb.ID;
        }


        public async Task<StudentModel> GetStudentAsync(int studentId)
        {


            IQueryable<EFCodeFirst.Student> iStudent = _myDbContext.Students
                .Where(x => x.ID == studentId);

            // questa istruzione verrà ignorata perché non avviene l'assegnazione
            iStudent.Where(x => x.State == State.Active);

            // questa istruzione verrà aggiunta in and alle altre condizioni
            iStudent = iStudent.Where(x => x.State == State.Inactive);      


            var student = await iStudent
                .Select(x => new StudentModel()
                {
                    StudentID = x.ID,
                    EnrollmentDate = x.EnrollmentDate,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    //Address = new AddressModel() { Address1 = JsonConvert.DeserializeObject<Person>(x.RenameTest).Email }
                    Person = new PersonModel()
                    {
                        Email = x.Owner.Email,
                        Surname = x.Owner.Surname,
                        Name = x.Owner.Name
                    }
                })
                .FirstOrDefaultAsync();

            return student;
        }
    }
}
