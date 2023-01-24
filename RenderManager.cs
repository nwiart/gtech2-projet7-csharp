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

		public int CameraPosX { get; set; }
		public int CameraPosY { get; set; }



		public RenderManager(int backBufferWidth, int backBufferHeight)
		{
			this.backBuffer = new char[backBufferWidth * backBufferHeight];
			this.bufferWidth = backBufferWidth;
			this.bufferHeight = backBufferHeight;

			CameraPosX = 0;
			CameraPosY = 0;
		}

		private void handleResize(int newWidth, int newHeight)
		{
			this.backBuffer = new char[newWidth * newHeight];
			this.bufferWidth = newWidth;
			this.bufferHeight = newHeight;
		}

		public void clear()
		{
			// Console window changed size.
			if (Console.WindowWidth != this.bufferWidth || Console.WindowHeight != this.bufferHeight)
			{
				this.handleResize(Console.WindowWidth, Console.WindowHeight);
			}

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

		public void renderHLine(int posX, int posY, int length, char c)
		{
			// Transform point.
			worldToConsole(ref posX, ref posY);

			if (isOutOfBoundsY(posY)) return;

			for (int i = 0; i < length; ++i)
			{
				int x = i + posX;
				if (isOutOfBoundsX(x)) continue;

				backBuffer[bufferWidth * posY + x] = c;
			}
		}

		public void renderImage(int posX, int posY, int sizeX, int sizeY, string data)
		{
			// Transform point.
			worldToConsole(ref posX, ref posY);

			for (int r = 0; r < sizeY; ++r)
			{
				int baseIndex = (posY + r) * bufferWidth + posX;
				for (int c = 0; c < sizeX; ++c)
				{
					char ch = data[r * sizeX + c];
					if (ch != ' ')
						backBuffer[baseIndex + c] = data[r * sizeX + c];
				}
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


		
		// Helper functions.
		public bool isOutOfBoundsX(int posX)
		{
			return posX < 0 || posX >= bufferWidth;
		}

		public bool isOutOfBoundsY(int posY)
		{
			return posY < 0 || posY >= bufferHeight;
		}

		public void worldToConsole(ref int posX, ref int posY)
		{
			// Move two characters along X to make square movements.
			posX = posX * 2 - CameraPosX * 2 + bufferWidth / 2;
			posY = posY - CameraPosY + bufferHeight / 2;
		}
	}
}
