using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGame.State
{
	public class StateGameOver : IState
	{
		public static StateGameOver Instance { get; }

		static StateGameOver()
		{
			Instance = new StateGameOver();
		}



		private static Menu.Option[] _options =
		{
			new Menu.Option("Reload Save", 80, 16, false, () => { Program.OpenScene(StateFreeRoam.Instance); }),
			new Menu.Option("Quit to Title", 80, 19, false, () => { Program.OpenScene(StateMainMenu.Instance); })
		};

		private Menu _menu = new Menu(_options);

		public void Enter()
		{
			
		}

		public void Leave()
		{
			
		}

		IState? IState.Update()
		{
			return null;
		}

		public void Render()
		{
			RenderManager rm = Program.RenderManager;
			rm.Transform = false;

			rm.CurrentColor = 0x0c;
			rm.RenderString(80, 10, "Game Over!", RenderManager.TextAlign.CENTER);

			_menu.Render(rm);

			rm.Transform = true;
		}

		public void KeyPress(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.UpArrow: _menu.NavigateToPrevious(); break;
				case ConsoleKey.DownArrow: _menu.NavigateToNext(); break;

				case ConsoleKey.Enter:
					_menu.CallSelectedOption();
					break;
			}
		}
	}
}
