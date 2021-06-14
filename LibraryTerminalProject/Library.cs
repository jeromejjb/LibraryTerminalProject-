using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryTerminalProject
{
    class Library
    {
        public string Title { get; set; }
        public string Status { get; set; }


        public virtual string PrintItems(string filePath)

        {
            StreamReader read = new StreamReader(filePath);
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
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
                    return $"{index++} : {i.Title}";

                }
            }
            return "I'm sorry, there has been an error in our system, please try again.";
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




        public virtual string CheckOutItem()

        {
            return "";
        }

        public virtual string ReturnItem()
        {
            return "";
        }

        public virtual string SearchFor(string libraryItem)

        {
            //if (libraryItem == "books")
            //{
            //    Books bb = new Books();
            //    Console.WriteLine("Would you like to browse by author, keyword, or view all? (author, keyword, all)");
            //    return bb.PrintItems(Console.ReadLine().ToLower());
            //}
            //else if (libraryItem == "audiobooks")
            //{
            //    Audiobooks aa = new Audiobooks();
            //    Console.WriteLine("Would you like to browse by author, narrator, keyword, or view all? (author, narrator, keyword, all)");
            //    return aa.PrintItems(Console.ReadLine().ToLower());
            //}
            //else
            //{
            //    Movie mm = new Movie();
            //    Console.WriteLine("Would you like to browse by genre, keyword, or view all?");
            //    return mm.PrintItems(Console.ReadLine().ToLower());
            //}
            if (libraryItem == "books")
            {
                Books bb = new Books();
                Console.WriteLine("Would you like to browse by author, keyword, or view all? (author, keyword, all)");
                return bb.SearchFor(Console.ReadLine().ToLower());
            }
            else if (libraryItem == "audiobooks")
            {
                Audiobooks aa = new Audiobooks();
                Console.WriteLine("Would you like to browse by author, narrator, keyword, or view all? (author, narrator, keyword, all)");
                return aa.SearchFor(Console.ReadLine().ToLower());
            }
            else
            {
                Movie mm = new Movie();
                Console.WriteLine("Would you like to browse by genre, keyword, or view all?");
                return mm.SearchFor(Console.ReadLine().ToLower());
            }


        }




    }
}



