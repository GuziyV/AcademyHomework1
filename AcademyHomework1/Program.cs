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
        static void Main(string[] args)
        {
            bool repeat = true;
            while(repeat)
            {
                try
                {
                    repeat = Menu.Start();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
