using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibraryTerminalProject
{
    class Audiobooks : Library
    {
        public string Author { get; set; }
        public string Narrator { get; set; }
        public string Category { get; set; }

        public void PrintItems(string filePath) //what goes in
        {
            StreamReader read = new StreamReader(filePath);
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Library> items = new List<Library>(); //change library
            foreach (string line in lines)
            {
                Library l = ConvertToAudio(line);
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

        public Audiobooks ConvertToAudio(string line)
        {
            string[] prop = line.Split(',');
            Audiobooks l = new Audiobooks();

            if (prop.Length == 5) //change
            {
                l.Status = prop[0]; //change
                l.Title = prop[1];
                l.Author = prop[2];
                l.Narrator = prop[3];
                l.Category = prop[4];
                    //change
                return l;
            }
            else
            {
                return null;
            }
        }

        public override void CheckOutItem()
        {
            string filePath = "Audiobooks.txt";
            StreamReader read = new StreamReader(filePath);
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Audiobooks> items = new List<Audiobooks>(); //change library
            foreach (string line in lines)
            {
                Audiobooks l = ConvertToAudio(line);
                if (l != null)
                {
                    items.Add(l);
                }
            }

            Console.WriteLine("Select an Audoibook to that you would like to checkout");
            int input = int.Parse(Console.ReadLine());
            Audiobooks a = items[input];
            if (a.Status == "No")
            {
                Console.WriteLine("Sorry but that book is already checked out."); 
                
            }
            else
            {
                DateTime current = DateTime.Now;
                Console.WriteLine($"You checked out this audiobook at :{current}");
                Console.WriteLine($"Please return audio book by {current.AddDays(14)}");
                
            }
        }
        public override string ReturnItem()
        {
            string line = "Sup";
            return line;
        }

        public override string SearchFor()
        {
            string line = "Hi";
            return line;
        }

    }
}
