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
		public static readonly int GWL_STYLE = -16;

		public static readonly int WS_MAXIMIZEBOX = 0x00010000;
		public static readonly int WS_THICKFRAME = 0x00040000;

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

		[DllImport("kernel32.dll")]
		public static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		public static extern int GetWindowLongA(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		public static extern int SetWindowLongA(IntPtr hWnd, int nIndex, int dwNewLong);
	}
}
