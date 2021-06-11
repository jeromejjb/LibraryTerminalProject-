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
                    Console.WriteLine($"{index++} : {i.Title}");
                }
            }
        }

        public Library ConvertToList(string line)
        {
            string[] prop = line.Split(',');
            Library l = new Library();

            if (prop.Length == 2) //change
            {
                l.Status = prop[0]; //change
                l.Title = prop[1]; //change
                return l;
            }
            else
            {
                return null;
            }
        }

        public string CheckOutItem()
        {
            string line = "Hi";
            return line;
        }

        public string ReturnItem()
        {
            string line = "Hi";
            return line;
        }

        public string SearchFor()
        {
            string line = "Hi";
            return line;
        }

    } 
}



