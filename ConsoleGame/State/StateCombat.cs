using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.State
{
	internal class StateCombat : IState
	{
		public static StateCombat Instance { get; }
		static StateCombat()
		{
			Instance = new StateCombat();
		}



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
			RenderManager? rm = Program.RenderManager;
			rm.Transform = false;

			rm.RenderString(2, 1, "You have encountered a wild ------!");

			rm.RenderBox(6, 8, 28, 14);
			rm.RenderString(6 + 1, 5, "Trucmuche");
			rm.RenderBox(Console.WindowWidth - 28 - 6, 8, 28, 14);
			rm.RenderString(Console.WindowWidth - 6 - 1, 5, "Bidule", RenderManager.TextAlign.RIGHT);

			rm.CurrentColor = 0x7a;
			rm.RenderHLine(6 + 1, 6, 26, '█');
			rm.RenderHLine(Console.WindowWidth - 28 - 6 + 1, 6, 26, '█');

			rm.CurrentColor = 0x0f;
			rm.RenderBox(6 + 30, 4, 28, 20);

			rm.Transform = true;
		}

		public void KeyPress(ConsoleKey key)
		{
			
		}
	}
}
