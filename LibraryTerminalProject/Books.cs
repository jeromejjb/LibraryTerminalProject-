using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryTerminalProject
{
    class Books : Library
    {

        public string Title { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }




        public Library ConvertToBook(string line)
        {
            string[] prop = line.Split(',');
            Books b = new Books();

            if (prop.Length == 2) //change
            {
                b.Status = prop[0]; //change
                b.Title = prop[1]; //change
                b.Category = (Category)Enum.Parse(typeof(Catetory), prop[2]);
                return b;
            }
            else
            {
                return null;
            }
        }

        public void PrintAllBooks()
        {
            for (int i = 0; i < Books.Count; i++)
            {
                Books b = Books[i];
                string title = b.Title;
                Console.WriteLine($"{i + 1}: {title}");
            }
        }


        public void CheckOutItem()
        {
            Console.WriteLine($"Search for for a book by author or keyword");
            string input = Console.ReadLine();
            
           
        }
    }   
    


    
}
