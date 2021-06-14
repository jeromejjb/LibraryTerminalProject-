using System;
using System.Collections.Generic;
using System.IO;
<<<<<<< HEAD
=======
using System.Text;
>>>>>>> 3615329cf276ab20801e08cbecaa85f86f75756c

namespace LibraryTerminalProject
{
    class Program
    {
        static void Main(string[] args)
        {


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
>>>>>>> 3615329cf276ab20801e08cbecaa85f86f75756c
    }
}
