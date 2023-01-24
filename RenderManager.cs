using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
	internal class RenderManager
	{
		private char[] _backBuffer;
		private short[] _backColors;
		private int _bufferWidth;
		private int _bufferHeight;

		public short CurrentColor { get; set; }

		public int CameraPosX { get; set; }
		public int CameraPosY { get; set; }



		public RenderManager(int backBufferWidth, int backBufferHeight)
		{
			_backBuffer = new char[backBufferWidth * backBufferHeight];
			_backColors = new short[backBufferWidth * backBufferHeight];
			_bufferWidth = backBufferWidth;
			_bufferHeight = backBufferHeight;

			CameraPosX = 0;
			CameraPosY = 0;
		}

		private void handleResize(int newWidth, int newHeight)
		{
			_backBuffer = new char[newWidth * newHeight];
			_backColors = new short[newWidth * newHeight];
			_bufferWidth = newWidth;
			_bufferHeight = newHeight;
		}

		public void Clear()
		{
			// Console window changed size.
			if (Console.WindowWidth != _bufferWidth || Console.WindowHeight != _bufferHeight)
			{
				handleResize(Console.WindowWidth, Console.WindowHeight);
			}

			// Fill buffer with spaces.
			Array.Fill(_backBuffer, ' ');
			Array.Fill(_backColors, (short) 0x0f);

			Console.CursorVisible = false;
		}

		public void SwapBuffers()
		{
			// Write back buffer to console.
			Console.SetCursorPosition(0, 0);
			Console.Write(_backBuffer);

			// Submit colors.
			IntPtr stdHandle = Win32.GetStdHandle(Win32.STD_OUTPUT_HANDLE);
			int num = 0;
			Win32.WriteConsoleOutputAttribute(stdHandle, _backColors, _bufferWidth * _bufferHeight, new Win32.COORD(0, 0), ref num);
		}

		public void SetForegroundColor(ConsoleColor c)
		{

		}

		public void SetBackgroundColor(ConsoleColor c)
		{

		}

		public void RenderHLine(int posX, int posY, int length, char c)
		{
			// Transform point.
			WorldToConsole(ref posX, ref posY);

			if (IsOutOfBoundsY(posY)) return;

			for (int i = 0; i < length; ++i)
			{
				int x = i + posX;
				if (IsOutOfBoundsX(x)) continue;

				_backBuffer[_bufferWidth * posY + x] = c;
			}
		}

		public void RenderImage(int posX, int posY, int sizeX, int sizeY, string data)
		{
			// Transform point.
			WorldToConsole(ref posX, ref posY);

			for (int r = 0; r < sizeY; ++r)
			{
				// Clip testing Y.
				int row = r + posY;
				if (row < 0) continue;
				if (row >= _bufferHeight) return;

				// Clip testing X.
				int baseIndexY = row * _bufferWidth;

				for (int c = 0; c < sizeX; ++c)
				{
					int indexX = c + posX;
					if (indexX < 0) continue;
					if (indexX >= _bufferWidth) break;

					char ch = data[r * sizeX + c];
					if (ch != ' ')
					{
						_backBuffer[baseIndexY + indexX] = ch;
						_backColors[baseIndexY + indexX] = CurrentColor;
					}
				}
			}
		}

		public void RenderSprite(int posX, int posY, Sprite sprite)
		{
			for (int i = 0; i < sprite.Height; ++i)
			{
				CurrentColor = (short) sprite.GetRowColor(i);

				string row = sprite.GetRow(i);
				RenderImage(posX, posY + i, row.Length, 1, row);
			}
		}

		public void RenderString(int posX, int posY, string text)
		{
			// Out of bounds check.
			if (IsOutOfBoundsY(posY)) return;

			for (int i = 0; i < text.Length; ++i)
			{
				// Out of bounds.
				int x = i + posX;
				if (IsOutOfBoundsX(x)) continue;

				_backBuffer[_bufferWidth * posY + x] = text[i];
			}
		}


		
		// Helper functions.
		public bool IsOutOfBoundsX(int posX)
		{
			return posX < 0 || posX >= _bufferWidth;
		}

		public bool IsOutOfBoundsY(int posY)
		{
			return posY < 0 || posY >= _bufferHeight;
		}

		public void WorldToConsole(ref int posX, ref int posY)
		{
			// Move two characters along X to make square movements.
			posX = posX * 2 - CameraPosX * 2 + _bufferWidth / 2;
			posY = posY - CameraPosY + _bufferHeight / 2;
		}
	}
}
