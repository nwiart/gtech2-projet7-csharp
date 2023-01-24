using System;

namespace ConsoleGame
{
	class Program
	{
		static void Main(string[] args)
		{
			ConsoleKey lastKey = 0;

			RenderManager renderManager = new RenderManager(Console.WindowWidth, Console.WindowHeight);

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

					switch (lastKey)
					{
						case ConsoleKey.UpArrow:
							renderManager.CameraPosY--;
							break;
						case ConsoleKey.DownArrow:
							renderManager.CameraPosY++;
							break;
						case ConsoleKey.LeftArrow:
							renderManager.CameraPosX--;
							break;
						case ConsoleKey.RightArrow:
							renderManager.CameraPosX++;
							break;
					}
				}

				// Render.
				renderManager.clear();

				renderManager.renderHLine(0, 0, Console.WindowWidth, '#');
				renderManager.renderHLine(0, Console.WindowHeight - 1, Console.WindowWidth, '#');

				string pl =
					"  @  " +
					" /|\\ " +
					"/ | \\" +
					" / \\ " +
					"/   \\";
				renderManager.renderImage(renderManager.CameraPosX, renderManager.CameraPosY, 5, 5, pl);

				string text = "You pressed " + lastKey.ToString() + "!";
				renderManager.renderString(20, 10, text);

				renderManager.renderString(2, Console.WindowHeight - 2, "ConsoleGame 2023");

				renderManager.swapBuffers();

				Thread.Sleep(1);
			}
		}
	}
}
