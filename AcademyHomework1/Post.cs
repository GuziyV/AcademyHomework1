using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyHomework1
{
    class Post
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int Likes { get; set; }
        public List<Comment> Comments { get; set; } 

        public override string ToString()
        {
            return "POST: Id: " + Id + ", Date: " + CreatedAt + ", Title: " + Title + ", Body: " + Body +  ", UserId: "
                + UserId + ", Likes: " + Likes;
        }
    }
}
