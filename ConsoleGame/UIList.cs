using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
	internal class UIList
	{
		// Position on screen, in characters.
		public int PosX { get; set; }
		public int PosY { get; set; }

		private int _selectedItemIndex;
		public int SelectedItemIndex { get => _selectedItemIndex; set {
				if (value < -1 || value >= _itemStrings.Count) throw new IndexOutOfRangeException();
				_selectedItemIndex = value;
			} }

		// Items and user defined object.
		private List<string>  _itemStrings;
		private List<object?> _itemObjects;

		public UIList()
		{
			PosX = 0;
			PosY = 0;
			_itemStrings = new List<string>();
			_itemObjects = new List<object?>();

			SelectedItemIndex = -1;
		}

		public void Clear()
		{
			_itemStrings.Clear();
			_itemObjects.Clear();
		}

		// Add an item to the end of the list.
		public void AddItem(string displayName, object? o)
		{
			_itemStrings.Add(displayName);
			_itemObjects.Add(o);
		}

		// Get the display name and associated object of the item at a specific index.
		public (string, object?) GetItemAt(int index)
		{
			return (_itemStrings[index], _itemObjects[index]);
		}

		public (string, object?)? GetSelectedItem()
		{
			return SelectedItemIndex == -1 ? null : GetItemAt(SelectedItemIndex);
		}

		public void SelectPrevious()
		{
			if (SelectedItemIndex > 0) SelectedItemIndex--;
		}

		public void SelectNext()
		{
			if (SelectedItemIndex < _itemStrings.Count - 1) SelectedItemIndex++;
		}

		// Render the list.
		public virtual void Render(RenderManager rm)
		{
			int posY = 0;
			rm.CurrentColor = 0x0f;

			foreach (string s in _itemStrings)
			{
				rm.RenderString(5 + PosX, posY + PosY, s);
				posY++;
			}

			if (SelectedItemIndex != -1)
				rm.RenderString(3 + PosX, SelectedItemIndex + PosY, ">");
		}
	}
}
