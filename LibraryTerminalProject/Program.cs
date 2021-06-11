using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryTerminalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Library ll = new Library();
            Movie mm = new Movie();

            ll.PrintItems("Computers.txt");
            mm.PrintItems("Movies.txt");
        }
    }
}
