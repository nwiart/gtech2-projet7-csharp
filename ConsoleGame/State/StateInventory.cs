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
			_selectingCategory = true;
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
			
			// Item categories.
			rm.CurrentColor = (short) (_selectingCategory ? 0x06 : 0x0F);
			rm.RenderBox(4, 2, 28, 9);

			int itemCategoriesX = 6, itemCategoriesY = 3;
			rm.RenderString(itemCategoriesX, itemCategoriesY + _selectedCategory * 2, ">");
			for (int i = 0; i < _categories.Length; ++i)
			{
				rm.RenderString(
					itemCategoriesX + 2,
					itemCategoriesY + i * 2,
					_categories[i]);
			}

			// Items view panel.
			if (!_selectingCategory)
			{
				rm.CurrentColor = (short)(0x06);
				rm.RenderBox(32, 2, 54, 26);
				rm.CurrentColor = 0x0F;

				rm.RenderBox(29, 5, 6, 3);
				rm.RenderString(31, 6, "->");

				// Item boxes test.
				rm.RenderBox(35, 3, 12, 6);
				rm.RenderBox(47, 3, 12, 6);
				rm.RenderBox(59, 3, 12, 6);
				rm.RenderBox(71, 3, 12, 6);
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

				// Navigation.
				case ConsoleKey.UpArrow:
					if (_selectingCategory)
					{
						if (_selectedCategory > 0) _selectedCategory--;
					}
					break;
				case ConsoleKey.DownArrow:
					if (_selectingCategory)
					{
						if (_selectedCategory < _categories.Length - 1) _selectedCategory++;
					}
					break;
				case ConsoleKey.RightArrow:
					_selectingCategory = false;
					break;

				case ConsoleKey.LeftArrow:
					_selectingCategory = true;
					break;
			}
		}
	}
}
