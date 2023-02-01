namespace ConsoleGame.Inventory
{
	public class Inventory
	{
		public class InventorySlot
		{
			public Item? Item { get; set; }
			public int Amount { get; set; }
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
