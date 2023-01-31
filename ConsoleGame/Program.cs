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
			RenderManager = new RenderManager(Console.WindowWidth, Console.WindowHeight);

			_currentState = new StateMainMenu();
			_nextState = null;

			Sprite.LoadSprites();

			Win32.SetConsoleTitle("ConsoleGame");

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
