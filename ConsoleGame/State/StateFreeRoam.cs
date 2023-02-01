using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleGame.State
{
	using SpriteList = List<Tuple<Sprite, int, int>>;
	internal class StateFreeRoam : IState
	{
		Level _level = new Level();
		public static StateFreeRoam Instance { get; }

		static StateFreeRoam()
		{
			Instance = new StateFreeRoam();
		}

		public void Enter()
		{
			_level.GetMap().Load();
		}

		public void Leave()
		{

		}

		public IState? Update()
		{
			_level.GetPlayer().Update();
			return null;
		}

		public void Render()
		{
			RenderManager rm = Program.RenderManager;
			rm.Transform = true;

			// Render image tilemap & colors.
			rm.RenderImage(0, 0, _level.GetMap().Width * 2, _level.GetMap().Height, _level.GetMap().GetImageMap());
			for (int y = 0; y < _level.GetMap().Height; y++)
			{
				rm.FillColors(0, y, _level.GetMap().Width * 2, _level.GetMap().GetImageColors(), y * _level.GetMap().Width * 2);
			}

			// Render sprites.
			foreach (var item in _level.GetMap().GetSprites())
			{
				rm.RenderSprite(item.Item2, item.Item3, item.Item1);
			}

			// Render player.
			Sprite player = Sprite.GetSprite("WARRIOR");
			rm.RenderSprite(Program.RenderManager.CameraPosX, Program.RenderManager.CameraPosY - 2, player);
		}

		public void KeyPress(ConsoleKey key)
		{
			_level.GetPlayer().KeyPress(key);
			// Spawn Enemy when player moves
			_level.SpawnEnemy();
			switch (key)
			{
				case ConsoleKey.Escape:
					Program.OpenScene(StatePaused.Instance);
					break;

				case ConsoleKey.E:
					Program.OpenScene(StateInventory.Instance);
					break;
			}
		}
	}
}
