using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Inventory
{
	public class BeastItem
	{
		public Beast.Beast Beast { get; }

		public int Health { get; set; }
		public int Mana { get; set; }

		public int Defense { get; set; }

		public BeastItem(Beast.Beast b)
		{
			Beast = b;
			Health = b.MaxHealth;
			Mana = b.MaxMana;
			Defense = b.Defense;
		}
	}
}
