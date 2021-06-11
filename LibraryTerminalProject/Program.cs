using System;

namespace LibraryTerminalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string filePath = "Audiobooks.txt";
            Audiobooks l = new Audiobooks();
            l.PrintItems(filePath);
            l.CheckOutItem();
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
