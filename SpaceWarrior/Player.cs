using System;
using SpaceWarriors;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SpaceWarriors
{
    class Laser
    {
        private int x;
        private int y;

        public int damage;
        public int speed;

        private int speedCounter = 0;

        private bool returnValue;

        public Laser(int x, int y, int damage, int speed)
        {
            this.x = x;
            this.y = y;
            this.damage = damage;
            this.speed = speed;
        }

        public bool IsHit()
        {
            foreach (var enemy in AliensArmy.enemiesArray.Where(i => i != null))
            {
                if (x == enemy.x && y>=enemy.y && y < enemy.y + 5)
                {
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write(' ');
                    enemy.hp--;
                    returnValue = true;
                }
            }
            returnValue = false;
            return returnValue;
        }
        private void Print()
        {
            Console.SetCursorPosition(x - 1, y);
            Console.Write(' ');
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = (ConsoleColor)14;
            Console.Write(">");
        }

        public bool Fly()
        {
            if (x == 208)
            {
                Console.SetCursorPosition(x - 1, y);
                Console.Write(' ');
                return false;
            }
            if (speedCounter <= 0)
            {
                speedCounter = Config.speedCounter;
                Print();
                x++;
            }
            else speedCounter -= speed;
            return true;
        }
    }
    class LaserGun
    {
        private int fireRate = Config.playerFireRate;

        private int damage = Config.playerDamage;
        private int bulletSpeed = Config.playerBulletSpeed;

        private Queue<Laser> bulletsQueue = new Queue<Laser>();
        private Laser[] bulletsArray;

        private int fireRateCounter = 0;

        public void Shot(int x, int y)
        {

            if (fireRateCounter <= 0)
            {
                fireRateCounter = Config.speedCounter;

                bulletsQueue.Enqueue(new Laser(x, y, damage, bulletSpeed));
            }
            else fireRateCounter -= fireRate;

            bulletsArray = bulletsQueue.ToArray();

            foreach (var item in bulletsArray)
            {
                if (!item.Fly())
                {
                    bulletsQueue.Dequeue();
                }
                if (item.IsHit())
                {
                    bulletsQueue.Dequeue();
                }
            }
        }
    }
    class SpaceShip
    {

        public static int x = Config.player_x;
        public static int y = Config.player_y;

        public static int hp = Config.playerHP;
        private static ConsoleColor color = (ConsoleColor)Config.playerColor;

        private static LaserGun gun = new LaserGun();

        private static void LastShipPositionClear(int last_x, int last_y)
        {
            Console.SetCursorPosition(last_x, last_y);
            Console.Write("     "); Console.SetCursorPosition(last_x, last_y + 1);
            Console.Write("     "); Console.SetCursorPosition(last_x, last_y + 2);
            Console.Write("       "); Console.SetCursorPosition(last_x, last_y + 3);
            Console.Write("          "); Console.SetCursorPosition(last_x, last_y + 4);
            Console.Write("       "); Console.SetCursorPosition(last_x, last_y + 5);
            Console.Write("     "); Console.SetCursorPosition(last_x, last_y + 6);
            Console.Write("     ");
        }

        public static void Print()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);

            Console.Write("   /|"); Console.SetCursorPosition(x, y + 1);
            Console.Write("/\\/#|"); Console.SetCursorPosition(x, y + 2);
            Console.Write("\\/#/##\\"); Console.SetCursorPosition(x, y + 3);
            Console.Write(" |#####()>"); Console.SetCursorPosition(x, y + 4);
            Console.Write("/\\#\\##/"); Console.SetCursorPosition(x, y + 5);
            Console.Write("\\/\\#|"); Console.SetCursorPosition(x, y + 6);
            Console.Write("   \\|"); 
        }

        public static void MoveRight()
        {
            if(x<209 - 13)
            {
                x+=2;
                LastShipPositionClear(x - 2, y);
                Print();
            }
        }
        public static void MoveLeft() 
        {
            if (x>3)
            {
                x-=2;
                LastShipPositionClear(x + 2, y);
                Print();
            }
        }
        public static void MoveUp() 
        { 
            if (y>5)
            {
                y--;
                LastShipPositionClear(x, y + 1);
                Print();
            }
        }
        public static void MoveDown() 
        {
            if (y<50 - 7)
            {
                y++;
                LastShipPositionClear(x, y - 1);
                Print();
            }
        }

        public static void Shot()
        {
            gun.Shot(x + 11, y + 3);
        }
    }
}
