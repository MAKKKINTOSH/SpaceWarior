using System;
using System.Linq;

namespace SpaceWarriors
{
    class GunBullet
    {
        private int x;
        private int y;

        public int damage = Config.enemiesDamage;
        public int speed = Config.enemiesBulletSpeed;

        private int speedCounter = 0;

        public GunBullet(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsHit()
        {
            if (y >= SpaceShip.y && y <= SpaceShip.y + 7 && x == SpaceShip.x + 6)
            {
                Console.SetCursorPosition(x +1, y);
                Console.Write(' ');

                return true;
            }
            return false;
        }
        private void Print()
        {
            Console.SetCursorPosition(x + 1, y);
            Console.Write(' ');
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = (ConsoleColor)Config.enemiesBulletColor;
            Console.Write("*");
        }
        public bool Fly()
        {
            if (x == 1)
            {
                Console.SetCursorPosition(x + 1, y);
                Console.Write(' ');
                return false;
            }
            if (speedCounter <= 0)
            {
                speedCounter = Config.speedCounter;
                Print();
                x--;
            }
            else speedCounter -= speed;
            return true;
        }
        public void Delete()
        {
            Console.SetCursorPosition(x + 1, y);
            Console.Write(' ');
        }
    }
    class BulletsArray
    {
        private static int quantityOfBullets = 0;
        public static GunBullet[] bullets = new GunBullet[20];

        public static void Append(int x, int y)
        {

            bullets[quantityOfBullets] = new GunBullet(x, y);
            quantityOfBullets++;
        }
        public static void Delete(GunBullet bullet)
        {
            bullets = bullets.Where(i => i != bullet).ToArray();
            quantityOfBullets--;
            Array.Resize(ref bullets, 20);
        }
        public static void CheckBullets()
        {
            foreach (var item in bullets.Where(i => i != null))
            {
                if (item.IsHit())
                {
                    SpaceShip.hp--;
                    HUD.UpdateHP();
                    Delete(item);
                }
                if (!item.Fly())
                {
                    item.Delete();
                    Delete(item);
                }
            }
        }
    }
    class Gun
    {

        private int fireRate = Config.enemiesFireRate;

        private int fireRateCounter = 0;

        public void Shot(int x, int y)
        {
            if (fireRateCounter <= 0)
            {
                fireRateCounter = Config.speedCounter;

                BulletsArray.Append(x, y);
            }
            else fireRateCounter -= fireRate;
        }
    }
    class SmallEnemy
    {
        public int x = Config.enemy_x;
        public int y;

        private Random random = new Random();

        public int hp = Config.enemiesHP;
        private int speed = Config.enemiesSpeed;
        private ConsoleColor color = (ConsoleColor)Config.enemiesColor;

        private Gun gun = new Gun();

        private int speedCounter = 0;

        public SmallEnemy()
        {
            y = random.Next(6, 45);
        }

        public bool IsAlive()
        {
            if (!(hp > 0 && x > 1))
            {
                Console.SetCursorPosition(x, y);

                Console.Write("       "); Console.SetCursorPosition(x, y + 1);
                Console.Write("        "); Console.SetCursorPosition(x, y + 2);
                Console.Write("         "); Console.SetCursorPosition(x, y + 3);
                Console.Write("        "); Console.SetCursorPosition(x, y + 4);
                Console.Write("       ");

                if (hp <= 0)
                {
                    Counter.Increment();
                }

                return false;
            }
            return true;
        }

        private void LastShipPositionClear()
        {
            Console.SetCursorPosition(x + 1, y);

            Console.Write("       "); Console.SetCursorPosition(x, y + 1);
            Console.Write("        "); Console.SetCursorPosition(x, y + 2);
            Console.Write("         "); Console.SetCursorPosition(x, y + 3);
            Console.Write("        "); Console.SetCursorPosition(x, y + 4);
            Console.Write("       ");
        }

        public void Print()
        {

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;

            Console.Write("    |\\"); Console.SetCursorPosition(x, y + 1);
            Console.Write("  /--\\\\"); Console.SetCursorPosition(x, y + 2);
            Console.Write("=()##|#|"); Console.SetCursorPosition(x, y + 3);
            Console.Write("  \\--//"); Console.SetCursorPosition(x, y + 4);
            Console.Write("    |/"); 
        }

        public void Move()
        {
            if (speedCounter <= 0)
            {
                speedCounter = Config.speedCounter;
                x--;
                LastShipPositionClear();
                Print();
            }
            else speedCounter -= speed;

            gun.Shot(x - 2, y + 2);
        }
    }

    class AliensArmy
    {
        private static int spawnRate = Config.EnemiesSpawnRate;

        public static SmallEnemy[] enemiesArray = new SmallEnemy[10];

        private static int spawnCounter = 0;
        private static int quantityOfEnemies = 0;

        public static void set()
        {
            if (spawnCounter <= 0)
            {
                spawnCounter = Config.speedCounter;

                enemiesArray[quantityOfEnemies] = new SmallEnemy();
                quantityOfEnemies++;
            }
            else spawnCounter -= spawnRate;

            foreach (var item in enemiesArray.Where(i => i != null).ToArray())
            {
                if (!item.IsAlive())
                {
                    enemiesArray = enemiesArray.Where(i => i !=item).ToArray();
                    quantityOfEnemies--;

                    Array.Resize(ref enemiesArray, 10);

                    HUD.UpdateKills();
                }
                item.Move();
            }
        }
    }
}
