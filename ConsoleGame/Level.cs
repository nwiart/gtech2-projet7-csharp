using ConsoleGame.State;
using System;
using System.Runtime.InteropServices;

namespace ConsoleGame
{
    class Level
    {
        Map _mapLevel = new Map();
        public Level()
        {
        }

        public void Update()
        {

        }

        public Map GetMap()
        {
            return _mapLevel;
        }

        public static bool IsSpawnable(char tileType)
        {
            switch (tileType)
            {
                case '#':
                    return true;

                default:
                    return false;
            }
        }

        public bool IsSpawnable(int x, int y)
        {
            return IsSpawnable(_mapLevel.GetImageMap()[y][x * 2]);
        }

        public void SpawnEnemy()
        {
            if (IsSpawnable(Program.RenderManager.CameraPosX, Program.RenderManager.CameraPosY))
            {
                Random randomNumber = new Random();
                int x = randomNumber.Next(1, 10);
                if (x < 5)
                {
                    Console.WriteLine("Enemy spawned");
                }
            }

        }


    }
}
