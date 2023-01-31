namespace ConsoleGame
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
        public string? Name { get; set; }
        public int Value { get; set; }
        public int Weight { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Speed { get; set; }

        // Make it specific for armor and weapons
        public int AttackSpeed { get; set; }
        public int AttackRange { get; set; }
        public int AttackCooldown { get; set; }
        public int AttackDamage { get; set; }
    }
}