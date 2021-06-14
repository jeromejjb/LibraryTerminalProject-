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


            Console.WriteLine("Welcome to the Tri-State Library of Grand Circus-dria!");
            Console.WriteLine("What brought you in today? Would you like to browse the catalog or return an item?");
            Console.WriteLine("Please enter: 'browse' or 'return'");
            string response = GetIntention();
            Console.WriteLine(response);


        }

        public static string GetIntention()

        {

            string answer = Console.ReadLine().ToLower();
            if (answer == "browse")
            {
                Console.WriteLine("What are you interested in?  Books, audiobooks, movies, or computers?");
                return GetLibraryItem("browse");
            }
            else if (answer == "return")
            {
                Console.WriteLine("What item would you like to return today?  Books, audiobooks, movies, or computers?");
                return GetLibraryItem("return");
            }
            else
            {
                Console.WriteLine("I'm sorry, I didn't understand that response, please try again.");
                Console.WriteLine("Please enter: 'browse' or 'return'");
                return GetIntention();
            }
        }

        public static string GetLibraryItem(string intention)
        {
            Library ll = new Library();

            string answer = Console.ReadLine().ToLower();
            if (answer == "books")
            {
                Books bb = new Books();
                bb.PrintItems("BookList.txt");
                if (intention == "browse")
                {
                    return ll.SearchFor(intention);
                }
                else
                {
                    return bb.ReturnItem();
                }

            }
            else if (answer == "audiobooks")
            {
                Audiobooks aa = new Audiobooks();

                if (intention == "browse")
                {
                    return ll.SearchFor(intention);
                }
                else
                {
                    return aa.ReturnItem();
                }
            }
            else if (answer == "movies")
            {
                Movie mm = new Movie();

                if (intention == "browse")
                {
                    return ll.SearchFor(intention);
                }
                else
                {
                    return mm.ReturnItem();
                }
            }
            else if (answer == "computers")
            {
                Computer cc = new Computer();
                cc.PrintItems("Computers.txt");
                if (intention == "browse")
                {
                    return cc.CheckOutItem();
                }
                else
                {
                    return cc.ReturnItem();
                }
            }
            else
            {
                Console.WriteLine("I'm sorry, I didn't understand that response.");
                Console.WriteLine("Please enter: books, audiobooks, movies, or computers?");
                return GetLibraryItem(intention);
            }
        }

        public static bool GoAgain(string message)
        {
            Console.Write("Would you like to start over? Y/N");
            string input = Console.ReadLine().ToUpper();

            if (input.StartsWith("Y"))
            {
                Console.WriteLine("");
                Console.WriteLine("");
                return true;
            }
            else if (input.StartsWith("N"))
            {
                return false;
            }
            else
            {
                return GoAgain("I'm sorry, I didn't understand.");
            }

        }
    }
}
