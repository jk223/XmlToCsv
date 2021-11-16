using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gentrack_JagmeetPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new ProcessingEngine().Process();
                Console.WriteLine("Processing Completed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occurred");
                Console.WriteLine(e);
                //swallow exception for demo purpose
            }

            Console.ReadLine();//hold the console window
        }
    }
}
