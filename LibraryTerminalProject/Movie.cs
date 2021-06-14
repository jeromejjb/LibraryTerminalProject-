using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace LibraryTerminalProject
{
    class Movie : Library
    {
        public Genre Genre { get; set; }

        public override List<Library> PrintItems()
        {
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
            return items;
        }
        public override string SearchFor(string browse)
        {
            StreamReader read = new StreamReader("Movies.txt");
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Movie> items = new List<Movie>(); //change library
            int index = 0;
            foreach (string line in lines)
            {
                Movie m = ConvertToMovie(line);
                if (m != null)
                {
                    items.Add(m);
                }
            }
            if (browse == "all")
            {
                if (index < items.Count)
                {
                    foreach (Movie h in items)
                    {
                        Console.WriteLine($"{index++} : {h.Title}");
                    }
                    return CheckOutItem();
                }
            }
            else if (browse == "genre")
            {
                Genre[] availableGenres = (Genre[])Enum.GetValues(typeof(Genre));
                for (int z = 0; z < availableGenres.Length; z++)
                {
                    Console.WriteLine($"{z} : {availableGenres[z]}");
                }

                Console.WriteLine("Which genre would you like to browse?");
                int pick = int.Parse(Console.ReadLine());

                //foreach (Movie i in items)
                //{
                Genre c = availableGenres[pick];
                
                foreach (Movie f in items)
                {
                    if (f.Genre == c)
                    {
                        Console.WriteLine($"{f.Title}");
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
                        Console.WriteLine("Would you like to continue to browse the library? (Y/N)");
                        string browseAgain = Console.ReadLine().ToLower();
                        if (browseAgain == "y")
                        {
                            return "browse";
                        }
                    }
                }
            
        }
            else
            {
                Console.WriteLine("Please enter a keyword to search the title for:");
                string keyword = Console.ReadLine();
                foreach (Movie m in items)
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
    string filePath = "Movies.txt";
    StreamReader read = new StreamReader(filePath);
    string output = read.ReadToEnd();


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
    Console.WriteLine("Which movie would you like to borrow? (Choose number)");
    int resp = int.Parse(Console.ReadLine());
    Movie a = items[resp];

    if (a.Status == "No")
    {
        Console.WriteLine("Sorry, but that movie is not available at this time.");
        return CheckOutItem();

    }
    else
    {
        DateTime current = DateTime.Now;
        Console.WriteLine($"You have successfully borrowed {a.Title}. You have 2 days.  Your time started:{current}");

        return $"Please return the movie by {current.AddDays(2)}";

    }

}
    }
}
