using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DobruyVecherYaDispetcher
{
    internal static class ArrowsMenu
    {
        public static int Show(int pos, int minStrelochka, int maxStrelochka)
        {

            ConsoleKeyInfo key;

            do
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, pos);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("->");
                Console.ResetColor();


                key = Console.ReadKey();

                Console.SetCursorPosition(0, pos);
                Console.WriteLine("  ");


                if (key.Key == ConsoleKey.UpArrow && pos != minStrelochka)
                {
                    pos--;
                }

                else if (key.Key == ConsoleKey.DownArrow && pos != maxStrelochka)
                {
                    pos++;
                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    return (int)knopochki.flashback;

                }

                else if (key.Key == ConsoleKey.D)
                {

                    return (int)knopochki.close;
                }

                else if (key.Key == ConsoleKey.Delete)
                {
                    return (int)knopochki.closeEverything;
                }

                if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Вы завершили работу с программой");
                    Console.ResetColor();
                    return -1;
                }

            } while (key.Key != ConsoleKey.Enter);


            Console.Clear();
            return pos;

        }
    }
}