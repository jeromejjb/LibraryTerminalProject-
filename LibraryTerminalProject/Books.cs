using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryTerminalProject
{
    public class Books : Library
    {

        public string Title { get; set; }
        public string Status { get; set; }
        public string Author { get; set; }

        public string Category { get; set; }

        public Books(string Status, string Title, string Author, string Category)
        {
            this.Status = Status;
            this.Title = Title; 
            this.Author = Author;
            this.Category = Category;
        }

        public void PrintItems(string filePath) //what goes in
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
                    Console.WriteLine($"{index++} : {i.Title} : {i.Status}");
                }
            }

        }

        public Books GetBook()
        {
            string filePath = @"BooksList.txt";

        }

        public static string GetUserInput(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine().ToLower().Trim();
            return input;
        }

        public void CheckOutItem()
        {
            Console.WriteLine("What book would you like to check out?");
            string input = Console.ReadLine();
            int index = int.Parse(input);
         
        }
    }

    public string ReturnItem()
    {
        string line = "Sup";
        return line;
    }

    public string SearchFor()
    {
        string line = "Hi";
        return line;
    }


}

}
