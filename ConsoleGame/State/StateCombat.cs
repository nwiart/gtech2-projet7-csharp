﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ConsoleGame.State
{
    using ConsoleGame.Beast;
    internal class StateCombat : IState
    {
        public static StateCombat Instance { get; }
        Beast? _playerBeast;
        Beast? _enemyBeast;


        static StateCombat()
        {
            Instance = new StateCombat();
        }

        public void Enter()
        {
            Random randomNumber = new Random(); 
            int x = randomNumber.Next(0, Beast.Bestiary.Count);
            _enemyBeast = Beast.Bestiary.ElementAt(x).Value; /*?? throw new NullReferenceException(); */
            // Needs to be assigned to a varaiable to be used in the render method


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
            RenderManager? rm = Program.RenderManager;
            rm.Transform = false;

            rm.RenderString(2, 1, "You have encountered a wild " + _enemyBeast.Name + " !");

            // ---------- Player Part ----------
            rm.RenderBox(6, 8, 28, 14);
            rm.RenderString(6 + 1, 5, "Trucmuche");

            // ---------- Enemy Part ----------
            rm.RenderBox(Console.WindowWidth - 28 - 6, 8, 28, 14);
            rm.RenderString(Console.WindowWidth - 6 - 1, 5, _enemyBeast.Name, RenderManager.TextAlign.RIGHT);


            // ---------- HP Bars ----------
            rm.CurrentColor = 0x7a;
            // Player HP Bar
            rm.RenderHLine(6 + 1, 6, 26, '█');

            // Enemy HP Bar
            rm.RenderHLine(Console.WindowWidth - 28 - 6 + 1, 6, 26, '█');
            rm.RenderString(Console.WindowWidth - 6 + 1, 6, _enemyBeast.ActualHealth.ToString() );

            rm.CurrentColor = 0x0f;
            rm.RenderBox(6 + 30, 4, 28, 20);

            rm.Transform = true;
        }

        public void KeyPress(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.A:
                    _enemyBeast.capacityOfBeast[0].UseCapacity(_enemyBeast, _enemyBeast);
                    Console.WriteLine(_enemyBeast.capacityOfBeast[0].Name + " used");
                    Console.WriteLine(_enemyBeast.ActualHealth);
                    break;
            }
        }
    }

}