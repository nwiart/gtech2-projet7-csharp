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
			RenderManager rm = Program.RenderManager;
			rm.Transform = false;

			rm.RenderString(2, 1, "You have encountered a wild ------!");

			rm.Transform = true;
		}

		public void KeyPress(ConsoleKey key)
		{
			
		}
	}
}
