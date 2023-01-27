﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleGame.State
{
    using SpriteList = List<Tuple<Sprite, int, int>>;
    internal class StateFreeRoam : IState
    {
        public static StateFreeRoam Instance { get; }

        static StateFreeRoam()
        {
            Instance = new StateFreeRoam();
        }
        private SpriteList? _list;
        private string[]? _stringArray;

        public void Enter()
        {

            Map.LoadMap(out _list, out _stringArray);
        }

        public void Leave()
        {

        }

        public IState? Update()
        {
            return null;
        }

        public void Render()
        {
            foreach (var item in _stringArray)
            {
                string s = "";
                foreach (string s0 in _stringArray) s += s0;
                Program.RenderManager.RenderImage(0, 0, _stringArray[0].Length, _stringArray.Length, s);
            }
            foreach (var item in _list)
            {
                Program.RenderManager.RenderSprite(item.Item2, item.Item3, item.Item1);
            }
            Sprite player = Sprite.GetSprite("WARRIOR");
            Sprite tree = Sprite.GetSprite("Tree");
            Program.RenderManager.RenderSprite(Program.RenderManager.CameraPosX, Program.RenderManager.CameraPosY, player);
        }

        public void KeyPress(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    // collision detection

                    
                        Program.RenderManager.CameraPosY--;
                    
                    break;
                case ConsoleKey.DownArrow:
                    // if (!)
                    // {

                    // }
                    Program.RenderManager.CameraPosY++;
                    break;
                case ConsoleKey.LeftArrow:
                    // if (!)
                    // {

                    // }
                    Program.RenderManager.CameraPosX--;
                    break;
                case ConsoleKey.RightArrow:
                    // if (true)
                    // {

                    // }
                    Program.RenderManager.CameraPosX++;
                    break;

                case ConsoleKey.E:
                    Program.OpenScene(StateInventory.Instance);
                    break;
            }
        }
    }
}
