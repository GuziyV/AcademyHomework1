using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyHomework1
{
    class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Likes { get; set; }

        public override string ToString()
        {
            return "COMMENT: Id: " + Id + ", Date: " + CreatedAt + ", Body: " + Body + ", UserId: " + UserId + 
                ", PostId: " + PostId + ", Likes: " + Likes;
        }
    }
}
