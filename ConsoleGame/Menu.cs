using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
	internal class Menu
	{
		// Option structure and callback type (when option is selected).
		public delegate void OptionCallback();

		public class Option
		{
			// Option attribs.
			public string name;
			public int posX;
			public int posY;

			// Disables (grays out) the option and prevents the user from selecting it.
			public bool disabled;
			public OptionCallback callback;

			public Option(string name, int posX, int posY, bool disabled, OptionCallback callback)
			{
				this.name = name;
				this.posX = posX;
				this.posY = posY;
				this.disabled = disabled;
				this.callback = callback;
			}
		}

		// Selected option index.
		public int SelectedOption { get; set; }

		// List of all options.
		private Option[] _options;



		public Menu(Option[] options)
		{
			// Default option.
			SelectedOption = 0;

			_options = options;
		}

		// Render the menu.
		// Assumes that the render manager has "Transform" set to false already.
		public void Render(RenderManager rm, int offsetX = 0, int offsetY = 0, RenderManager.TextAlign align = RenderManager.TextAlign.CENTER)
		{
			// Display all option strings.
			foreach (var opt in _options)
			{
				rm.CurrentColor = (opt.disabled ? (short)0x08 : (short)0x07);
				rm.RenderString(opt.posX + offsetX, opt.posY + offsetY, opt.name, align);
			}

			// Render a cursor next to the selected option.
			rm.CurrentColor = 0x0a;
			Option s = _options[SelectedOption];
			int posX = s.posX - 4;
			if (align == RenderManager.TextAlign.CENTER) posX -= s.name.Length / 2;
			rm.RenderString(posX + offsetX, s.posY + offsetY, ">>>");
		}

		// Navigates to first previous non-disabled option (skips disabled options on the way).
		public void NavigateToPrevious()
		{
			int newSelectedOption = SelectedOption - 1;

			while (newSelectedOption >= 0 && _options[newSelectedOption].disabled) newSelectedOption--;
			if (newSelectedOption != -1)
			{
				SelectedOption = newSelectedOption;
			}
		}

		// Navigates to next non-disabled option (skips disabled options on the way).
		public void NavigateToNext()
		{
			int newSelectedOption = SelectedOption + 1;

			while (newSelectedOption < _options.Length && _options[newSelectedOption].disabled) newSelectedOption++;
			if (newSelectedOption != _options.Length)
			{
				SelectedOption = newSelectedOption;
			}
		}

		public void CallSelectedOption()
		{
			Option opt = _options[SelectedOption];
			if (!opt.disabled)
			{
				opt.callback();
			}
		}
	}
}
