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
        //Movie uses the parent properties, but also news a Genre (enum) property
        public Genre Genre { get; set; }

        public override List<Library> PrintItems()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            StreamReader read = new StreamReader("Movies.txt");
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Library> items = new List<Library>(); //change library
            foreach (string line in lines)
            {
                Movie m = ConvertToMovie(line);
                if (m != null)
                {
                    items.Add(m);
                }
            }
            int index = 0;
            if (index < items.Count)
            {
                foreach (Movie h in items)
                {
                    //Prints the index and the title at that index
                    Console.WriteLine($"{index++} : {h.Title}");
                }
            }
            read.Close();
            return items;
        }
        public override string SearchFor(string browse)
        {
            //Need to create the list to reference for searching, but not print full list
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
            {//Prints all movies
                foreach (Movie h in items)
                {
                    Console.WriteLine($"{index++} : {h.Title}");
                }
                return CheckOutItem();
            }
            else if (browse.ToLower() == "genre")
            {
                //Will print an array of genres from the enums list,
                //by getting the values of each enum in Genre
                Genre[] availableGenres = (Genre[])Enum.GetValues(typeof(Genre));
                for (int z = 0; z < availableGenres.Length; z++)
                {
                    //Prints index and genres
                    Console.WriteLine($"{z} : {availableGenres[z]}");
                }

                Console.WriteLine("Which genre would you like to browse? (#)");
                
                int pick;
                while (Int32.TryParse(Console.ReadLine(), out pick) != true)
                {
                    Console.WriteLine("Invalid input please try again.");
                }

                Genre c = availableGenres[pick];

                foreach (Movie f in items)
                {
                    if (f.Genre == c)
                    {
                        //This line will give the original index number for the titles from the
                        //items list, so the user is still picking from the items list.
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
                        //To browse another genre, this will loop back through the SearchFor method.
                        return SearchFor("browse");
                    }
                    else if (moreGenre == "n")
                    {
                        Console.WriteLine("Would you like to continue to browse movies? (Y/N)");
                        string browseAgain = Console.ReadLine().ToLower();
                        if (browseAgain == "y")
                        {
                            Console.WriteLine("Would you like to browse by genre, keyword, all?");
                            return SearchFor(Console.ReadLine().ToLower());
                        }
                    }
                }
                return "I don't understand, please try again.";
            }
            //Searches the title of the movie for keywords
            else if (browse.ToLower() == "keyword")
            {
                Console.WriteLine("Please enter a keyword to search the title for:");
                string keyword = (Console.ReadLine().ToLower());
                foreach (Movie m in items)
                {
                    //This takes the m.Title to make it a lowercase string, so the string can be check for the keyword entered
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

            if (prop.Length == 3) 
            {
                //This needs to be changed to reflect the number of properties for the specific child class.
                //Movies have 3 properties, so it is changed to 3
                m.Status = prop[0]; 
                m.Title = prop[1]; 
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
            Console.WriteLine("\nWhich movie would you like to borrow? (#)");
            int resp;
            while (Int32.TryParse(Console.ReadLine(), out resp) != true)
            {
                Console.WriteLine("Invalid input please try again.");
            }
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
            int input;
            while (Int32.TryParse(Console.ReadLine(), out input) != true)
            {
                Console.WriteLine("Invalid input please try again.");
            }
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

                string newLine = $"Yes,{a.Title},{a.Genre}\n";
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
                return $"Movie returned: {a.Title},{a.Genre}\n";
            }
        }
    }
}
