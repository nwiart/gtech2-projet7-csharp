using ConsoleGame.State;
using System;
using System.Runtime.InteropServices;

namespace ConsoleGame
{
    public class Level
    {
        Map _mapLevel = new Map();
        Player _player;

        public Level()
        {
            _player = Player.Instance;
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

                // 5% chance of spawning an enemy
                
                int x = randomNumber.Next(0, 100);
                if (x < 5)
                {
                    Program.OpenScene(StateCombat.Instance);
                }
            }
        }
    }
}
