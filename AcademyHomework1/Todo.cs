using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyHomework1
{
    class Todo
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }

        public override string ToString()
        {
            return Id + " " + CreatedAt + " " + Name + " " + IsCompleted + " " + UserId;
        }
    }
}
