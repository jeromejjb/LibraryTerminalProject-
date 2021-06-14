using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryTerminalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Audiobooks a = new Audiobooks();
            a.ReturnItem();
            a.CheckOutItem();
        }

        public static bool GoAgain()
        {
            Console.Write("Would you like to go again? Y/N");
            string input = Console.ReadLine();

            if (input.ToUpper() == "Y" || input.ToUpper() == "YES")
            {
                Console.WriteLine("");
                Console.WriteLine("");
                return true;
            }
            else if (input.ToUpper() == "N" || input.ToUpper() == "NO")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Must input a valid response.");
                return GoAgain();
            }
        }
    }
}
