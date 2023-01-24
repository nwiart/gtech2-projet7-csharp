using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.State
{
	internal class StateFreeRoam : IState
	{
		public void Enter()
		{
			
		}

		public void Leave()
		{
			
		}

		public IState? Update()
		{
			return null;
		}

		public void Render()
		{
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
			Program.RenderManager.RenderImage(0, 0, 100, 100, CreateMap());

			Sprite player = Sprite.GetSprite("WARRIOR");
			Sprite tree = Sprite.GetSprite("Tree");
			Program.RenderManager.RenderSprite(0, -8, tree);
			Program.RenderManager.RenderSprite(4, -8, tree);
			Program.RenderManager.RenderSprite(8, -8, tree);
			Program.RenderManager.RenderSprite(Program.RenderManager.CameraPosX, Program.RenderManager.CameraPosY, player);
		}

		public void KeyPress(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.UpArrow:
					Program.RenderManager.CameraPosY--;
					break;
				case ConsoleKey.DownArrow:
					Program.RenderManager.CameraPosY++;
					break;
				case ConsoleKey.LeftArrow:
					Program.RenderManager.CameraPosX--;
					break;
				case ConsoleKey.RightArrow:
					Program.RenderManager.CameraPosX++;
					break;
			}
		}
	}
}
