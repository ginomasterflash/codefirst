using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace MyModel
{
    public class StudentModel
    {
        public int? StudentID { get; set; }

        [MinLength(5)]
        public string LastName { get; set; }

        [Required]
        [MinLength(5)]
        public string FirstName { get; set; } = null!;

        [Required]
        public DateTime EnrollmentDate { get; set; }

        public AddressModel? Address { get; set; }

        public PersonModel Person  { get;set; }
    }
}
