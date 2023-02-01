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



		private UIList _categoriesList = new UIList();
		private UIList _itemsList  = new UIList();
		private UIList _beastsList = new UIList();
		private UIList _partyList  = new UIList();

		private UIList _focusedList;



		private StateInventory()
		{
			_focusedList = _categoriesList;

			_categoriesList.AddItem("Items",  _itemsList);
			_categoriesList.AddItem("Beasts", _beastsList);
			_categoriesList.AddItem("Party",  _partyList);
		}

		public void Enter()
		{
			// Default UI values.
			_focusedList = _categoriesList;

			_categoriesList.SelectedItemIndex = 0;

			_itemsList.Clear();
			/*foreach (Inventory.BeastItem bi in Player.Instance.Inventory.Beasts)
			{
				_itemsList.AddItem($"{bi.Beast.Name} - LVL 1", bi);
			}*/

			_beastsList.Clear();
			foreach (Inventory.BeastItem bi in Player.Instance.Inventory.Beasts)
			{
				_beastsList.AddItem($"{bi.Beast.Name} - LVL {bi.Level}", bi);
			}

			_partyList.Clear();
			/*foreach (Inventory.BeastItem bi in Player.Instance.Inventory.Beasts)
			{
				_partyList.AddItem($"{bi.Beast.Name} - LVL 1", bi);
			}*/
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
			_categoriesList.PosX = 5;
			_categoriesList.PosY = 3;
			rm.CurrentColor = (short) (_focusedList == _categoriesList ? 0x06 : 0x0F);
			rm.RenderBox(4, 2, 28, 9);
			_categoriesList.Render(rm);

			/*rm.CurrentColor = 0x0f;
			int itemCategoriesX = 6, itemCategoriesY = 3;
			rm.RenderString(itemCategoriesX, itemCategoriesY + _selectedCategory * 2, ">");
			for (int i = 0; i < _categories.Length; ++i)
			{
				rm.RenderString(
					itemCategoriesX + 2,
					itemCategoriesY + i * 2,
					_categories[i]);
			}*/

			// Items view panel.
			if (_focusedList == _itemsList || _focusedList == _beastsList || _focusedList == _partyList)
			{
				rm.CurrentColor = (short)(0x06);
				rm.RenderBox(32, 2, 54, 26);
				rm.CurrentColor = 0x0F;

				rm.RenderBox(29, 5, 6, 3);
				rm.RenderString(31, 6, "->");

				if (_focusedList == _itemsList)
				{

				}
				else if (_focusedList == _beastsList)
				{
					_beastsList.PosX = 33;
					_beastsList.PosY = 3;
					_beastsList.Render(rm);
				}
				else if (_focusedList == _partyList)
				{

				}
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
					_focusedList.SelectPrevious();
					break;
				case ConsoleKey.DownArrow:
					_focusedList.SelectNext();
					break;
				case ConsoleKey.RightArrow:
					if (_focusedList == _categoriesList)
					{
						(string, object?)? nextList = _categoriesList.GetSelectedItem();
						if (nextList?.Item2 != null)
						{
							_focusedList = (UIList)nextList?.Item2;
							_focusedList.SelectedItemIndex = 0;
						}
					}
					break;
				case ConsoleKey.LeftArrow:
					if (_focusedList == _itemsList || _focusedList == _beastsList || _focusedList == _partyList)
					{
						_focusedList = _categoriesList;
					}
					break;
			}
		}
	}
}
