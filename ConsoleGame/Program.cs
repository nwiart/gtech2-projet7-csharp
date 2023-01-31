using ConsoleGame.State;
using System;
using System.Runtime.InteropServices;

namespace ConsoleGame
{
	class Program
	{
		public static RenderManager? RenderManager;
		private static IState? _currentState;
		private static IState? _nextState;

		public static bool _paused = false;

		public static void OpenScene(IState s)
		{
			_nextState = s;
		}

		static void Main(string[] args)
		{
			Console.WindowWidth = 160;
			Console.WindowHeight = 42;

			RenderManager = new RenderManager(Console.WindowWidth, Console.WindowHeight);

			_currentState = new StateMainMenu();
			_nextState = null;

			Sprite.LoadSprites();

			// Set console title & make it unresizable.
			Win32.SetConsoleTitle("ConsoleGame");

			IntPtr consoleHwnd = Win32.GetConsoleWindow();
			int style = Win32.GetWindowLongA(consoleHwnd, Win32.GWL_STYLE);
			style = style & ~(Win32.WS_MAXIMIZEBOX | Win32.WS_THICKFRAME);
			Win32.SetWindowLongA(consoleHwnd, Win32.GWL_STYLE, style);

			// Game loop.
			while (true)
			{
				// Key input.
				while (Console.KeyAvailable)
				{
					ConsoleKey key = Console.ReadKey().Key;
					switch (key)
					{
						case ConsoleKey.Escape:
							_paused = !_paused;
							break;

						default:
							if (!_paused)
							{
								_currentState.KeyPress(key);
							}
							break;
					}
				}

                _currentState.Update();

				// Render.
				RenderManager.Clear();
				_currentState.Render();
				RenderManager.SwapBuffers();

				Thread.Sleep(1);

				if (_nextState != null)
				{
					_currentState.Leave();
					_currentState = _nextState;
					_nextState = null;
					_currentState.Enter();
				}
			}
		}
	}
}
