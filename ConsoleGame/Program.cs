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
		public static bool _wantsToExit = false;

		public static float ShakeIntensity { get; set; }

		public static void OpenScene(IState s)
		{
			_nextState = s;
		}

		public static void Exit()
		{
			_wantsToExit = true;
		}

		static void Main(string[] args)
		{
			Console.WindowWidth = 160;
			Console.WindowHeight = 42;

			RenderManager = new RenderManager(Console.WindowWidth, Console.WindowHeight);

			// Initialize state and emulate state entering in main menu.
			_currentState = StateMainMenu.Instance;
			_nextState = null;
			_currentState.Enter();

			Sprite.LoadSprites();

			// Set console title & make it unresizable.
			Win32.SetConsoleTitle("ConsoleGame");

			IntPtr consoleHwnd = Win32.GetConsoleWindow();
			int style = Win32.GetWindowLongA(consoleHwnd, Win32.GWL_STYLE);
			style = style & ~(Win32.WS_MAXIMIZEBOX | Win32.WS_THICKFRAME);
			Win32.SetWindowLongA(consoleHwnd, Win32.GWL_STYLE, style);

			ShakeIntensity = 0.0F;
			Win32.SetWindowPos(consoleHwnd, IntPtr.Zero, 100, 100, 0, 0, Win32.SWP_NOSIZE);
			Random random = new Random();

			// Game loop.
			while (!_wantsToExit)
			{
				// Key input.
				while (Console.KeyAvailable)
				{
					ConsoleKey key = Console.ReadKey().Key;
					_currentState.KeyPress(key);
				}

				ShakeIntensity /= 1.1F;
				Win32.SetWindowPos(consoleHwnd, IntPtr.Zero, 100 + (int)(random.Next(-1, 1) * ShakeIntensity), 100 + (int)(random.Next(-1, 1) * ShakeIntensity), 0, 0, Win32.SWP_NOSIZE);

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
