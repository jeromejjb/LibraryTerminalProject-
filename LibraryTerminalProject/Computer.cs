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

            Console.WriteLine("Which computer would you like to use? (Enter number)");
            int resp = int.Parse(Console.ReadLine());
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
                //Console.WriteLine($"Please vacate computer at {current.AddHours(2)}");
                string newLine = $"No,{a.Title}";
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
                return $"Please vacate computer at {current.AddHours(2)}";


            }
        }

        public override string ReturnItem()
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            List<Library> items = new List<Library>(PrintItems());

            Console.WriteLine($"Are you finished using the computer? (Y/N)");
            string finished = Console.ReadLine().ToLower();
            if (finished == "y")
        }
        public override string CheckOutItem()
        {
            List<Library> items = new List<Library>(PrintItems());

            Console.WriteLine("Which computer would you like to use?");
            int resp = int.Parse(Console.ReadLine());
            Library a = (Library)items[resp];
            if (a.Status.ToLower() == "no")
            {
                Console.WriteLine("Sorry but that computer is already in use.");
                return CheckOutItem();

            }
            else
            {
                DateTime current = DateTime.Now;
                Console.WriteLine($"You can now use the computer.  You have 2 hours.  Your time started:{current}");
                //Console.WriteLine($"Please vacate computer at {current.AddHours(2)}");
                string newLine = $"No, {a.Title}";
                items.Remove(a);
                for (int i = 0; i < items.Count; i++)
                {
                    StreamWriter write = new StreamWriter("Computers.txt");
                    int num = 0;
                    foreach (Library t in items)
                    {
                        if (num < 2)
                        {
                            write.Write($"{t.Status},{t.Title}");
                            num++;
                        }
                    }
                    write.Write($"{newLine}");
                    write.Close();

                }
                return $"Please vacate computer at {current.AddHours(2)}";


            }
        }

        public override string ReturnItem()
        {

            Console.WriteLine($"Are you finished using the computer?");
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.WriteLine("Which computer would you like to check back in?");
                int resp = int.Parse(Console.ReadLine());
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
            else if (finished == "n")
            {
                Console.WriteLine("Would you like to add more time?");
                if (Console.ReadLine().ToLower() == "y")
                {
                    DateTime current = DateTime.Now;
                    return $"Please vacate computer at {current.AddHours(2)}";
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