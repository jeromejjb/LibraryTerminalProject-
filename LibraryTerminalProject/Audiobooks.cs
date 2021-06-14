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
        public Category Category { get; set; }

        public override void PrintItems(string filePath) //what goes in
        {
            Console.ForegroundColor = ConsoleColor.Blue;
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
                l.Category = (Category)Enum.Parse(typeof(Category), prop[4]);
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
            Console.ForegroundColor = ConsoleColor.Blue;

            string filePath = "Audiobooks.txt";
            StreamReader read = new StreamReader(filePath);
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Audiobooks> items = new List<Audiobooks>(); 
            foreach (string line in lines)
            {
                Audiobooks l = ConvertToAudio(line);
                if (l != null)
                {
                    items.Add(l);
                }
            }

            int index = 0;
            if (index < items.Count)
            {
                
                foreach (Audiobooks i in items)
                {
                    Console.WriteLine($"{index++} : STATUS({i.Status}), TITLE : {i.Title}, by {i.Author}, narrated by {i.Narrator}, genre : {i.Category}");
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
        public override void ReturnItem()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            string filePath = "Audiobooks.txt";
            StreamReader read = new StreamReader(filePath);
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Audiobooks> items = new List<Audiobooks>();
            foreach (string line in lines)
            {
                Audiobooks l = ConvertToAudio(line);
                if (l != null)
                {
                    items.Add(l);
                }
            }

            int index = 0;
            if (index < items.Count)
            {

                foreach (Audiobooks i in items)
                {
                    Console.WriteLine($"{index++} : STATUS({i.Status}), TITLE : {i.Title}, by {i.Author}, narrated by {i.Narrator}, genre : {i.Category}");
                }
            }

            Console.WriteLine("What Audiobook would you like to return");
            int input = int.Parse(Console.ReadLine());
            Audiobooks a = items[input];

            if (a.Status == "Yes")
            {
                Console.WriteLine("Sorry but that book is not checked out");
            }
            else
            {
                read.Close();
                DateTime current = DateTime.Now;
                Console.WriteLine($"You returned this audiobook at :{current}");
                Console.WriteLine("Thank you have a nice day!");

                string newLine = $"Yes, {a.Title}, {a.Author}, {a.Narrator}, {a.Category}";
                items.Remove(a);

                List<Audiobooks> lib = new List<Audiobooks>();

                for (int i = 0; i < items.Count; i++)
                {
                    
                    StreamWriter write = new StreamWriter("Audiobooks.txt");
                    int num = 0;
                    foreach (Audiobooks t in items)
                    {
                        if (num < 5)
                        {
                            write.Write($"{t.Status},{t.Title} {t.Author}, {t.Narrator}, {t.Category}\n");
                            num++;
                        }
                    }
                    write.Write($"{newLine}");
                    write.Close();
                }
                

                //StreamReader reader = new StreamReader(filePath);
                //string original = reader.ReadToEnd();
                //reader.Close();
                //StreamWriter write = new StreamWriter("Audiobooks.txt");
                //write.Write (original + newLine);
            }

        }

        public override void SearchFor()
        {
            
        }

    }
}
