using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGame
{
	public class Sprite
	{
		public int Width { get; }
		public int Height { get; }

		private string[] _rows;
		private int[] _colors;

		private static Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();


		public Sprite(int w, int h, string[] data, int[] colors)
		{
			Width = w;
			Height = h;
			_rows = data;
			_colors = colors;
		}

		public string GetRow(int index)
		{
			return _rows[index];
		}

		public int GetRowColor(int index)
		{
			return _colors[index];
		}

		public static Sprite GetSprite(string name)
		{
			return _sprites[name];
		}

		public static void LoadSprites()
		{
			string[] lines = System.IO.File.ReadLines(@"sprites.txt").ToArray();

			int i = 0;
			while (i < lines.Count())
			{
				if (lines[i].StartsWith("sprite"))
				{
					// Get sprite name.
					string spriteName = lines[i].Split(' ')[1];
					List<string> spriteData = new List<string>();
					List<int> spriteColors = new List<int>();

					i++;

					while (i < lines.Count() && lines[i].Length > 0)
					{
						// Color code.
						int color = int.Parse(lines[i].Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
						spriteColors.Add(color);

						// Sprite row.
						spriteData.Add(lines[i].Substring(2));

						i++;
					}

					Sprite sprite = new Sprite(0, spriteData.Count(), spriteData.ToArray(), spriteColors.ToArray());
					_sprites.Add(spriteName, sprite);
				}
				else
				{
					i++;
				}
			}
		}
	}
}