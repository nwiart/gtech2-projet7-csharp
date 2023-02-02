/*  ---- MAP INDEXS ---- */
/*
|        |*| = Grass
|        |@| = Player Spawn
|        |!| = NPC Spawn
|        |+| = Rock
|        |?  | = Tree --- offest = 2
|        |~| = Water
|        |X| = House
|        |#| = Wall


*/

namespace ConsoleGame
{
	using System.Text;
	using SpriteList = List<Tuple<Sprite, int, int>>;

	// Map class made of indexes 
	public class Map
	{
		private string[] _mapLoaded =
			{
				"?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ",
				"   *********************************************.......*********************................................   ",
				"   *********************************************.......*********************................................   ",
				"   *********************************************.......*********************................................   ",
				"   *********************************************.......*********************................................   ",
				"?  *******************************************.......***********************................................?  ",
				"   *******************************************.........*********************................................   ",
				"   *******************************************.........*********************................................   ",
				"   ******************************************..........*********************................................   ",
				"   ******************************************...........********************................................   ",
				"?  *******************X**********X***********........***********************................................?  ",
				"   *******************************************........**********************................................   ",
				"   *****************************************..........**********************................................   ",
				"   *****************************************..........**********************................................   ",
				"   *****************************************..........**********************................................   ",
				"?  ****************************************.........************************................................?  ",
				"   *****************************************.........***********************................................   ",
				"   *****************************************.........***********************................................   ",
				"   ****************************************..........***********************................................   ",
				"   *****************************************...........*********************................................   ",
				"?  ******************************************..........*********************................................?  ",
				"   *********************************************...........*****************................................   ",
				"   **********************************************..........*****************................................   ",
				"   **********************************************..........*****************................................   ",
				"   *********************************************............****************................................   ",
				"?  **************************##########***###******........*****************................................?  ",
				"****************************##~~~~~~~~#####~########.........***************................................   ",
				"***************************##~~~~~~~~~~~~~~~~~~~~~~~##**..........**********...................................",
				" *************************#~~~~~~~~~~~~~~~~~~~~~~~~~~#***.........**********...................................",
				"#################********#~~~~~~~~~~~~~~~~~~~~~~~~~##****.........**********.............####............##....",
				"~~~~~~~~~~~~~~~~#********#~~~~~~~~~~~~~~~~~~~~~~~~#*******...........................####~~~~#..##..#####~~####",
				"~~~~~~~~~~~~~~~~##*******#~~~~~~~~~~~~~~~~~~~~~~~~##*****..........................##~~~~~~~~~##~~##~~~~~~~~?..",
				"~~~~~~~~~~~~~~~~~#*******#~~~~~~~~~~~~~~~~~~~~~~~~#*********......................#~~~~~~~~~~~~~~~~~~~~~~~~~...",
				"~~~~~~~~~~~~~~~~~#*******#~~~~~~~~~~~~~~~~~~~~~~~~#*********......................#~~~~~~~~~~~~~~~~~~~~~~~~~...",
				"~~~~~~~~~~~~~~~~~#########~~~~~~~~~~~~~~~~~~~~~~~~#*********......................#~~~~~~~~~~~~~~~~~~~~~~~~~...",
				"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#***************................#~~~~~~~~~~~~~~~~~~~~~~~~~...",
				"#############~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#***************................#~~~~~~~~~~~~~~~~~~~~~~~~~...",
				"*************###~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~##****************................#~~~~~~~~~~~~~~~~~~~~~~~~~...",
				"?  ************#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~##****************.................##~~~~~~~~~~~~~~~~~~~~~~~?  ",
				" ***************#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#****************.................#~~~~~~~~~~~~~~~~~~~~~~~~   ",
				" ***************#########~~~~~~~~~~~~~~~~~~~~~~~~#****************..................#~~~~~~~~~~~~~~~~~~~~~~~   ",
				" ***********************##~~~~~~~~~~~~~~~~~~~~~~~#****************..................#~~~~~~~~~~~~~~~~~~~~~~~   ",
				" ************************####~~~~~~~~~~~~~~~~~~~~#************......................#~~~~~~~~~~~~~~~~~~~~~~~   ",
				"?  ************************##~~~~~~~~~~~~~~~~~~~#***********....**..................#~~~~~~~~~~~~~~~~~~~~~~~?  ",
				"****************************#~~~~~~~~~~~~~~~~~~~#************....*.................##~~~~~~~~~~~~~~~~~~~~~~~   ",
				"****************************#~~~~~~~~~~~~~~~~~~##*********.......*................#~~~~~~~~~~~~~~~~~~~~~~~~~   ",
				"****************************##~~~####~~~~#######**********.......*................#~~~~~~~~~~~~~~~~~~~~~~~~~   ",
				"*****************************##~##**######*************.........**................#~~~~~~~~~~~~~~~~~~~~~~~~~   ",
				"?  ***************************###**********************........***.................##~~~~~~~~~~~~~~~~~~~~~~~?  ",
				"*******************............*****........*****..............***..................#~~~~~~~~~~~~~~~~~~~~~~~   ",
				"******************...........................................*****..................#~~~~~~~~~~~~~~~~~~~~~~~   ",
				"****************.............................................*****...................##~~~~~~~~~#####~~#####   ",
				"***************..............................................*****.....................######~~##....##.....   ",
				"?**************............................................*******...........................##.............?  ",
				"************...............................................*******..........................................   ",
				"************.........******...........************.........*******..........................................   ",
				"**********...........*********************************************..........................................   ",
				"**********...........*********************************************..............................X...........   ",
				"?*********...........*********************************************......X...................................?  ",
				"**********........************************************************..........................................   ",
				"*********.........************************************************..........................................   ",
				"*********.......**************************************************..........................................   ",
				"*********.......**************************************************..........................................   ",
				"?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  ?  "
			};
		private string? _imageMap;  // create a new string array for letters map
		private short[]? _imageColors;
		private SpriteList _spriteList = new SpriteList(); // list of sprites

