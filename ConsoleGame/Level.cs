using ConsoleGame.State;
using System;
using System.Runtime.InteropServices;

namespace ConsoleGame
{
    public class Level
    {
        Map _mapLevel = new Map();
        Player _player = new Player();

        public Level()
        {
            _player.SetLevel(this);
        }

        public void Update()
        {

        }

        public Map GetMap()
        {
            return _mapLevel;
        }

        public Player GetPlayer()
        {
            return _player;
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
            return IsSpawnable(_mapLevel.GetTileAt(x, y));
        }

        public void SpawnEnemy()
        {
            if (IsSpawnable(Program.RenderManager.CameraPosX, Program.RenderManager.CameraPosY))
            {
                Random randomNumber = new Random();
                // 1% chance of spawning an enemy
                int x = randomNumber.Next(1, 1000);
                if (x < 10)
                {
                    Program.OpenScene(StateCombat.Instance);
                }
            }
        }
    }
}
