namespace ConsoleGame.Inventory
{
	public class Inventory
	{
		private List<BeastItem> _beasts;
		public IEnumerable<BeastItem> Beasts { get => _beasts; }

		public static readonly int MAX_BEASTS_IN_PARTY = 4;
		private BeastItem?[] _party = new BeastItem?[MAX_BEASTS_IN_PARTY];

		public class InventorySlot
		{
			public Item? Item { get; set; }
			public int Amount { get; set; }
		}

		public Inventory()
		{
			_beasts = new List<BeastItem>();
		}


		public void AddBeast(BeastItem bi)
		{
			_beasts.Add(bi);
		}

		public void AddBeastToParty(int partySlotIndex, int beastSlotIndex)
		{
			BeastItem? previous = _party[partySlotIndex];

			// Move beast to party.
			BeastItem bi = _beasts[beastSlotIndex];
			_beasts.RemoveAt(beastSlotIndex);
			_party[partySlotIndex] = bi;

			// Move previous beast back to inventory (if there was one).
			if (previous != null)
				_beasts.Add(previous);
		}

		public void RemoveBeastFromParty(int index)
		{
			if (index < 0 || index >= MAX_BEASTS_IN_PARTY) throw new IndexOutOfRangeException();
			if (_party[index] != null)
			{
				// Move beast to main inventory.
				_beasts.Add(_party[index]);
				_party[index] = null;
			}
		}
	}


	public class Item
	{
		// Type of Items
		public enum ItemTypes
		{
			Weapon,
			Armor,
			Consumable,
			QuestItem,
			Misc
		}
		public string? Name { get; private set; }
		public int Value { get; private set; }
		public int Weight { get; private set; }
		public int Damage { get; private set; }
		public int Defense { get; private set; }
		public int Health { get; private set; }
		public int Mana { get; private set; }
		public int Speed { get; private set; }

		// Make it specific for armor and weapons
		public int AttackSpeed { get; private set; }
		public int AttackRange { get; private set; }
		public int AttackCooldown { get; private set; }
		public int AttackDamage { get; private set; }
	}
}
