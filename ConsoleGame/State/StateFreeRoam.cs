using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleGame.State
{
    using SpriteList = List<Tuple<Sprite, int, int>>;
    internal class StateFreeRoam : IState
    {

        Level _level = new Level();
        public static StateFreeRoam Instance { get; }

        static StateFreeRoam()
        {
            Instance = new StateFreeRoam();
        }

        public void Enter()
        {
            _level.GetMap().Load();
        }

        public void Leave()
        {

        }

        public IState? Update()
        {
            _level.GetPlayer().Update();
            return null;
        }

        public void Render()
        {


            for (int y = 0; y < _level.GetMap().GetImageMap().Count(); y++)
            {
                string row = _level.GetMap().GetImageMap()[y];
                Program.RenderManager.RenderImage(0, y, row.Length, 1, row);
            }
            foreach (var item in _level.GetMap().GetSprites())
            {
                Program.RenderManager.RenderSprite(item.Item2, item.Item3, item.Item1);
            }
            Sprite player = Sprite.GetSprite("WARRIOR");
            Sprite tree = Sprite.GetSprite("Tree");
            Program.RenderManager.RenderSprite(Program.RenderManager.CameraPosX, Program.RenderManager.CameraPosY - 2, player);
        }

        public void KeyPress(ConsoleKey key)
        {
            _level.GetPlayer().KeyPress(key);
            // Spawn Enemy when player moves
            _level.SpawnEnemy();
            switch (key)
            {
                case ConsoleKey.E:
                    Program.OpenScene(StateInventory.Instance);
                    break;
            }
        }
    }
}
