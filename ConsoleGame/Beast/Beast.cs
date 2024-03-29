﻿using ConsoleGame.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Beast
{
    public class Beast
    {

        // Beast registry, listing all beasts by their registry ID.
        private static Dictionary<string, Beast> _beastsByRegistryID;
        public static Dictionary<string, Beast> Bestiary { get { return _beastsByRegistryID; } }

        // List of all capacities that Beast instances can access
        private static List<Capacity> _capacityList = new List<Capacity>();
        public static List<Capacity> CapacityList { get { return _capacityList; } }



        // public static CapacityOfBeast { get { return _capacityOfBeast } }

        // Beast type stats.
        public string Name { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int MaxHealth { get; private set; }
        public int MaxMana { get; private set; }
        public int ExperienceToEvolve { get; private set; }
        public int ExperienceDropped { get; private set; }
        public Capacity[] Capacities { get; private set; }

        // Beast registration.

        static Beast()
        {
            _beastsByRegistryID = new Dictionary<string, Beast>();



            //  ---- Create Capacities ----
            //                                   Name | Damage | Defense | Heal | ManaCost | Cooldown 
            Capacity BITE =         new Capacity("Bite", 7, 0, 0, 0, 0, 0, 0);
            Capacity SCRATCH =      new Capacity("Scratch", 5, 0, 0, 0, 0, 0, 0);
            Capacity HEAL =         new Capacity("Heal", 0, 0, 10, 3, 0, 0, 0);
            Capacity FIREBALL =     new Capacity("Fireball", 10, 0, 0, 5, 0, 0, 0);
            // Capacity ICEBALL = new Capacity("Iceball", 10, 0, 0, 10, 0, 0, 0);
            // Capacity THUNDERBALL = new Capacity("Thunderball", 10, 0, 0, 10, 0, 0, 0);
            Capacity JUMP =         new Capacity("Jump", 2, 0, 0, 0, 0, 0, 0);
            Capacity ARMOR_UP =     new Capacity("Armor Up", 0, 2, 0, 2, 0, 0, 0);
            Capacity ARMOR_DOWN =   new Capacity("Armor Down", 0, -2, 0, 2, 0, 0, 0);

            // skip next turn (cooldown all capactities +1)
            Capacity STUN = new Capacity("Stun", 0, 0, 0, 0, 0, 0, 0);

            //  ---- Create Beasts ----
            //                            Name | Attack | Defense | ActualHealth | Maxhealth | Cooldown

            // create list of capacities for each beast
            Capacity[] leggedCapacity = { BITE, SCRATCH, HEAL };

            Capacity[] ambushCapacity = { BITE, SCRATCH, ARMOR_UP };

            Capacity[] papiermachetteCapacity = { BITE, SCRATCH, ARMOR_UP, FIREBALL };

            registerBeast("leggedthing", new Beast("Truc à Pats", 20, 10, 3, 4, 30, 20, leggedCapacity));
            registerBeast("ambush", new Beast("Embuisscade", 15, 10, 0, 6, 20, 20, ambushCapacity));
            registerBeast("papiermachette", new Beast("Origamonstre", 10, 10, 0, 6, 20, 20, papiermachetteCapacity));


            //  ---- Add Capacities to Capacity Lists ----
            _capacityList.Add(BITE);
            _capacityList.Add(SCRATCH);
            _capacityList.Add(HEAL);
            _capacityList.Add(FIREBALL);
            // _capacityList.Add(ICEBALL);
            // _capacityList.Add(THUNDERBALL);
            _capacityList.Add(JUMP);
            _capacityList.Add(ARMOR_UP);
            _capacityList.Add(ARMOR_DOWN);
            // _capacityList.Add(STUN);


            // Add Capacities to Beast
            // GetBeastByID("leggedthing").CapacityList.Add(BITE);

        }

        private static void registerBeast(string registryID, Beast beast)
        {
            _beastsByRegistryID.Add(registryID, beast);
            beast.RegistryID = registryID;
        }

        public static Beast? GetBeastByID(string registryID)
        {
            return _beastsByRegistryID[registryID];
        }
        public string? RegistryID { get; private set; }

        Beast(string name, int attack, int maxHealth, int defense, int maxMana, int exp, int expDropped, Capacity[] capacities)
        {
            RegistryID = null;
            Name = name;
            Attack = attack;
            MaxHealth = maxHealth;
            Defense = defense;
            MaxMana = maxMana;
            ExperienceToEvolve = exp;
            ExperienceDropped = expDropped;
            this.Capacities = capacities;
        }

        public class Capacity
        {
            public string Name { get; private set; }
            public int Damage { get; private set; }
            public int Defense { get; private set; }
            public int Heal { get; private set; }
            public int ManaCost { get; private set; }
            public int Cooldown { get; private set; }

            public Capacity(string name, int damage, int defense, int heal, int manaCost, int speed, int cost, int cooldown)
            {
                Name = name;
                Damage = damage;
                Defense = defense;
                Heal = heal;
                ManaCost = manaCost;
                Cooldown = cooldown;
            }

            // create UseCapacity method
            public bool UseCapacity(BeastItem launcher, BeastItem target)
            {
                if (launcher.Mana >= ManaCost)
                {
                    launcher.Mana -= ManaCost;
                    launcher.Health += Heal;
                    if(launcher.Health > launcher.GetMaxHealth()) {launcher.Health = launcher.GetMaxHealth();}

                    launcher.Defense += Defense;
                    
                    if(launcher.Defense <= 0) {launcher.Defense = 0;} // avoiding Defense to be negative
                    if(target.Defense <= 0) {target.Defense = 0;} // avoiding Defense to be negative

                    if (target.Defense >= Damage && Damage > 0)
                    {
                        target.Health -= 1;
                    }
                    else if (Damage > 0)
                    {
                        target.Health -= (Damage - target.Defense);
                    }
                    // launcher.Attack += Damage;
                    // launcher.Cooldown += Cooldown;
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }
    }
}
