using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace LibraryTerminalProject
{
    class Computer : Library
    {
        public Computer()
        {
           
        }
        public override string CheckOutItem()
        {
            string filePath = "Computers.txt";
            StreamReader read = new StreamReader(filePath);
            string output = read.ReadToEnd();
  

            string[] lines = output.Split('\n');
            List<Library> items = new List<Library>();
            foreach (string line in lines)
            {
                Library l = ConvertToList(line);
                if (l != null)
                {
                    items.Add(l);
                }
            }

            Console.WriteLine("Which computer would you like to use?");
            int resp = int.Parse(Console.ReadLine());
            Library a = items[resp];
            if (a.Status == "No")
            {
                Console.WriteLine("Sorry but that computer is already in use.");
                return CheckOutItem();

            }
            else
            {   
                DateTime current = DateTime.Now;
                Console.WriteLine($"You can now use the computer.  You have 2 hours.  Your time started:{current}");
                //Console.WriteLine($"Please vacate computer at {current.AddHours(2)}");
                return $"Please vacate computer at {current.AddHours(2)}";
            }

        }

        public override string ReturnItem()
        {
            Console.WriteLine($"Are you finished using the computer?");
            if(Console.ReadLine().ToLower() == "y")
            {
                return "Yes";
            }
            else if (Console.ReadLine().ToLower() == "n")
            {
                return "Would you like more time?";
    
            }
            else
            {
                Console.WriteLine("I don't understand...");
                return ReturnItem();
            }
        }
    }

    
}
