using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyHomework1
{
    class User
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }

        public string outputAsNode()
        {
            return "-" + this.ToString();
        }

        public override string ToString()
        {
            return Id + " " + CreatedAt + " " + Name + " " + Avatar + " " + Email;
        }
    }
}
