namespace ConsoleGame
{
    public class Player
    {
        public static Player Instance { get; private set; }

        private Level? _level;
        public Inventory.Inventory Inventory { get; private set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Player()
        {
            Instance = this;
            Inventory = new Inventory.Inventory();
        }

        public void SetLevel(Level level)
        {
            _level = level;
        }


        public void Update()
        {
            // Camera Following
            Program.RenderManager.CameraPosX = PosX;
            Program.RenderManager.CameraPosY = PosY;
            
        }


        public void KeyPress(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (!_level.GetMap().IsCollidable(PosX, PosY - 1))
                    {
                        PosY--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (!_level.GetMap().IsCollidable(PosX, PosY + 1))
                    {
                        PosY++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (!_level.GetMap().IsCollidable(PosX - 1, PosY))
                    {
                        PosX--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (!_level.GetMap().IsCollidable(PosX + 1, PosY))
                    {
                        PosX++;
                    }
                    break;
            }
        }
    }
}
