using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleGame
{
	public class Save
	{
		public static readonly string SAVE_FILENAME = "save.txt";

		public static bool Exists()
		{
			return File.Exists(SAVE_FILENAME);
		}

		public static void LoadProgress()
		{
			using (FileStream file = File.OpenRead(SAVE_FILENAME))
			{
				Inventory.Inventory i = JsonSerializer.Deserialize<Inventory.Inventory>(file);
				Player.Instance.Inventory = i;
			}
		}

		public static void SaveProgress()
		{
			JsonSerializerOptions opt = JsonSerializerOptions.Default;

			string s = JsonSerializer.Serialize(Player.Instance.Inventory, opt);
			File.WriteAllText(SAVE_FILENAME, s);
		}
	}
}
