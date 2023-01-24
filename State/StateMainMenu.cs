using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.State
{
	internal class StateMainMenu : IState
	{
		private int _option = 0;

		private (string, int, int, Func<bool>)[] _options =
		{
			("Play", 6, 1, () =>
				{
					Program.OpenScene(new StateFreeRoam());
					return true;
				}
			),
			("Quit", 6, 3, () =>
				{
					return true;
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
			Program.RenderManager.RenderString(6, 1, "Play");
			Program.RenderManager.RenderString(6, 3, "Quit");

			Program.RenderManager.RenderString(2, _options[_option].Item3, ">>>");

			Program.RenderManager.RenderString(2, Console.WindowHeight - 2, "© 2023 ConsoleGame");
		}

		public void KeyPress(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.UpArrow: if (_option > 0 ) _option--; break;
				case ConsoleKey.DownArrow: if ( _option < _options.Length - 1 ) _option++; break;

				case ConsoleKey.Enter:
					_options[_option].Item4();
					break;
			}
		}
	}
}
