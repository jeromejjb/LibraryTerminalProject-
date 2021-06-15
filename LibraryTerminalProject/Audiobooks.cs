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

        public override List<Library> PrintItems() //Need this method here because it must use convert to audio!!
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            StreamReader read = new StreamReader("Audiobooks.txt");
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Library> items = new List<Library>();
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
            read.Close();
            return items;
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

        public override string CheckOutItem()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            List<Audiobooks> items = new List<Audiobooks>();

            Console.WriteLine("Select an audoibook to that you would like to checkout");
            int input = int.Parse(Console.ReadLine());
            Audiobooks a = items[input];
            if (a.Status == "No")
            {
                Console.WriteLine("Sorry but that book is already checked out.");
                return CheckOutItem();
            }
            else
            {
                DateTime current = DateTime.Now;
                Console.WriteLine($"You checked out this audiobook at :{current}");
                Console.WriteLine($"Please return audio book by {current.AddDays(14)}");

                string newLine = $"No, {a.Title}, {a.Author}, {a.Narrator}, {a.Category}";
                items.Remove(a);

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
                return $"Audiobook checked out: {a.Title}, {a.Author}, {a.Narrator}, {a.Category}";
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override string ReturnItem()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            List<Library> items = new List<Library>(PrintItems());

            Console.WriteLine("What Audiobook would you like to return");
            int input = int.Parse(Console.ReadLine());
            Audiobooks a = (Audiobooks)items[input];

            if (a.Status == "Yes")
            {
                return "Sorry but that book is not checked out";
            }
            else
            {
                DateTime current = DateTime.Now;
                Console.WriteLine($"You returned this audiobook at :{current}");
                Console.WriteLine("Thank you have a nice day!");

                string newLine = $"Yes, {a.Title}, {a.Author}, {a.Narrator}, {a.Category}";
                items.Remove(a);

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
                return $"Audiobook returned: {a.Title}, {a.Author}, {a.Narrator}, {a.Category}";
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override string SearchFor(string browse)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            StreamReader read = new StreamReader("Audiobooks.txt");
            string output = read.ReadToEnd();
            read.Close();
            string[] lines = output.Split('\n');
            List<Audiobooks> items = new List<Audiobooks>(); 
            int index = 0;
            foreach (string line in lines)
            {
                Audiobooks m = ConvertToAudio(line);
                if (m != null)
                {
                    items.Add(m);
                }
            }
            if (browse == "all")
            {
                foreach (Audiobooks h in items)
                {
                    Console.WriteLine($"{index++} : {h.Title}");
                }
                return CheckOutItem();

            }

            else if (browse == "author")
            {
                Console.WriteLine("Please select an author.");
                foreach (Audiobooks h in items)
                {
                    Console.WriteLine($"{index++} : {h.Author}");
                    int a = int.Parse(Console.ReadLine());
                    Console.WriteLine($"{index++} : {h.Title[a]}");
                }
                return CheckOutItem();

            }
            else if (browse == "narrator")
            {
                Console.WriteLine("Please select a narrator.");
                foreach (Audiobooks h in items)
                {
                    Console.WriteLine($"{index++} : {h.Narrator}");
                    int a = int.Parse(Console.ReadLine());

                    Console.WriteLine($"{index++} : {h.Title[a]}");
                }
                return CheckOutItem();
            }

            else
            {
                Console.WriteLine("Please enter a keyword to search the title for:");
                string keyword = Console.ReadLine();
                foreach (Audiobooks m in items)
                {
                    if (m.Title.Contains(keyword))
                    {

                        Console.WriteLine(m.Title);
                        return CheckOutItem();
                    }
                    else
                    {
                        return "I'm sorry, we do not have any movies matching that keyword.";
                    }
                }
            }
            return "";

            Console.ForegroundColor = ConsoleColor.White;


        }
    }
}

