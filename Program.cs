using ConsoleGame.State;
using System;
using System.Runtime.InteropServices;

namespace ConsoleGame
{
	class Program
	{
		public static RenderManager? RenderManager;
		public static IState? currentState;
		public static IState? nextState;

		public static void OpenScene(IState s)
		{
			nextState = s;
		}

		static void Main(string[] args)
		{
			RenderManager = new RenderManager(Console.WindowWidth, Console.WindowHeight);

			currentState = new StateMainMenu();
			nextState = null;

			Sprite.LoadSprites();

			Win32.SetConsoleTitle("ConsoleGame");

			// Game loop.
			while (true)
			{
				// Key input.
				while (Console.KeyAvailable)
				{
					currentState.KeyPress(Console.ReadKey().Key);
				}

				// Render.
				RenderManager.Clear();
				currentState.Render();
				RenderManager.SwapBuffers();

				Thread.Sleep(20);

				if (nextState != null)
				{
					currentState.Leave();
					currentState = nextState;
					nextState = null;
					currentState.Enter();
				}
			}
		}
	}
}
