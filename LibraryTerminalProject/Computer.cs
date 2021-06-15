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
            Console.ForegroundColor = ConsoleColor.Yellow;
            List<Library> items = new List<Library>(PrintItems());

            Console.WriteLine("Which computer would you like to use? (#)");

            //This while loop ensures that an int is entered by user
            int resp;
            while (Int32.TryParse(Console.ReadLine(), out resp) != true)
            {
                Console.WriteLine("Invalid input please try again.");
            }

            //Checks if the computer is available to use
            Library a = (Library)items[resp];
            if (a.Status.ToLower() == "no")
            {
                Console.WriteLine("Sorry but that computer is already in use.");
                return CheckOutItem();
            }
            else
            {
                DateTime current = DateTime.Now;
                Console.WriteLine($"You can now use the {a.Title}.  You have 2 hours.  Your time started:{current}");

                //Creates new line for the txt file to change the status.
                string newLine = $"No,{a.Title}";

                //This line removes the original string from the List,
                //so we can replace it with the new status string.
                items.Remove(a);

                for (int i = 0; i < items.Count; i++)
                {
                    StreamWriter write = new StreamWriter("Computers.txt");
                    int num = 0;
                    foreach (Library t in items)
                    {
                        if (num < 2)
                        {
                            write.Write($"{t.Status},{t.Title}\n");
                            num++;
                        }
                    }
                    write.Write($"{newLine}");
                    write.Close();
                }
                return $"Please vacate computer at {current.AddHours(2)}\n";
            }
        }

        public override string ReturnItem()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            List<Library> items = new List<Library>(PrintItems());
            Console.WriteLine($"Are you finished using the computer? (Y/N)");
            string finished = Console.ReadLine().ToLower();
            if (finished == "y")
            {
                Console.WriteLine("Which computer would you like to check back in? (#)");
                int resp;
                while(Int32.TryParse(Console.ReadLine(), out resp) != true)
                {
                    Console.WriteLine("Invalid input please try again.");
                }
                Library a = (Library)items[resp];
                if (a.Status.ToLower() == "no")
                {
                    string newLine = $"Yes,{a.Title}";
                    items.Remove(a);
                    for (int i = 0; i < items.Count; i++)
                    {
                        StreamWriter write = new StreamWriter("Computers.txt");
                        int num = 0;
                        foreach (Library t in items)
                        {
                            if (num < 2)
                            {
                                write.Write($"{t.Status},{t.Title}\n");
                                num++;
                            }
                        }
                        write.Write($"{newLine}");
                        write.Close();
                    }
                    return "Thank you!";
                }
            }
            //Allows user to get more time with the computer
            else if (finished == "n")
            {
                Console.WriteLine("Would you like to add more time? (Y/N)");
                if (Console.ReadLine().ToLower() == "y")
                {
                    DateTime current = DateTime.Now;
                    return $"Please vacate computer at {current.AddHours(2)}\n";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                Console.WriteLine("I don't understand...");
                return ReturnItem();
            }
            return "";
        }
    }
}