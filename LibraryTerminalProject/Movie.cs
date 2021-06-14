using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace LibraryTerminalProject
{
    class Movie : Library
    {
        public Genre Genre { get; set; }
        //public Movie(string Status, string Title, Category Category)
        //{

        //}

        public override void PrintItems(string filePath)
        {
            StreamReader read = new StreamReader(filePath);
            string output = read.ReadToEnd();

            string[] lines = output.Split('\n');
            List<Library> items = new List<Library>(); //change library
            foreach (string line in lines)
            {
                Library l = ConvertToMovie(line);
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
                    Console.WriteLine(($"{index++} : {i.Title}"));
                }
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
    }
}
