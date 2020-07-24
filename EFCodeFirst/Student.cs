using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCodeFirst
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public State State { get; set; }// = State.Active;

        public string RenameTest { get; set; }

        [NotMapped]
        public List<string> Sport { get; set; }

        internal string _Owner { get; set; }
        [NotMapped]
        public Person Owner
        {
            get { return _Owner == null ? null : JsonConvert.DeserializeObject<Person>(_Owner); }
            set { _Owner = JsonConvert.SerializeObject(value); }
        }


        // 1 a molti
        public ICollection<Enrollment> Enrollments { get; set; }

        // 1 a 1
        //public virtual StudentAddress Address { get; set; }
        public StudentAddress Address { get; set; }
    }
}
