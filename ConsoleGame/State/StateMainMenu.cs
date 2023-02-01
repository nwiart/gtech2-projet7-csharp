using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.State
{
	internal class StateMainMenu : IState
	{
		public static StateMainMenu Instance;

		static StateMainMenu()
		{
			Instance = new StateMainMenu();
		}



		// Main menu options.
		private static Menu.Option[] _options =
		{
			new Menu.Option("New Game", 80, 10, false, () =>
				{
					Program.OpenScene(StateFreeRoam.Instance);
				}
			),
			new Menu.Option("Load Saved Game", 80, 13, true, () =>
				{
					Save.LoadProgress();
					Program.OpenScene(StateFreeRoam.Instance);
				}
			),
			new Menu.Option("Quit", 80, 16, false, () =>
				{
					Program.Exit();
				}
			)
		};

		private Menu _menu = new Menu(_options);

		

		public void Enter()
		{
			_options[1].disabled = !Save.Exists();
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

			_menu.Render(rm);

			rm.RenderString(2, Console.WindowHeight - 2, "Valentin FAGUET & Noah WIART");
			rm.Transform = false;
		}

		public void KeyPress(ConsoleKey key)
		{
			switch (key)
			{
				// Navigation.
				case ConsoleKey.UpArrow:   _menu.NavigateToPrevious(); break;
				case ConsoleKey.DownArrow: _menu.NavigateToNext(); break;

				case ConsoleKey.Enter:     _menu.CallSelectedOption(); break;
			}
		}
	}
}
