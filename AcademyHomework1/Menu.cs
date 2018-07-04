using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyHomework1
{
    static class Menu
    {
        static Service _service = new Service();

        static public bool Start()
        {
            string key;
            Console.WriteLine("enter a task(1-6), or press another keyword to exit the programm");
            key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    FirstTask();
                    return true;
                case "2":
                    SecondTask();
                    return true;
                case "3":
                    ThirdTask();
                    return true;
                case "4":
                    FourthTask();
                    return true;
                case "5":
                    FifthTask();
                    return true;
                case "6":
                    SixthTask();
                    return true;
                default: return false;
            }
        }

        static private void FirstTask()
        {
            Console.WriteLine("enter user id: "); 
            int userId = Int32.Parse(Console.ReadLine());
            foreach (var post in _service.GetNumberOfCommentsById(userId))
            {
                Console.WriteLine(post.Item1);
                Console.WriteLine("Number of comments: {0}", post.Item2);
            }
        }

        static private void SecondTask()
        {
            Console.WriteLine("enter user id: "); 
            int userId = Int32.Parse(Console.ReadLine());
            foreach (var comment in _service.GetCommentsWithSmallBodyById(userId))
            {
                Console.WriteLine(comment);
            }
        }

        static private void ThirdTask()
        {
            Console.WriteLine("enter user id: "); 
            int userId = Int32.Parse(Console.ReadLine());
            foreach (var todo in _service.GetCompletedTasksById(userId))
            {
                Console.WriteLine("Id: {0}, name:{1}", todo.Item1, todo.Item2);
            }
        }

        static private void FourthTask()
        {
            foreach (var user in _service.GetSortedUsers())
            {
                Console.WriteLine("User: {0}", user.Name);
                user.ShowTodos();
                Console.WriteLine();
            }
        }
        
        static private void FifthTask()
        {
            Console.WriteLine("enter user id: "); 
            int userId = Int32.Parse(Console.ReadLine());
            var structure = _service.GetFirstStructure(userId);
            Console.WriteLine("User: ");
            Console.WriteLine(structure.Item1);
            Console.WriteLine("Last Post: ");
            Console.WriteLine(structure.Item2);
            Console.WriteLine("Number of comments under the last post: ");
            Console.WriteLine(structure.Item3);
            Console.WriteLine("Number of undone tasks: ");
            Console.WriteLine(structure.Item4);
            Console.WriteLine("The most popular post(by comments, where comment > 80)");
            Console.WriteLine(structure.Item5);
            Console.WriteLine("The Most Popular Post(by Likes)");
            Console.WriteLine(structure.Item6);
        }

        static private void SixthTask()
        {
            Console.WriteLine("enter post id: "); 
            int userId = Int32.Parse(Console.ReadLine());
            var structure = _service.GetSecondStructure(userId);
            Console.WriteLine("Post: ");
            Console.WriteLine(structure.Item1);
            Console.WriteLine("The longest comment: ");
            Console.WriteLine(structure.Item2);
            Console.WriteLine("The likest comment: ");
            Console.WriteLine(structure.Item3);
            Console.WriteLine("Number of comments where 0 likes or body > 80: ");
            Console.WriteLine(structure.Item4);
        }
    }
}
