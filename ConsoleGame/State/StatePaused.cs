using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.State
{
	internal class StatePaused : IState
	{
		private static readonly string PAUSED_IMAGE =
			" ______   ______   _    _   ______   ______   ______ " +
			"|  __  | |  __  | | |  | | |  ____| |  ____| |  __  |" +
			"| |__| | | |__| | | |  | | | |____  | |___   | |  | |" +
			"|  ____| |  __  | | |  | | |____  | |  ___|  | |  | |" +
			"| |      | |  | | | |__| |  ____| | | |____  | |_/  /" +
			"|_|      |_|  |_| |______| |______| |______| |_____/ ";

		public static StatePaused Instance { get; }
		static StatePaused()
		{
			Instance = new StatePaused();
		}



		private static Menu.Option[] _options =
		{
			new Menu.Option("Resume Game", 80, 20, false, () =>
			{
				Program.OpenScene(StateFreeRoam.Instance);
			}),
			new Menu.Option("Save & Quit", 80, 24, false, () =>
			{
				Save.SaveProgress();
				Program.OpenScene(StateMainMenu.Instance);
			})
		};

		private Menu _menu = new Menu(_options);



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
			rm.Transform = false;

			rm.CurrentColor = 0x0e;
			rm.RenderImage((Console.WindowWidth - PAUSED_IMAGE.Count() / 6) / 2, 6, PAUSED_IMAGE.Count() / 6, 6, PAUSED_IMAGE);

			// Render menu.
			_menu.Render(rm);

			rm.Transform = true;
		}

		public void KeyPress(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.UpArrow:   _menu.NavigateToPrevious(); break;
				case ConsoleKey.DownArrow: _menu.NavigateToNext(); break;

				case ConsoleKey.Enter:     _menu.CallSelectedOption(); break;

				case ConsoleKey.Escape:
					Program.OpenScene(StateFreeRoam.Instance);
					break;
			}
		}
	}
}
