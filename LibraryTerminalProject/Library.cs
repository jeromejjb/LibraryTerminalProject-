using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryTerminalProject
{

    public class Library
    {
        public string Title { get; set; }
        public string Status { get; set; }

        public virtual List<Library> PrintItems()
        {
            //This takes in the Computers.txt because the
            //computer child class has no additional properties

            StreamReader read = new StreamReader("Computers.txt");
            string output = read.ReadToEnd();
            string[] lines = output.Split('\n', '\r');
            List<Library> items = new List<Library>();
            foreach (string line in lines)
            {
                Library l = ConvertToList(line);
                if (l != null)
                {
                    items.Add(l);
                }
            }
            int index = 0;
            if (index < items.Count)
            {
                foreach (Library i in items)
                {
                    Console.WriteLine($"{index++} : {i.Title}");
                }
                read.Close();
                return items;
            }
            else
            {
                Console.WriteLine("I'm sorry, there has been an error in our system, please try again.");

                //Repeats the method if there is not a proper response
                return PrintItems();
            }


        }


        public virtual Library ConvertToList(string line)

        {
            string[] prop = line.Split(',');
            Library l = new Library();

            if (prop.Length == 2) //change
            {
                l.Status = prop[0]; //change
                l.Title = prop[1];

                //change
                return l;
            }
            else
            {
                return null;
            }
        }


        public virtual string SearchFor(string libraryItem)

        {
            //Parent SearchFor will delegate to the child SearchFor methods

            if (libraryItem == "books")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Books bb = new Books();
                Console.WriteLine("Books: Would you like to browse by author, keyword, or all?");
                return bb.SearchFor(Console.ReadLine().ToLower());
            }
            else if (libraryItem == "audiobooks")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Audiobooks aa = new Audiobooks();
                Console.WriteLine("Audiobooks:  Would you like to browse by author, narrator, keyword, or all?");
                return aa.SearchFor(Console.ReadLine().ToLower());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Movie mm = new Movie();
                Console.WriteLine("Movies:  Would you like to browse by genre, keyword, or all?");
                return mm.SearchFor(Console.ReadLine().ToLower());
            }

        }


        //CheckOutItem and ReturnItem are not used in parent
        //class, but must be in child classes.

        public virtual string CheckOutItem()
        {
            return "";
        }
        public virtual string ReturnItem()
        {
            return "";
        }
    }


}


