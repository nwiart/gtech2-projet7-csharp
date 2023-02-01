using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			using (FileStream f = new FileStream(SAVE_FILENAME, FileMode.Open))
			{

			}
		}

		public static void SaveProgress()
		{
			using (FileStream f = new FileStream(SAVE_FILENAME, FileMode.OpenOrCreate))
			{
				
			}
		}
	}
}
