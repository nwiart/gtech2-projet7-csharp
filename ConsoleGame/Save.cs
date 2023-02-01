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
	}
}
