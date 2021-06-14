using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryTerminalProject
{
<<<<<<< HEAD
     public class Library
=======
    class Library
>>>>>>> 3615329cf276ab20801e08cbecaa85f86f75756c
    {
        public string Title { get; set; }
        public string Status { get; set; }

        public string Category { get; set; }


        public virtual void PrintItems(string filePath)

        {
            StreamReader read = new StreamReader(filePath);
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Library> items = new List<Library>(); //change library
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
                    Console.WriteLine(($"{index++} : {i.Title}"));

                }
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

<<<<<<< HEAD
   
        public override ()
        {
            List<Books> books = new List<Books>();
            string filePath = @"BookList.txt";
            StreamReader reader = new StreamReader(filePath);

            while (reader.EndOfStream != true)
            {
                string book = reader.ReadLine();
                string[] items = book.Split(',');

                books.Add(new Books(items[0], items[1], items[2], items[3]));

            }

            foreach (Books b in books)
            {
                Console.WriteLine(b.Title);
            }

            int counter = 0;
            string line;
            Console.WriteLine("Enter a author you would like to search our library for");
            string author = Console.ReadLine();
            System.IO.StreamReader file = new System.IO.StreamReader("BookList.txt");

            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(author))
                {
                    break;
                }
                counter++;
            }
            Console.WriteLine("Books by this author are on line {0}", counter);
            file.Close();
            Console.ReadLine();
            
        }

        //public string ReturnItem()
        //{
        //    string line = "Sup";
        //    return line;
        //}

        //public virtual void SearchFor()
        //{
        //    string line = "Hi";
        //    return line;
        //}

=======

   

        public virtual void CheckOutItem()

        {

        }

        public virtual void ReturnItem()
        {
          
            
        }



        public virtual void SearchFor()

        {
            
        }

>>>>>>> 3615329cf276ab20801e08cbecaa85f86f75756c
    }
}



