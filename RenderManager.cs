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
		private int _bufferWidth;
		private int _bufferHeight;

		public int CameraPosX { get; set; }
		public int CameraPosY { get; set; }



		public RenderManager(int backBufferWidth, int backBufferHeight)
		{
			_backBuffer = new char[backBufferWidth * backBufferHeight];
			_bufferWidth = backBufferWidth;
			_bufferHeight = backBufferHeight;

			CameraPosX = 0;
			CameraPosY = 0;
		}

		private void handleResize(int newWidth, int newHeight)
		{
			_backBuffer = new char[newWidth * newHeight];
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

			Console.CursorVisible = false;
		}

		public void SwapBuffers()
		{
			// Write back buffer to console.
			Console.SetCursorPosition(0, 0);
			Console.Write(_backBuffer);
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
				int baseIndex = (posY + r) * _bufferWidth + posX;
				for (int c = 0; c < sizeX; ++c)
				{
					char ch = data[r * sizeX + c];
					if (ch != ' ')
						_backBuffer[baseIndex + c] = data[r * sizeX + c];
				}
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
