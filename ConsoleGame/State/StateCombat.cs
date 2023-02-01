using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ConsoleGame.State
{
	using ConsoleGame.Beast;
	using ConsoleGame.Inventory;

	internal class StateCombat : IState
	{
		public static StateCombat Instance { get; }

		static StateCombat()
		{
			Instance = new StateCombat();
		}



		private static Menu.Option[] _menuOptionsCombatStart =
		{
			new Menu.Option("Choose Beast", 5, 0, false, () => {}),
			new Menu.Option("Run Away", 5, 2, false, () => { Program.OpenScene(StateFreeRoam.Instance); })
		};

		private static Menu _menuCombatStart = new Menu(_menuOptionsCombatStart);



		BeastItem? _playerBeast = null;
		BeastItem? _enemyBeast = null;

		public void Enter()
		{
			Random randomNumber = new Random(); 
			int x = randomNumber.Next(1, Beast.Bestiary.Count);
			_enemyBeast = new BeastItem(Beast.Bestiary.ElementAt(x).Value); /*?? throw new NullReferenceException(); */
			// Needs to be assigned to a varaiable to be used in the render method
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

			// Base color.
			rm.CurrentColor = 0x0f;

			if (_enemyBeast != null)
				rm.RenderString(2, 1, $"You have encountered a wild {_enemyBeast.Beast.Name}!");

			// ----- UI Metrics -----
			const int MARGIN = 6;
			const int SPRITE_SIZE = 12;
			const int SPRITE_RECTX0 = MARGIN;
			const int SPRITE_RECTY = 8;
			const int SPRITE_RECTW = SPRITE_SIZE * 2 + 2;
			const int SPRITE_RECTH = SPRITE_SIZE + 2;

			// ----- Player Part -----
			rm.RenderBox(SPRITE_RECTX0, SPRITE_RECTY, SPRITE_RECTW, SPRITE_RECTH);
			if (_playerBeast != null)
			{
				rm.RenderString(MARGIN + 1, SPRITE_RECTY - 3, "Trucmuche");
				rm.RenderString(MARGIN + 1, SPRITE_RECTY + SPRITE_RECTH + 1, "LVL 1");
			}

			// ----- Enemy Part -----
			rm.RenderBox(Console.WindowWidth - SPRITE_RECTW - MARGIN, SPRITE_RECTY, SPRITE_RECTW, SPRITE_RECTH);
			if (_enemyBeast != null)
			{
				rm.RenderString(Console.WindowWidth - MARGIN - 1, SPRITE_RECTY - 3, _enemyBeast.Beast.Name, RenderManager.TextAlign.RIGHT);
				rm.RenderString(Console.WindowWidth - MARGIN - 1, SPRITE_RECTY + SPRITE_RECTH + 1, "LVL 1", RenderManager.TextAlign.RIGHT);
			}

			// ----- Health bars -----
			rm.CurrentColor = 0x7a;
			rm.RenderHLine(MARGIN + 1, SPRITE_RECTY - 2, 26, '█');
			rm.RenderHLine(Console.WindowWidth - SPRITE_RECTW - MARGIN - 1, SPRITE_RECTY - 2, 26, '█');
			rm.RenderString(Console.WindowWidth - 6, SPRITE_RECTY - 2, $"{_enemyBeast.Health.ToString()}/{_enemyBeast.Beast.MaxHealth.ToString()}");

			// ----- Player's choice box -----
			rm.CurrentColor = 0x0f;
			rm.RenderBox(SPRITE_RECTX0 + SPRITE_RECTW, SPRITE_RECTY, SPRITE_RECTW, 20);
			_menuCombatStart.Render(rm, SPRITE_RECTX0 + SPRITE_RECTW + 1, SPRITE_RECTY + 1, RenderManager.TextAlign.LEFT);

	
			// ----- Narration box -----
			rm.CurrentColor = 0x0f;
			rm.RenderBox(20, Console.WindowHeight - 5 - 1, 120, 5);

			rm.Transform = true;
		}
		

		public void KeyPress(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.UpArrow:   _menuCombatStart.NavigateToPrevious(); break;
				case ConsoleKey.DownArrow: _menuCombatStart.NavigateToNext(); break;

				case ConsoleKey.Enter:     _menuCombatStart.CallSelectedOption(); break;
				case ConsoleKey.E:
					Program.RenderManager.RenderString(20, Console.WindowHeight - 5 , $"The opponent {_enemyBeast.Beast.Name} used {_enemyBeast.Beast.Capacities[0].Name}!");
					_enemyBeast.Beast.Capacities[0].UseCapacity(_enemyBeast, _enemyBeast);
					break;
			}
		}
	}
}
