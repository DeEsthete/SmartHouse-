using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseLibrary
{
    public class ConsoleMenu
    {
        private string[] menuItems;
        private int counter = 0;
        public ConsoleMenu(string[] menuItems)
        {
            this.menuItems = menuItems;
        }

        public int PrintMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (counter == i)
                    {
                        //Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.Green;
                        var width = Console.WindowWidth;
                        var padding = width / 2 + menuItems[i].Length / 2;
                        Console.WriteLine("{0," + padding + "}", menuItems[i]);
                    

                    //Console.WriteLine("{50}", menuItems[i]);
                        //Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else { 
                        var width = Console.WindowWidth;
                    var padding = width / 2 + menuItems[i].Length / 2;
                    Console.WriteLine("{0," + padding + "}", menuItems[i]);
                        // Console.WriteLine(menuItems[i]);
                    }

                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    counter--;
                    if (counter == -1) counter = menuItems.Length - 1;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    counter++;
                    if (counter == menuItems.Length) counter = 0;
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return counter;
        }
    }
}
