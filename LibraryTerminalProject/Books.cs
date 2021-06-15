using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace LibraryTerminalProject
{
    public class Books : Library
    {

        //public string Title { get; set; }
        //public string Status { get; set; }
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
                Library l = ConvertToBook(line);
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
                    //Console.WriteLine($"{index++} : {i.Title} : {i.Status}");
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

             List<Library> items = PrintItems();

             Console.WriteLine("Select a book that you would like to checkout");
             int input = int.Parse(Console.ReadLine());
             Books b = (Books)items[input-1];
             if (b.Status == "Not Available")
             {
                 Console.WriteLine("Sorry but that book is already checked out.");
                 return CheckOutItem();
             }
             else
             {
                 DateTime current = DateTime.Now;
                 Console.WriteLine($"You checked out this book at :{current}");
                 Console.WriteLine($"Please return book by {current.AddDays(14)}");

                b.Status = "Not Available";

                StreamWriter write = new StreamWriter("BookList.txt");
                     
                     foreach (Books v in items)
                    {
                    write.WriteLine($"{v.Status},{v.Title},{v.Author},{v.Category}");
                     }
                     write.Close();
          
                 return $"Book checked out: {b.Title}, {b.Author}, {b.Category}";
             }
        }

        public override string SearchFor(string browse)
        {
            StreamReader read = new StreamReader("BookList.txt");
            string output = read.ReadToEnd();
            read.Close();

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
                        Console.WriteLine($"{index + 1} : {b.Title}");
                        index++;
                    }
                    
                }
                return CheckOutItem();
            }
            else if (browse == "author")
            {
                if (index < items.Count)
                {
                    List<string> authors = items.Select(a => a.Author).OrderBy(a => a).ToList();
                    for (var i = 0; i < authors.Count(); i++)
                    {
                        Console.WriteLine($"{i + 1}: {authors[i]}");
                    }
                    int selection = int.Parse(Console.ReadLine());
                    string author = authors[selection - 1];
                    List<Books> booksByAuthor = items.Where(b => b.Author == author).ToList();
                    for (var i = 0; i < booksByAuthor.Count(); i++)
                    {
                        Console.WriteLine($"{i + 1}: {booksByAuthor[i].Title}");
                    }
                    selection = int.Parse(Console.ReadLine());
                    Books b = booksByAuthor[selection - 1];
                    Console.WriteLine("Would you like to check out this book (y/n)");

                    if (Console.ReadLine() == "y")
                    {
                        if (b.Status == "Not Available")
                        {
                            Console.WriteLine("Sorry but that book is already checked out.");
                            return SearchFor("author");
                        }
                        else
                        {
                            DateTime current = DateTime.Now;
                            Console.WriteLine($"You checked out this book at :{current}");
                            Console.WriteLine($"Please return book by {current.AddDays(14)}");

                            b.Status = "Not Available";

                            StreamWriter write = new StreamWriter("BookList.txt");

                            foreach (Books v in items)
                            {
                                write.WriteLine($"{v.Status},{v.Title},{v.Author},{v.Category}");
                            }
                            write.Close();

                            return $"Book checked out: {b.Title}, {b.Author}, {b.Category}";
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a keyword to search the title for:");
                string keyword = Console.ReadLine();

                List<Books> booksByKeyword = items.Where(b => b.Title.ToLower().Contains(keyword.ToLower())).ToList();
                if (booksByKeyword.Count() == 0)
                {
                    Console.WriteLine("No books were found matching that keyword.");
                    return SearchFor("keyword");
                }

                for (var i = 0; i < booksByKeyword.Count(); i++)
                {
                    Console.WriteLine($"{i + 1}: {booksByKeyword[i].Title}");
                    Console.WriteLine("Select a book by it's number");
                }
                int selection = int.Parse(Console.ReadLine());

                Books b = booksByKeyword[selection - 1];
                Console.WriteLine("Would you like to check out this book (y/n)");

                if (Console.ReadLine() == "y")
                {
                    if (b.Status == "Not Available")
                    {
                        Console.WriteLine("Sorry but that book is already checked out.");
                        return SearchFor("keyword");
                    }
                    else
                    {
                        DateTime current = DateTime.Now;
                        Console.WriteLine($"You checked out this book at :{current}");
                        Console.WriteLine($"Please return book by {current.AddDays(14)}");

                        b.Status = "Not Available";

                        StreamWriter write = new StreamWriter("BookList.txt");

                        foreach (Books v in items)
                        {
                            write.WriteLine($"{v.Status},{v.Title},{v.Author},{v.Category}");
                        }
                        write.Close();

                        return $"Book checked out: {b.Title}, {b.Author}, {b.Category}";

                    }
                }
                return "";


            }return "";
        }

        public override string ReturnItem()
        {

                List<Library> items = PrintItems();

            for (var i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i+1} : {items[i].Title}");
            }

            Console.WriteLine("Select a book that you would like to return");
                int input = int.Parse(Console.ReadLine());
                Books b = (Books)items[input-1];
                if (b.Status == "Available")
                {
                    Console.WriteLine("Sorry but that book cannot be returned.");
                    return ReturnItem();
                }
                else
                {
                    DateTime current = DateTime.Now;
                    Console.WriteLine($"You returned this book at :{current}");

                    b.Status = "Available";

                    StreamWriter write = new StreamWriter("BookList.txt");

                    foreach (Books v in items)
                    {
                        write.WriteLine($"{v.Status},{v.Title},{v.Author},{v.Category}");
                    }
                    write.Close();

                    return $"Book returned: {b.Title}, {b.Author}, {b.Category}";
                }
            }

    }

}


