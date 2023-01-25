using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.State
{
	internal class StateInventory : IState
	{
		public static StateInventory Instance { get; }

		static StateInventory()
		{
			Instance = new StateInventory();
		}



		private bool _selectingCategory = true;

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

		public void RenderBox(int posX, int posY, int sizeX, int sizeY)
		{
			// Render borders.
			Program.RenderManager.RenderHLine(posX, posY, sizeX, '─');
			Program.RenderManager.RenderHLine(posX, posY + sizeY - 1, sizeX, '─');
			Program.RenderManager.RenderVLine(posX, posY + 1, sizeY - 2, '│');
			Program.RenderManager.RenderVLine(posX + sizeX - 1, posY + 1, sizeY - 2, '│');

			Program.RenderManager.RenderChar(posX, posY, '┌');
			Program.RenderManager.RenderChar(posX + sizeX - 1, posY, '┐');
			Program.RenderManager.RenderChar(posX, posY + sizeY - 1, '└');
			Program.RenderManager.RenderChar(posX + sizeX - 1, posY + sizeY - 1, '┘');
		}

		public void Render()
		{
			Program.RenderManager.Transform = false;

			Program.RenderManager.CurrentColor = (short) (_selectingCategory ? 0x06 : 0x0F);
			RenderBox(4, 2, 24, 26);
			Program.RenderManager.CurrentColor = (short) (_selectingCategory ? 0x0F : 0x06);
			RenderBox(28, 2, 60, 26);
			Program.RenderManager.CurrentColor = 0x0F;

			RenderBox(29, 3, 12, 6);
			RenderBox(41, 3, 12, 6);

			Program.RenderManager.RenderString(6, 3, ">");
			Program.RenderManager.RenderString(8, 3, "Items");
			Program.RenderManager.RenderString(8, 5, "Potions");
			Program.RenderManager.RenderString(8, 7, "Party");
			Program.RenderManager.RenderString(8, 9, "Specials");

			Program.RenderManager.Transform = true;
		}

		public void KeyPress(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.Enter:
					_selectingCategory = false;
					break;

				case ConsoleKey.Backspace:
					_selectingCategory = true;
					break;
			}
		}
	}
}
