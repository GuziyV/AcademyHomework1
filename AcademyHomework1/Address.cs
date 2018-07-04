using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyHomework1
{
    class Address
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string UserId { get; set; }


        public override string ToString()
        {
            return Id + " " + CreatedAt + " " + Country + " " + City + " " + Zip + " " + UserId;
        }
    }
}
