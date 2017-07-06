using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int init;

            Console.WriteLine("Choose one of the variant: \r\n" +
                              "1: Start server.\r\n" +
                              "2: Exit.\r\n");

            while (int.TryParse(Console.ReadLine(), out init) && (init == 1 || init == 2))
            {
                // Main loop.
                switch (init)
                {
                    case 1:
                        Core.Start();
                        Environment.Exit(0);
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            }            
        }
    }
}
