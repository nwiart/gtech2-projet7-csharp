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

		public void Render()
		{
			RenderManager rm = Program.RenderManager;

			rm.Transform = false;

			// Category & items view panel.
			rm.CurrentColor = (short) (_selectingCategory ? 0x06 : 0x0F);
			rm.RenderBox(4, 2, 24, 26);
			rm.CurrentColor = (short) (_selectingCategory ? 0x0F : 0x06);
			rm.RenderBox(28, 2, 60, 26);
			rm.CurrentColor = 0x0F;

			// Item boxes test.
			rm.RenderBox(29, 3, 12, 6);
			rm.RenderBox(41, 3, 12, 6);

			// Item categories.
			int itemCategoriesX = 6, itemCategoriesY = 3;
			rm.RenderString(itemCategoriesX, itemCategoriesY + _selectedCategory * 2, ">");
			for (int i = 0; i < _categories.Length; ++i)
			{
				rm.RenderString(
					itemCategoriesX + 2,
					itemCategoriesY + i * 2,
					_categories[i]);
			}

			rm.Transform = true;
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
