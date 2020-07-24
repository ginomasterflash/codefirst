using System;
using System.Collections.Generic;
using System.Text;

namespace MyModel
{
    public class AddressModel
    {
        public int? AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