		public int Width { get; private set; }
		public int Height { get; private set; }


		public SpriteList GetSprites() { return _spriteList; } // Get the Sprites of the map
	   
		public string GetImageMap() { return _imageMap; }  // Get the chars of the map
		public short[] GetImageColors() { return _imageColors; }  // Get the chars of the map



		public void Load()
		{
			// create a temp 2d array of chars 
			char[,] _temp = new char[_mapLoaded.Length, _mapLoaded[0].Length + 1];
			short[,] _tempColor = new short[_mapLoaded.Length, _mapLoaded[0].Length];

			Width = _mapLoaded[0].Length;
			Height = _mapLoaded.Length;

			// Sprites
			Sprite player = Sprite.GetSprite("WARRIOR");
			Sprite tree = Sprite.GetSprite("Tree");

			// Double for loop go through  _mapLoaded -> add sprites to the list --- add characters to _imageMap
			for (int row = 0; row < _mapLoaded.Length; row++)
			{
				for (int column = 0; column < _mapLoaded[0].Length; column++)
				{
					switch (_mapLoaded[row][column])
					{
						// if index is a tree
						case '?':
							_spriteList.Add(new Tuple<Sprite, int, int>(Sprite.GetSprite("Tree"), column, row));
							break;

						// if index is a grass
						case '*':
							_temp[row, column] = '#';
							_tempColor[row, column] = 0x2a;
							break;

						// if index is a rock
						case '+':
							_temp[row, column] = '+';
							break;

						// if index is water
						case '~':
							_temp[row, column] = '░';
							_tempColor[row, column] = 0x31;
							break;

						// if index is a wall
						case '#':
							_temp[row, column] = '█';
							_tempColor[row, column] = 0x07;
							break;

						// if index is a house
						case 'X':
							_spriteList.Add(new Tuple<Sprite, int, int>(Sprite.GetSprite("House"), column, row));
							break;

						// if index is a player spawn
						case '@':
							_spriteList.Add(new Tuple<Sprite, int, int>(Sprite.GetSprite("WARRIOR"), column, row));
							break;

						// if index is a npc spawn
						case '!':
							_temp[row, column] = '!';
							break;

						// if nothing is there
						case '.':
							_temp[row, column] = ' ';
							break;
					}
				}
			}

			// Sending _temp to _imageMap
			int elemCount = _mapLoaded.Length * _mapLoaded[0].Length * 2;

			StringBuilder sb = new StringBuilder(elemCount);
			_imageColors = new short[elemCount];

			for (int i = 0; i < _mapLoaded.Length; i++) // _mapLoaded for number of lines
			{
				for (int j = 0; j < _mapLoaded[0].Length; j++)
				{
					sb.Append(_temp[i, j]);
					sb.Append(_temp[i, j]);
					_imageColors[i * _mapLoaded[0].Length * 2 + j * 2] = _tempColor[i, j];
					_imageColors[i * _mapLoaded[0].Length * 2 + j * 2 + 1] = _tempColor[i, j];
				}
			}

			_imageMap = sb.ToString();
		}

		public char GetTileAt(int x, int y)
		{
			if (x < 0 || x >= Width || y < 0 || y >= Height) return '\0';
			return _imageMap[y * Width * 2 + x * 2];
		}

		public static bool IsCollidable(char tileType)
		{
			switch (tileType)
			{
				case '█':
				case '?':
				case 'X':
					return true;

				default:
					return false;
			}
		}

		public bool IsCollidable(int x, int y)
		{
			char tile = GetTileAt(x, y);
			return (tile == '\0') ? false : IsCollidable(tile);
		}
	}
}
