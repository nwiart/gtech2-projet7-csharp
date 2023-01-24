using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.State
{
	internal interface IState
	{
		public void Enter();

		public void Leave();

		public IState? Update();

		public void Render();

		public void KeyPress(ConsoleKey key);
	}
}
