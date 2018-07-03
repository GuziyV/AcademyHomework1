using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AcademyHomework1
{
    class Program
    {
        static Service s = new Service();

        static public void FirstTask()
        {
            int userId = 11; //set user id here
            foreach (var post in s.GetNumberOfCommentsById(userId))
            {
                Console.WriteLine(post.Key);
                Console.WriteLine("Number of comments: {0}", post.Value);
            }
        }

        static public void SecondTask()
        {
            int userId = 17; //set user id here
            foreach (var comment in s.GetCommentsWithBigBodyById(userId))
            {
                Console.WriteLine(comment);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("first task:");
            FirstTask();
            Console.WriteLine("second task: ");
            SecondTask();
        }
    }
}
