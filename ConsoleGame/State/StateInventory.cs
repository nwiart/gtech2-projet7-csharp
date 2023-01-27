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
		private int _selectedCategory = 0;

		private static string[] _categories =
		{
			"Items",
			"Potions",
			"Party",
			"Specials"
		};

		public void Enter()
		{
			// Default UI values.
			_selectedCategory = 0;
			_selectingCategory = false;
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

			// Category & items view panel.
			Program.RenderManager.CurrentColor = (short) (_selectingCategory ? 0x06 : 0x0F);
			RenderBox(4, 2, 24, 26);
			Program.RenderManager.CurrentColor = (short) (_selectingCategory ? 0x0F : 0x06);
			RenderBox(28, 2, 60, 26);
			Program.RenderManager.CurrentColor = 0x0F;

			// Item boxes test.
			RenderBox(29, 3, 12, 6);
			RenderBox(41, 3, 12, 6);

			// Item categories.
			int itemCategoriesX = 6, itemCategoriesY = 3;
			Program.RenderManager.RenderString(itemCategoriesX, itemCategoriesY + _selectedCategory * 2, ">");
			for (int i = 0; i < _categories.Length; ++i)
			{
				Program.RenderManager.RenderString(
					itemCategoriesX + 2,
					itemCategoriesY + i * 2,
					_categories[i]);
			}

			Program.RenderManager.Transform = true;
		}

		public void KeyPress(ConsoleKey key)
		{
			switch (key)
			{
				// E to exit inventory.
				case ConsoleKey.E:
					Program.OpenScene(StateFreeRoam.Instance);
					break;

				case ConsoleKey.Enter:
					_selectingCategory = false;
					break;

				case ConsoleKey.Backspace:
					_selectingCategory = true;
					break;

				case ConsoleKey.UpArrow:
					if (_selectedCategory > 0) _selectedCategory--;
					break;
				case ConsoleKey.DownArrow:
					if (_selectedCategory < _categories.Length - 1) _selectedCategory++;
					break;
			}
		}
	}
}
