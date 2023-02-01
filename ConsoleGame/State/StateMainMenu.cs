using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.State
{
	internal class StateMainMenu : IState
	{
		private delegate void OptionCallback();

		private class Option
		{
			public string name;
			public int posX;
			public int posY;
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

		// Main menu options.
		private int _option = 0;
		private Option[] _options =
		{
			new Option("New Game", 80, 10, false, () =>
				{
					Program.OpenScene(StateFreeRoam.Instance);
				}
			),
			new Option("Load Saved Game", 80, 13, true, () =>
				{
					
				}
			),
			new Option("Quit", 80, 16, false, () =>
				{
					
				}
			)
		};

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
			RenderManager rm = Program.RenderManager;

			foreach (var opt in _options)
			{
				rm.CurrentColor = (opt.disabled ? (short) 0x08 : (short) 0x07);
				rm.RenderString(opt.posX, opt.posY, opt.name, RenderManager.TextAlign.CENTER);
			}
			rm.RenderString(66, _options[_option].posY, ">>>");

			rm.RenderString(2, Console.WindowHeight - 2, "© 2023 ConsoleGame");
		}

		public void KeyPress(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.UpArrow: if (_option > 0 ) _option--; break;
				case ConsoleKey.DownArrow: if ( _option < _options.Length - 1 ) _option++; break;

				case ConsoleKey.Enter:
					if (!_options[_option].disabled)
						_options[_option].callback();
					break;
			}
		}
	}
}
