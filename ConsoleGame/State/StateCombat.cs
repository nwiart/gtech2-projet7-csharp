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
            CHOOSE_ATTACK,

            PLAYER_ATTACK,
            ENEMY_ATTACK,

            PLAYER_DIED,
            ENEMY_DIED
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
        Menu.OptionCallback _nextStateCallback;

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
            SetCombatState(ECombatState.PLAYER_ATTACK);

            c.UseCapacity(_playerBeast, _enemyBeast);
            Program.ShakeIntensity = 15.0F;

            SetNarration($"{_playerBeast.Beast.Name} used {c.Name}!");

            // Enemy has died.
            if (_enemyBeast.Health <= 0)
            {
                _nextStateCallback = () =>
                {
                    SetCombatState(ECombatState.ENEMY_DIED);
                    SetNarration($"{_enemyBeast.Beast.Name} has died!");
                    foreach (BeastItem? item in Player.Instance.Inventory.Party)
                    {
                        if (item != null)
                        {
                            item.GainExperience(_enemyBeast.Beast.ExperienceDropped + _enemyBeast.Level);
                        }
                    }
                    _enemyBeast = null;
                };
            }
        }
        public void EnemyAttack()
        {
            SetCombatState(ECombatState.ENEMY_ATTACK);

            Random random = new Random();
            Beast.Capacity c = _enemyBeast.Beast.Capacities[random.Next(0, _enemyBeast.Beast.Capacities.Count())];

            c.UseCapacity(_enemyBeast, _playerBeast);
            Program.ShakeIntensity = 15.0F;

            SetNarration($"{_enemyBeast.Beast.Name} used {c.Name}!");

            // Player's beast died.
            if (_playerBeast.Health <= 0)
            {
                _nextStateCallback = () =>
                {
                    SetNarration($"Your {_playerBeast.Beast.Name} has died!");
                    Player.Instance.Inventory.RemoveBeastFromParty(_playerBeast);
                    _playerBeast = null;
                    SetCombatState(ECombatState.PLAYER_DIED);
                };
            }
            else
            {
                _nextStateCallback = () => { SetCombatState(ECombatState.CHOOSE_ACTION); };
            }
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
                    foreach (BeastItem? b in Player.Instance.Inventory.Party)
                    {
                        if (b == null)
                            options.Add(new Menu.Option("---", 5, posY, true, () => { }));
                        else
                            options.Add(new Menu.Option(b.Beast.Name, 5, posY, false, () => { ChooseBeast(b); }));

                        posY += 2;
                    }
                    break;

                case ECombatState.CHOOSE_ACTION:
                    SetNarration("");
                    options.Add(new Menu.Option("Attack", 5, posY, false, () => { SetCombatState(ECombatState.CHOOSE_ATTACK); })); posY += 2;
                    options.Add(new Menu.Option("Switch Beast", 5, posY, false, () => { SetCombatState(ECombatState.CHOOSE_BEAST); })); posY += 2;
                    options.Add(new Menu.Option("Run Away", 5, posY, false, () => { RunAway(); })); posY += 2;
                    break;

                case ECombatState.CHOOSE_ATTACK:
                    foreach (Beast.Capacity capacity in _playerBeast.Beast.Capacities)
                    {
                        bool enoughMana = (_playerBeast.Mana >= capacity.ManaCost);
                        options.Add(new Menu.Option(capacity.Name, 5, posY, !enoughMana, () => { PlayerAttack(capacity); }));
                        posY += 2;
                    }
                    break;

                case ECombatState.PLAYER_ATTACK:
                    _nextStateCallback = () => { EnemyAttack(); };
                    break;
                case ECombatState.ENEMY_ATTACK:
                    break;

                case ECombatState.PLAYER_DIED:
                    _nextStateCallback = () =>
                    {
                        // Game over?
                        bool hasAliveBeast = false;
                        foreach (BeastItem? bi in Player.Instance.Inventory.Party)
                        {
                            if (bi != null)
                            {
                                hasAliveBeast = true;
                                break;
                            }
                        }

                        if (hasAliveBeast) SetCombatState(ECombatState.COMBAT_BEGIN);
                        else Program.OpenScene(StateGameOver.Instance);
                    };
                    break;
                case ECombatState.ENEMY_DIED:
                    _nextStateCallback = () => { Program.OpenScene(StateFreeRoam.Instance); };
                    break;
            }

            if (options.Count == 0)
                _focusedMenu = null;
            else
                _focusedMenu = new Menu(options.ToArray());
        }

        private Menu? _focusedMenu;

        public StateCombat()
        {
            _focusedMenu = null;
        }



        public void Enter()
        {
            Random x = new Random();
            Random y = new Random();

            int averageLevel = 0;
            foreach (BeastItem? b in Player.Instance.Inventory.Party)
            {
                if (b != null)
                    averageLevel += b.Level;
            }
            averageLevel /= Player.Instance.Inventory.Party.Count();

     


            int randomLevel = y.Next(averageLevel - 1, averageLevel + 7);
            int randomNumber = x.Next(1, Beast.Bestiary.Count);


            int maxLevelPossible = randomLevel + 5;


            _enemyBeast = new BeastItem(Beast.Bestiary.ElementAt(randomNumber).Value, (randomLevel)); /*?? throw new NullReferenceException(); */
            

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
            const int SPRITE_RECTY = 10;
            const int SPRITE_RECTW = SPRITE_SIZE * 2 + 2;
            const int SPRITE_RECTH = SPRITE_SIZE + 2;

            // ----- Player Part -----
            rm.RenderBox(SPRITE_RECTX0, SPRITE_RECTY, SPRITE_RECTW, SPRITE_RECTH);
            if (_playerBeast != null)
            {
                rm.RenderString(MARGIN + 1, SPRITE_RECTY -5, $"{_playerBeast.Beast.Name}");
                rm.RenderString(MARGIN + 1, SPRITE_RECTY + SPRITE_RECTH + 1, $"LVL {_playerBeast.Level}");
            }

            // ----- Enemy Part -----
            rm.RenderBox(Console.WindowWidth - SPRITE_RECTW - MARGIN, SPRITE_RECTY, SPRITE_RECTW, SPRITE_RECTH);
            if (_enemyBeast != null)
            {
                rm.RenderString(Console.WindowWidth - MARGIN - 1, SPRITE_RECTY - 5, _enemyBeast.Beast.Name, RenderManager.TextAlign.RIGHT);
                rm.RenderString(Console.WindowWidth - MARGIN - 1, SPRITE_RECTY + SPRITE_RECTH + 1, $"LVL {_enemyBeast.Level}", RenderManager.TextAlign.RIGHT);
            }

            // ----- Health bars -----
            rm.CurrentColor = 0x8a;
            {
                if (_playerBeast != null)
                {
                    rm.RenderHLine(MARGIN + 1, SPRITE_RECTY - 4, 26, ' ');
                    rm.RenderHLine(MARGIN + 1, SPRITE_RECTY - 4, (int)(26 * (_playerBeast.Health / (float)_playerBeast.GetMaxHealth())), '█');
                }
                if (_enemyBeast != null)
                {
                    rm.RenderHLine(Console.WindowWidth - SPRITE_RECTW - MARGIN - 1, SPRITE_RECTY - 4, 26, ' ');
                    rm.RenderHLine(Console.WindowWidth - SPRITE_RECTW - MARGIN - 1, SPRITE_RECTY - 4, (int)(26 * (_enemyBeast.Health / (float)_enemyBeast.GetMaxHealth())), '█');
                }
            }

            // ----- Mana bars -----
            rm.CurrentColor = 0x89;
            {
                // Player Mana
                if (_playerBeast != null)
                {
                    rm.RenderHLine(MARGIN + 1, SPRITE_RECTY - 2, 26, ' ');
                    rm.RenderHLine(MARGIN + 1, SPRITE_RECTY - 2, (int)(26 * (_playerBeast.Mana / (float)_playerBeast.GetMaxMana())), '█');
                }
                // Enemy Mana
                if (_enemyBeast != null)
                {
                    rm.RenderHLine(Console.WindowWidth - SPRITE_RECTW - MARGIN - 1, SPRITE_RECTY - 2, 26, ' ');
                    rm.RenderHLine(Console.WindowWidth - SPRITE_RECTW - MARGIN - 1, SPRITE_RECTY - 2, (int)(26 * (_enemyBeast.Mana / (float)_enemyBeast.GetMaxMana())), '█');
                }
            }
            // Health Values
            rm.CurrentColor = 0x0a;
            {
                // Player health
                if (_playerBeast != null)
                    rm.RenderString(6, SPRITE_RECTY - 4, $"{_playerBeast.Health.ToString()}/{_playerBeast.GetMaxHealth().ToString()}", RenderManager.TextAlign.RIGHT);
                if (_enemyBeast != null)
                    rm.RenderString(Console.WindowWidth - 6, SPRITE_RECTY - 4, $"{_enemyBeast.Health.ToString()}/{_enemyBeast.GetMaxHealth().ToString()}");
                    
                // Enemy health
                if (_playerBeast != null)
                    rm.RenderString(6, SPRITE_RECTY - 4, $"{_playerBeast.Health.ToString()}/{_playerBeast.GetMaxHealth().ToString()}", RenderManager.TextAlign.RIGHT);
                if (_enemyBeast != null)
                    rm.RenderString(Console.WindowWidth - 6, SPRITE_RECTY - 4, $"{_enemyBeast.Health.ToString()}/{_enemyBeast.GetMaxHealth().ToString()}");
            }
            // Mana Values
            rm.CurrentColor = 0x09;
            {
                // Player mana
                if (_playerBeast != null)
                    rm.RenderString(6, SPRITE_RECTY - 2, $"{_playerBeast.Mana.ToString()}/{_playerBeast.GetMaxMana().ToString()}", RenderManager.TextAlign.RIGHT);
                if (_enemyBeast != null)
                    rm.RenderString(Console.WindowWidth - 6, SPRITE_RECTY - 2, $"{_enemyBeast.Mana.ToString()}/{_enemyBeast.GetMaxMana().ToString()}");
                    
                // Enemy mana
                if (_playerBeast != null)
                    rm.RenderString(6, SPRITE_RECTY - 2, $"{_playerBeast.Mana.ToString()}/{_playerBeast.GetMaxMana().ToString()}", RenderManager.TextAlign.RIGHT);
                if (_enemyBeast != null)
                    rm.RenderString(Console.WindowWidth - 6, SPRITE_RECTY - 2, $"{_enemyBeast.Mana.ToString()}/{_enemyBeast.GetMaxMana().ToString()}");
            }

            // ----- Player's choice box -----
            if (_focusedMenu != null)
            {
                rm.CurrentColor = 0x0f;
                rm.RenderBox(SPRITE_RECTX0 + SPRITE_RECTW, SPRITE_RECTY , SPRITE_RECTW, 10);
                _focusedMenu.Render(rm, SPRITE_RECTX0 + SPRITE_RECTW + 1, SPRITE_RECTY + 1, RenderManager.TextAlign.LEFT);
            }

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

                case ConsoleKey.Enter:
                    if (_focusedMenu != null)
                    {
                        _focusedMenu.CallSelectedOption();
                    }
                    else
                    {
                        _nextStateCallback();
                    }
                    break;
            }
        }
    }
}
