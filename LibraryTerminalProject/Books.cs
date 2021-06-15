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
        public Genre Category { get; set; }

        public override List<Library> PrintItems() //what goes in
        {
            StreamReader read = new StreamReader("BookList.txt");
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Library> items = new List<Library>(); //change library
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                Library l = ConvertToBook(line);
                Console.WriteLine(l);
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
            read.Close();
            return items;

        }

        public Books ConvertToBook(string line)
        {
            string[] prop = line.Split(',');
            Books b = new Books();

            if (prop.Length == 4) //change
            {
                b.Status = prop[0]; //change
                b.Title = prop[1];
                b.Author = prop[2];
                b.Category = (Genre)Enum.Parse(typeof(Genre), prop[3]);
                //change
                return b;
            }
            else
            {
                return null;
            }
        }

        public override string CheckOutItem()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            List<Library> items = PrintItems();

            Console.WriteLine("Select a book that you would like to checkout");
            int input = int.Parse(Console.ReadLine());
            Books b = (Books)items[input];
            if (b.Status == "No")
            {
                Console.WriteLine("Sorry but that book is already checked out.");
                return CheckOutItem();
            }
            else
            {
                DateTime current = DateTime.Now;
                Console.WriteLine($"You checked out this audiobook at :{current}");
                Console.WriteLine($"Please return book by {current.AddDays(14)}");

                string newLine = $"No, {b.Title}, {b.Author}, {b.Category}";
                items.Remove(b);

                for (int i = 0; i < items.Count; i++)
                {
                    StreamWriter write = new StreamWriter("BookList.txt");
                    int num = 0;
                    foreach (Books v in items)
                    {
                        if (num < 5)
                        {
                            write.Write($"{b.Status},{b.Title} {b.Author}, {b.Category}\n");
                            num++;
                        }
                    }
                    write.Write($"{newLine}");
                    write.Close();
                }
                return $"Book checked out: {b.Title}, {b.Author}, {b.Category}";
            }





        }

        public override string SearchFor(string browse)
        {
            StreamReader read = new StreamReader("BookList.txt");
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Books> items = new List<Books>(); //change library
            int index = 0;
            foreach (string line in lines)
            {
                Books b = ConvertToBook(line);
                if (b != null)
                {
                    items.Add(b);
                }
            }
            if (browse == "all")
            {
                if (index < items.Count)
                {
                    foreach (Books b in items)
                    {
                        Console.WriteLine($"{index++} : {b.Title}");
                    }
                }
                return CheckOutItem();
            }
            else if (browse == "author")
            {
                return "";
            }
            else
            {
                Console.WriteLine("Please enter a keyword to search the title for:");
                string keyword = Console.ReadLine();
                foreach (Books b in items)
                {
                    if (b.Title.Contains(keyword))
                    {

                        Console.WriteLine(b.Title);
                        return CheckOutItem();
                    }
                    else
                    {
                        return "I'm sorry, we do not have any books matching that keyword.";
                    }
                }
                return "";
            }



        }

    }

}


