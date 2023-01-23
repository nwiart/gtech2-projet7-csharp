using System;

namespace ConsoleGame
{
	class Program
	{
		static void Main(string[] args)
		{
            ConsoleKey lastKey = 0;

            // Game loop.
            while (true)
			{
				// Key input.
                while (Console.KeyAvailable)
				{
					lastKey = Console.ReadKey().Key;
					if (lastKey == ConsoleKey.A)
					{
						return;
					}
				}

				// Render.
                Console.CursorVisible = false;

                Console.SetCursorPosition(0, 0);
				for (int i = 0; i < Console.WindowWidth; ++i)
				{
					Console.Write("#");
				}

                Console.SetCursorPosition(10, 10);
				Console.Write("You pressed " + lastKey.ToString() + "!");

                Console.SetCursorPosition(0, Console.WindowHeight - 1);
				for (int i = 0; i < Console.WindowWidth - 1; ++i)
				{
					Console.Write("#");
				}
			}
		}
	}
}
