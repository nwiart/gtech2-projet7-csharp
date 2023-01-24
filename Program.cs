using System;
using System.Runtime.InteropServices;

namespace ConsoleGame
{
	class Program
	{
		static void Main(string[] args)
		{
			ConsoleKey lastKey = 0;

			RenderManager renderManager = new RenderManager(Console.WindowWidth, Console.WindowHeight);

			Sprite.LoadSprites();

			Win32.SetConsoleTitle("ConsoleGame");

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
				renderManager.Clear();

				// renderManager.RenderHLine(0, 0, Console.WindowWidth, '#');
				// renderManager.RenderHLine(0, Console.WindowHeight - 1, Console.WindowWidth, '#');

				string CreateMap()
				{
					string map = "";
					for (int i = 0; i < 100; i++)
					{
						for (int j = 0; j < 100; j++)
						{
							if (i == 5 && j == 5)
							{
								map += "@";
							}
							else if (i == 10 && j == 10)
							{
								map += "!";
							}
							else if (i > 20 && i < 30 && j > 20 && j < 30)
							{
								map += "+";
							}
							else if (i > 30 && i < 40 && j > 30 && j < 40)
							{
								map += "?";
							}
							else if (i > 40 && i < 50 && j > 40 && j < 50)
							{
								map += "~";
							}
							else if (i > 50 && i < 60 && j > 50 && j < 60)
							{
								map += "X";
							}
							else
							{
								map += "*";
							}
						}
					}
					return map;
				}
				renderManager.RenderImage(0, 0, 100, 100, CreateMap());

				string tree =
					"  /\\  " +
					" /  \\ " +
					" /  \\ " +
					"/    \\" +
					"  ||  ";

				Sprite player = Sprite.GetSprite("WARRIOR");
				renderManager.RenderImage(0, 0, 6, 5, tree);
				renderManager.RenderSprite(renderManager.CameraPosX, renderManager.CameraPosY, player);

				renderManager.RenderString(2, Console.WindowHeight - 2, "© ConsoleGame 2023");

				renderManager.SwapBuffers();

				Thread.Sleep(1);
			}
		}
	}
}
