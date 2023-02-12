using System;

namespace SpaceWarriors
{
    internal class Config
    {
        public static int speedCounter = 3000000;

        public static int player_x = 2;
        public static int player_y = 24;

        public static int playerSpeed = 100;
        public static int playerDamage = 1;
        public static int playerHP = 3;
        public static int playerFireRate = 10;
        public static int playerBulletSpeed = 400;

        public static int playerColor = 12;
        public static int playerBulletColor = 14;

        public static int enemy_x = 199;
        /*public static int enemy_y = 0;*/ 
        //Using random spawn in runtime

        public static int enemiesSpeed = 200;
        public static int enemiesDamage = 1;
        public static int enemiesHP = 3;
        public static int enemiesFireRate = 2;
        public static int enemiesBulletSpeed = 300;

        public static int enemiesColor = 10;
        public static int enemiesBulletColor = 4;

        public static int EnemiesSpawnRate = 2;
    }
}
