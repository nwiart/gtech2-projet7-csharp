using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
	internal class Win32
	{
		public struct COORD
		{
			short x;
			short y;

			public COORD(short x, short y)
			{
				this.x = x;
				this.y = y;
			}
		}

		public static int STD_OUTPUT_HANDLE = -11;

		[DllImport("kernel32.dll")]
		public static extern IntPtr GetStdHandle(int nStdHandle);

		[DllImport("kernel32.dll")]
		public static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, byte[] lpCharacter, int nLength, COORD dwWriteCoord, ref int lpNumberOfCharsWritten);

		[DllImport("kernel32.dll")]
		public static extern bool WriteConsoleOutputAttribute(IntPtr hConsoleOutput, short[] lpAttribute, int nLength, COORD dwWriteCoord, ref int lpNumberOfAttrsWritten);

		[DllImport("kernel32.dll")]
		public static extern bool SetConsoleTitle(string lpConsoleTitle);
	}
}
