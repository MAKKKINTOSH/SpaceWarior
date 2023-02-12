using System;
using SpaceWarriors;
using System.Windows.Input;
using System.Threading;
using System.Diagnostics;

namespace SpaceWarriors
{
    class Window
    {
        public static void PrintBorder(int color)
        {
            Console.Clear();
            Console.ForegroundColor = (ConsoleColor) color;
            Console.SetCursorPosition(0, 0);

            try
            {
                for (int i = 0; i < 209; i++)
                {
                    Console.Write('-');
                }

                Console.WriteLine();

                for (int i = 1; i < 50; i++)
                {
                    Console.Write('|');
                    Console.SetCursorPosition(208, i);
                    Console.Write('|');
                    Console.WriteLine();
                }
                for (int i = 0; i < 209; i++)
                {
                    Console.Write('-');
                }
            }
            catch (Exception)
            {
                Console.SetCursorPosition(0, 0);
                Console.Clear();
                Console.WriteLine("Сказано же было в полный экран консоль сделать!");
                Console.ReadKey();
            }
            
        }
    }
    class LoseScreen
    {
        public static void Set()
        {
            Window.PrintBorder(4);

            Console.SetCursorPosition(60, 20);
            Console.ForegroundColor= (ConsoleColor)4;

            Console.WriteLine("**  **  ******  **  **         **      ******  ******  ******"); Console.SetCursorPosition(60, 20 + 1);
            Console.WriteLine("**  **  **  **  **  **         **      **  **  **      **    "); Console.SetCursorPosition(60, 20 + 2);
            Console.WriteLine("**  **  **  **  **  **         **      **  **  ******  ******"); Console.SetCursorPosition(60, 20 + 3);
            Console.WriteLine("  **    **  **  **  **         **      **  **      **  **    "); Console.SetCursorPosition(60, 20 + 4);
            Console.WriteLine("  **    ******  ******         ******  ******  ******  ******"); 
            
            Console.SetCursorPosition(80, 20 + 8);
            Console.WriteLine("You make {0} kills", Counter.count) ;
        }
    }
    class Counter
    {
        public static int count = 0;

        public static void Increment()
        {
            count++;
        }
    }
    class HUD
    {
        private static int x1 = 2;
        private static int x2 = 100;
        private static int y = 2;
        public static void Print()
        {
            Console.SetCursorPosition(1, 4);
            for (int i = 0; i < 208; i++)
            {
                Console.Write("-");
            }

            Console.SetCursorPosition(x1, y);
            Console.ForegroundColor = (ConsoleColor)10;
            Console.Write("HP: {0}/{0}", Config.playerHP);
            Console.SetCursorPosition(x2, y);
            Console.Write("KILLS: 0");
        }
        public static void UpdateHP()
        {
            Console.ForegroundColor = (ConsoleColor)10;
            Console.SetCursorPosition(x1 + 3, y);
            Console.Write(" {0}/{1}", SpaceShip.hp, Config.playerHP);
        }
        public static void UpdateKills()
        {
            Console.ForegroundColor = (ConsoleColor)10;
            Console.SetCursorPosition(x2 + 7, y);
            Console.Write("{0}", Counter.count);
        }
    }
    class Game
    {
        private static int speedCounter = 0;

        public static void OnStart()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(20, 8);
            Console.Write("СДЕЛАЙТЕ КОНСОЛЬ ВО ВЕСЬ ЭКРАН");

            /*Thread.Sleep(3000);*/

            Console.SetCursorPosition(20, 10);
            Console.Write("НАЖМИТЕ ENTER");

            while (Console.ReadKey().Key != ConsoleKey.Enter) { }

            Console.Clear();

            Window.PrintBorder(11);
            HUD.Print();

        }
        public static void KeyInput()
        {
            if (speedCounter <= 0)
            {
                if (Keyboard.IsKeyDown(Key.W))
                {
                    SpaceShip.MoveUp();
                }
                if (Keyboard.IsKeyDown(Key.A))
                {
                    SpaceShip.MoveLeft();
                }
                if (Keyboard.IsKeyDown(Key.S))
                {
                    SpaceShip.MoveDown();
                }
                if (Keyboard.IsKeyDown(Key.D))
                {
                    SpaceShip.MoveRight();
                }
                speedCounter = Config.speedCounter;
            }
            else 
            {
                speedCounter -= Config.playerSpeed;
            }
            
            
        }
        [STAThread]
        public static void Main(string[] args)
        {

            ConsoleKey key = new ConsoleKey();

            OnStart();

            SpaceShip.Print();

            while (SpaceShip.hp>0)
            {
                AliensArmy.set();

                KeyInput();

                SpaceShip.Shot();

                BulletsArray.CheckBullets();
            }

            LoseScreen.Set();

            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Enter);
        }
    }
}