using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Inventory
{
    public class BeastItem
    {
        public delegate void OnDieCallback(BeastItem bi);
        public event OnDieCallback OnDie;

        public enum ValuesPerLevel
        {
            Attack = 2,
            Health = 5,
            Defense = 1,
            Mana = 2,
        }
        public Beast.Beast Beast { get; }
        public int Attack { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Defense { get; set; }
        public int Experience { get; set; }

        public BeastItem(Beast.Beast b, int level)
        {
            Beast = b;
            Level = level;
            if (level == 1)
            {
                // Set to basic species values.
                Attack = b.Attack;
                Health = b.MaxHealth;
                Mana = b.MaxMana;
                Defense = b.Defense;
            }
            else if (level > 1)
            {
                // Attack = (int)(ValuesAtLevel1.Attack + ((int)ValuesPerLevel.Attack * (level - 1)));
                // Health = (int)(ValuesAtLevel1.MaxHealth + ((int)ValuesPerLevel.Health * (level - 1)));
                // Defense = (int)(ValuesAtLevel1.Defense + ((int)ValuesPerLevel.Defense * (level - 1)));

                // Species base values + growth values

                Attack = b.Attack + ((int)ValuesPerLevel.Attack * (level - 1));
                Health = b.MaxHealth + ((int)ValuesPerLevel.Health * (level - 1));
                Mana = b.MaxMana + ((int)ValuesPerLevel.Mana * (level - 1));
                Defense = b.Defense + ((int)ValuesPerLevel.Defense * (level - 1));
            }

        }
        public int GetMaxHealth()
        {
            return (Beast.MaxHealth + ((int)ValuesPerLevel.Health * (Level - 1)));
        }
        public int GetMaxMana()
        {
            return (Beast.MaxMana + ((int)ValuesPerLevel.Mana * (Level - 1)));
        }
        public void GainExperience(int exp)
        {
            Experience += exp;
            CheckEvolve();
        }
        public bool CheckEvolve()
        {
            if (Experience >= Beast.ExperienceToEvolve)
            {
                Level++;
                Attack += (int)ValuesPerLevel.Attack;
                // Health part

                // Health += (int)ValuesPerLevel.Health;
                Health = GetMaxHealth();
                // Mana part

                // Mana += (int)ValuesPerLevel.Mana;
                Mana = GetMaxMana();
                Defense += (int)ValuesPerLevel.Defense;

                Experience -= Beast.ExperienceToEvolve;
                return true;
            }
            else return false;
        }

        // make a Die fonction

    }

}
