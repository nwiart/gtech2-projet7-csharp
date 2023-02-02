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
		public enum ECombatState
		{
			COMBAT_BEGIN,
			CHOOSE_BEAST,
			CHOOSE_ACTION,
			CHOOSE_ATTACK
		}

		public static StateCombat Instance { get; }

		static StateCombat()
		{
			Instance = new StateCombat();
		}



		BeastItem? _playerBeast;
		BeastItem? _enemyBeast = null;

		string _narrationString = "";

		ECombatState _state;

		private static Menu.Option[] _menuOptionsActions =
		{
			new Menu.Option("Attack", 5, 0, false, () => { }),
			new Menu.Option("Switch Beast", 5, 2, false, () => { }),
			new Menu.Option("Run Away", 5, 4, false, () => { }),
		};

		public void ChooseBeast(BeastItem b)
		{
			_playerBeast = b;
			SetCombatState(ECombatState.CHOOSE_ACTION);
		}

		public void SetNarration(string s)
		{
			_narrationString = s;
		}

		public void RunAway()
		{
			Program.OpenScene(StateFreeRoam.Instance);
		}

		public void PlayerAttack(Beast.Capacity c)
		{
			c.UseCapacity(_playerBeast, _enemyBeast);
		}
		public void EnemyAttack(Beast.Capacity c)
		{
			c.UseCapacity(_enemyBeast, _playerBeast);
		}

		public void SetCombatState(ECombatState s)
		{
			_state = s;

			int posY = 0;

			List<Menu.Option> options = new List<Menu.Option>();
			switch (_state)
			{
				case ECombatState.COMBAT_BEGIN:
					options.Add(new Menu.Option("Choose Beast", 5, 0, false, () => { SetCombatState(ECombatState.CHOOSE_BEAST); }));
					options.Add(new Menu.Option("Run Away", 5, 2, false, () => { RunAway(); }));
					break;

				case ECombatState.CHOOSE_BEAST:
					foreach (BeastItem? b in Player.Instance.Inventory.Party) {
						if (b == null)
							options.Add(new Menu.Option("---", 5, posY, true, () => { }));
						else
							options.Add(new Menu.Option(b.Beast.Name, 5, posY, false, () => { ChooseBeast(b); }));

						posY += 2;
					}
					break;

				case ECombatState.CHOOSE_ACTION:
					options.Add(new Menu.Option("Attack", 5, posY, false, () => { SetCombatState(ECombatState.CHOOSE_ATTACK); })); posY += 2;
					options.Add(new Menu.Option("Switch Beast", 5, posY, false, () => { })); posY += 2;
					options.Add(new Menu.Option("Run Away", 5, posY, false, () => { RunAway(); })); posY += 2;
					break;

				case ECombatState.CHOOSE_ATTACK:
					foreach (Beast.Capacity capacity in _playerBeast.Beast.Capacities)
					{
						options.Add(new Menu.Option(capacity.Name, 5, posY, false, () => { PlayerAttack(capacity); }));
						posY += 2;
					}
					break;
			}

			_focusedMenu = new Menu(options.ToArray());
		}

		private Menu _focusedMenu;

		//private static Menu _menuCombatStart = new Menu(_menuOptionsCombatStart);

		private static Menu? _menuChooseBeast;

		private static Menu? _menuCombat;

		public StateCombat()
		{
			_focusedMenu = null;
		}



		public void Enter()
		{
			Random x = new Random();
			Random y = new Random();
			int randomLevel = y.Next(1, 6);
			int randomNumber = x.Next(1, Beast.Bestiary.Count);
			_enemyBeast = new BeastItem(Beast.Bestiary.ElementAt(randomNumber).Value, randomLevel); /*?? throw new NullReferenceException(); */
			// Needs to be assigned to a varaiable to be used in the render method

			SetNarration($"You have encountered a wild {_enemyBeast.Beast.Name}!");
			SetCombatState(ECombatState.COMBAT_BEGIN);
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
				rm.RenderString(MARGIN + 1, SPRITE_RECTY - 3, $"{_playerBeast.Beast.Name}");
				rm.RenderString(MARGIN + 1, SPRITE_RECTY + SPRITE_RECTH + 1, $"LVL {_playerBeast.Level}");
			}

			// ----- Enemy Part -----
			rm.RenderBox(Console.WindowWidth - SPRITE_RECTW - MARGIN, SPRITE_RECTY, SPRITE_RECTW, SPRITE_RECTH);
			if (_enemyBeast != null)
			{
				rm.RenderString(Console.WindowWidth - MARGIN - 1, SPRITE_RECTY - 3, _enemyBeast.Beast.Name, RenderManager.TextAlign.RIGHT);
				rm.RenderString(Console.WindowWidth - MARGIN - 1, SPRITE_RECTY + SPRITE_RECTH + 1, $"LVL {_enemyBeast.Level}", RenderManager.TextAlign.RIGHT);
			}

			// ----- Health bars -----
			rm.CurrentColor = 0x7a;
			rm.RenderHLine(MARGIN + 1, SPRITE_RECTY - 2, 26, '█');
			rm.RenderHLine(Console.WindowWidth - SPRITE_RECTW - MARGIN - 1, SPRITE_RECTY - 2, 26, '█');
			rm.CurrentColor = 0x0a;
			if (_playerBeast != null)
				rm.RenderString(6, SPRITE_RECTY - 2, $"{_playerBeast.Health.ToString()}/{_playerBeast.GetMaxHealth().ToString()}", RenderManager.TextAlign.RIGHT);
			if (_enemyBeast != null)
				rm.RenderString(Console.WindowWidth - 6, SPRITE_RECTY - 2, $"{_enemyBeast.Health.ToString()}/{_enemyBeast.GetMaxHealth().ToString()}");

			// ----- Player's choice box -----
			rm.CurrentColor = 0x0f;
			rm.RenderBox(SPRITE_RECTX0 + SPRITE_RECTW, SPRITE_RECTY, SPRITE_RECTW, 10);
			_focusedMenu.Render(rm, SPRITE_RECTX0 + SPRITE_RECTW + 1, SPRITE_RECTY + 1, RenderManager.TextAlign.LEFT);

			/*}
			else
			{
				rm.CurrentColor = 0x0f;
				rm.RenderBox(SPRITE_RECTX0 + SPRITE_RECTW, SPRITE_RECTY, SPRITE_RECTW, 20);
				rm.RenderString(SPRITE_RECTX0 + SPRITE_RECTW + 1, SPRITE_RECTY + 1, $"1. {_playerBeast.Beast.Capacities[0].Name}");
				rm.RenderString(SPRITE_RECTX0 + SPRITE_RECTW + 1, SPRITE_RECTY + 2, $"2. {_playerBeast.Beast.Capacities[1].Name}");
				rm.RenderString(SPRITE_RECTX0 + SPRITE_RECTW + 1, SPRITE_RECTY + 3, $"3. {_playerBeast.Beast.Capacities[1].Name}");
				rm.RenderString(SPRITE_RECTX0 + SPRITE_RECTW + 1, SPRITE_RECTY + 4, $"4. {_playerBeast.Beast.Capacities[1].Name}");
			}*/

			// ----- Narration box -----
			rm.CurrentColor = 0x0f;
			rm.RenderBox(20, Console.WindowHeight - 5 - 1, 120, 5);
			rm.RenderString(22, Console.WindowHeight - 5 + 1, _narrationString);

			rm.Transform = true;
		}

		public void KeyPress(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.UpArrow: _focusedMenu.NavigateToPrevious(); break;
				case ConsoleKey.DownArrow: _focusedMenu.NavigateToNext(); break;

				case ConsoleKey.Enter: _focusedMenu.CallSelectedOption(); break;
				case ConsoleKey.E:
					Program.RenderManager.RenderString(20, Console.WindowHeight - 5, $"The opponent {_enemyBeast.Beast.Name} used {_enemyBeast.Beast.Capacities[0].Name}!");
					_playerBeast.Beast.Capacities[0].UseCapacity(_playerBeast, _enemyBeast);
					if (_enemyBeast.Health <= 0)
					{
						// Say You won
						// gain exp for all the team
						// Check Evolve for each beast
					}
					else
					{
						// Say WELCOME TO DARK SOULS
					}
					break;
			}
		}
	}
}
