using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LibraryTerminalProject
{
    class Movie : Library
    {
        public Genre Genre { get; set; }

<<<<<<< Updated upstream
        public override List<Library> PrintItems()
=======
        //}

        public void PrintItems(string filePath)
>>>>>>> Stashed changes
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            StreamReader read = new StreamReader("Movies.txt");
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Library> items = new List<Library>(); //change library
            int index = 0;
            foreach (string line in lines)
            {
                Movie m = ConvertToMovie(line);
                if (m != null)
                {
                    items.Add(m);
                }
                if (index < items.Count)
                {
                    foreach (Movie h in items)
                    {
                        Console.WriteLine($"{index++} : {h.Title}");
                    }
                }
            }
            read.Close();
            return items;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override string SearchFor(string browse)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            StreamReader read = new StreamReader("Movies.txt");
            string output = read.ReadToEnd();
            string[] lines = output.Split('\n');
            List<Movie> items = new List<Movie>();
            read.Close();

            int index = 0;
            foreach (string line in lines)
            {
                Movie m = ConvertToMovie(line);
                if (m != null)
                {
                    items.Add(m);
                }
            }
            if (browse.ToLower() == "all")
            {
                foreach (Movie h in items)
                {
                    Console.WriteLine($"{index++} : {h.Title}");
                }
                return CheckOutItem();
            }
            else if (browse.ToLower() == "genre")
            {
                Genre[] availableGenres = (Genre[])Enum.GetValues(typeof(Genre));
                for (int z = 0; z < availableGenres.Length; z++)
                {
                    Console.WriteLine($"{z} : {availableGenres[z]}");
                }

                Console.WriteLine("Which genre would you like to browse?");
                int pick = int.Parse(Console.ReadLine());

                Genre c = availableGenres[pick];

                foreach (Movie f in items)
                {
                    if (f.Genre == c)
                    {
                        Console.WriteLine($"{items.IndexOf(f)}: {f.Title}");
                    }
                }
                Console.WriteLine("\nWould you like to check out one of these movies? (Y/N)");
                string checkOut = Console.ReadLine().ToLower();
                if (checkOut == "y")
                {
                    return CheckOutItem();
                }
                else if (checkOut == "n")
                {
                    Console.WriteLine("Would you like to browse another genre? (Y/N)");
                    string moreGenre = Console.ReadLine().ToLower();
                    if (moreGenre == "y")
                    {
                        return SearchFor("browse");
                    }
                    else if (moreGenre == "n")
                    {
                        Console.WriteLine("Would you like to continue to browse movies? (Y/N)");
                        string browseAgain = Console.ReadLine().ToLower();
                        if (browseAgain == "y")
                        {
                            Console.WriteLine("Would you like to browse by genre, keyword, or view all?");
                            return SearchFor(Console.ReadLine().ToLower());
                        }

                    }

                }
                return "I don't understand, please try again.";

            }
            else if (browse.ToLower() == "keyword")
            {
                Console.WriteLine("Please enter a keyword to search the title for:");
                string keyword = (Console.ReadLine().ToLower());
                foreach (Movie m in items)
                {
                    if (m.Title.ToString().ToLower().Contains(keyword))
                    {
                        Console.WriteLine($"{items.IndexOf(m)}: {m.Title}");
                    }

                }
                Console.WriteLine("\nWould you like to check out a movie from this list? (Y/N)");
                string yesCheckOut = Console.ReadLine().ToLower();
                if (yesCheckOut == "y")
                {
                    return CheckOutItem();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "I'm sorry, I don't understand.";
            }
        }

        public virtual Movie ConvertToMovie(string line)
        {
            string[] prop = line.Split(',');
            Movie m = new Movie();

            if (prop.Length == 3) //change
            {
                m.Status = prop[0]; //change
                m.Title = prop[1]; //change
                m.Genre = (Genre)Enum.Parse(typeof(Genre), prop[2]);

                return m;
            }
            else
            {
                return null;
            }
        }

        public override string CheckOutItem()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            StreamReader read = new StreamReader("Movies.txt");
            string output = read.ReadToEnd();
            read.Close();

            string[] lines = output.Split('\n');
            List<Movie> items = new List<Movie>();
            foreach (string line in lines)
            {
                Movie mov = ConvertToMovie(line);
                if (mov != null)
                {
                    items.Add(mov);
                }
            }
            Console.WriteLine("\nWhich movie would you like to borrow? (Choose number)");
            int resp = int.Parse(Console.ReadLine());
            Movie a = items[resp];

            if (a.Status == "No")
            {
                Console.WriteLine("Sorry, but that movie is not available at this time.");
                return CheckOutItem();
            }
            else
            {
                string newLine = $"No,{a.Title},{a.Genre}\n";
                items.Remove(a);

                for (int i = 0; i < items.Count; i++)
                {
                    StreamWriter write = new StreamWriter("Movies.txt");
                    int num = 0;
                    foreach (Movie t in items)
                    {
                        if (num < 9)
                        {
                            write.Write($"{t.Status},{t.Title},{t.Genre}\n");
                            num++;
                        }
                    }
                    write.Write($"{newLine}");
                    write.Close();

                }
                DateTime current = DateTime.Now;
                Console.WriteLine($"You have successfully borrowed {a.Title} for 2 days. Your time started:{current}");
                return $"Please return the movie by {current.AddDays(2)}\n";

            }


        }
        public override string ReturnItem()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            List<Library> items = new List<Library>(PrintItems());

            Console.WriteLine("What movie would you like to return");
            int input = int.Parse(Console.ReadLine());
            Movie a = (Movie)items[input];

            if (a.Status == "Yes")
            {
                Console.WriteLine("Sorry but that movie is not checked out");
                return ReturnItem();
            }
            else
            {
                DateTime current = DateTime.Now;
                Console.WriteLine($"You returned this movie at :{current}");


                string newLine = $"Yes,{a.Title},{a.Genre}/n";
                items.Remove(a);

                for (int i = 0; i < items.Count; i++)
                {
                    StreamWriter write = new StreamWriter("Movies.txt");
                    int num = 0;
                    foreach (Movie t in items)
                    {
                        if (num < 9)
                        {
                            write.Write($"{t.Status},{t.Title},{t.Genre}\n");
                            num++;
                        }
                    }
                    write.Write($"{newLine}");
                    write.Close();
                }
                return $"Movie returned: {a.Title},{a.Genre}";

            }
        }
    }
}
