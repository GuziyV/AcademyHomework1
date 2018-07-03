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
            foreach (var post in s.GetNumberOfCommentsById(11))
            {
                Console.WriteLine(post.Key);
                Console.WriteLine("Number of comments: {0}", post.Value);
            }
        }

        static void Main(string[] args)
        {
            FirstTask();
        }
    }
}
