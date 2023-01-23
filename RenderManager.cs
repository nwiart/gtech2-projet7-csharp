using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
	internal class RenderManager
	{
		private char[] backBuffer;
		private int bufferWidth;
		private int bufferHeight;

		public RenderManager(int backBufferWidth, int backBufferHeight)
		{
			this.backBuffer = new char[backBufferWidth * backBufferHeight];
			this.bufferWidth = backBufferWidth;
			this.bufferHeight = backBufferHeight;

			CameraPosX = 0;
			CameraPosY = 0;
		}

		public void clear()
		{
			// Fill buffer with spaces.
			Array.Fill(backBuffer, ' ');

			Console.CursorVisible = false;
		}

		public void swapBuffers()
		{
			// Write back buffer to console.
			Console.SetCursorPosition(0, 0);
			Console.Write(backBuffer);
		}

		public int CameraPosX { get; set; }
		public int CameraPosY { get; set; }

		public bool isOutOfBoundsX(int posX)
		{
			return posX < 0 || posX >= bufferWidth;
		}

		public bool isOutOfBoundsY(int posY)
		{
			return posY < 0 || posY >= bufferHeight;
		}

		public void renderHLine(int posX, int posY, int length, char c)
		{
			// Transform point.
			posX -= CameraPosX; posY += CameraPosY;

			if (isOutOfBoundsY(posY)) return;

			for (int i = 0; i < length; ++i)
			{
				int x = i + posX;
				if (isOutOfBoundsX(x)) continue;

				backBuffer[bufferWidth * posY + x] = c;
			}
		}

		public void renderString(int posX, int posY, string text)
		{
			// Out of bounds check.
			if (isOutOfBoundsY(posY)) return;

			for (int i = 0; i < text.Length; ++i)
			{
				// Out of bounds.
				int x = i + posX;
				if (isOutOfBoundsX(x)) continue;

				backBuffer[bufferWidth * posY + x] = text[i];
			}
		}
	}
}
