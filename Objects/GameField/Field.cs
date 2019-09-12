using System;
using SnakeExtreme.Globals;
using SnakeExtreme.Objects.Scene;

namespace SnakeExtreme.Objects.GameField
{
	/// <summary>
	/// The Field class.
	/// </summary>
	public class Field
	{
		public int BorderWidth { get; set; }
		public int OffsetX { get; }
		public int OffsetY { get; }
		public int Width { get; }
		public int Height { get; }
		private int[] content;

		public void Clear()
		{
			this.content = new int[this.Width * this.Height];
		}

		/// <summary>
		/// Gets a tile at the specific position.
		/// </summary>
		/// <param name="posX">The x postion of the tile.</param>
		/// <param name="posY">The y postion of the tile.</param>
		/// <returns>The tile id at the specific position.</returns>
		public int GetTile(int posX, int posY)
		{	
			if (posX < 0)
			{
				posX += this.Width;
			}

			if (posY < 0)
			{
				posY += this.Height;
			}

			posX %= this.Width;
			posY %= this.Height;

			return this.content[posX + posY * this.Width];
		}

		/// <summary>
		/// Sets a tile at the specific position.
		/// </summary>
		/// <param name="posX">The x position of the tile.</param>
		/// <param name="posY">The y position of the tile.</param>
		/// <param name="tile">The tile id to set.</param>
		public void SetTile(int posX, int posY, int tile)
		{
			if (posX < 0)
			{
				posX += this.Width;
			}

			if (posY < 0)
			{
				posY += this.Height;
			}

			posX %= this.Width;
			posY %= this.Height;

			this.content[posX + posY * this.Width] = tile;
		}

		/// <summary>
		/// Draws the border of the field.
		/// </summary>
		public void DrawBorder()
		{
			Console.SetCursorPosition(this.OffsetX, this.OffsetY);
			Console.BackgroundColor = ConsoleColor.White;

			for (int i = 0; i < (this.Width + this.BorderWidth * 2); i++)
			{
				Console.Write(Global.Tile);
			}

			Console.SetCursorPosition(this.OffsetX, this.Height + this.BorderWidth + this.OffsetY);

			for (int i = 0; i < (this.Width + this.BorderWidth * 2); i++)
			{
				Console.Write(Global.Tile);
			}

			for (int i = 0; i < (this.Height + this.BorderWidth * 2); i++)
			{
				Console.SetCursorPosition(this.OffsetX, i + this.OffsetY);
				Console.Write(Global.Tile);
			}

			for (int i = 0; i < (this.Height + this.BorderWidth * 2); i++)
			{
				Console.SetCursorPosition(this.Width + this.BorderWidth + this.OffsetY, i + this.OffsetY);
				Console.Write(Global.Tile);
			}
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="width">The width of the field.</param>
		/// <param name="height">The height of the field.</param>
		public Field(int width, int height)
		{
			this.Width = width;
			this.Height = height;
			this.BorderWidth = 1;
			this.content = new int[this.Width * this.Height];
		}

		/// <summary>
		/// Constructor with offset.
		/// </summary>
		/// <param name="width">The width of the field.</param>
		/// <param name="height">The height of the field.</param>
		/// <param name="offsetX">The horizontal offset of the field.</param>
		/// <param name="offsetY">The vertikal offset of the field.</param>
		public Field(int width, int height, int offsetX, int offsetY)
		{
			this.Width = width;
			this.Height = height;
			this.BorderWidth = 1;
			this.OffsetX = offsetX;
			this.OffsetY = offsetY;
			this.content = new int[this.Width * this.Height];
		}
	}
}
