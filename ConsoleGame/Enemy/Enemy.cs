using ConsoleGame.State;
using System;
using System.Runtime.InteropServices;
    

namespace ConsoleGame
{
    using System.Text;

    public class Enemy
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public int AttackSpeed { get; set; }
        public int AttackRange { get; set; }
        public int AttackCooldown { get; set; }
        public int AttackCooldownCounter { get; set; }
        public int AttackDamage { get; set; }
        public int AttackDamageCounter { get; set; }

    }

}

