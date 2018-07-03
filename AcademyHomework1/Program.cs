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
            int userId = 12; //set user id here
            foreach (var post in s.GetNumberOfCommentsById(userId))
            {
                Console.WriteLine(post.Item1);
                Console.WriteLine("Number of comments: {0}", post.Item2);
            }
        }

        static public void SecondTask()
        {
            int userId = 21; //set user id here
            foreach (var comment in s.GetCommentsWithSmallBodyById(userId))
            {
                Console.WriteLine(comment);
            }
        }

        static public void ThirdTask()
        {
            int userId = 45; //set user id here
            foreach (var todo in s.GetCompletedTasksById(userId))
            {
                Console.WriteLine("Id: {0}, name:{1}", todo.Item1, todo.Item2);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("first task:");
            FirstTask();
            Console.WriteLine("\nsecond task: ");
            SecondTask();
            Console.WriteLine("\nthird task:");
            ThirdTask();
        }
    }
}
