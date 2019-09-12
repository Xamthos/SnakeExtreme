using System;
using SnakeExtreme.Globals;
using SnakeExtreme.Objects.Scene;
using System.Threading;

namespace SnakeExtreme.Objects.Entity
{
	/// <summary>
	/// Snake tile class.
	/// </summary>
	public class SnakeTile
	{
		public int PosX { get; set; }
		public int PosY { get; set; }

		/// <summary>
		/// Draws a SnakeTile at the position and with the color of the tile.
		/// </summary>
		/// <param name="color">Tile color</param>
		public void Draw(ConsoleColor color)
		{
			Console.SetCursorPosition(this.PosX + Game.field.OffsetX + Game.field.BorderWidth, this.PosY + Game.field.OffsetY + Game.field.BorderWidth);
			Console.BackgroundColor = color;
			Console.Write(Global.Tile);
		}

		/// <summary>
		/// Draws the head with a specific headtile.
		/// </summary>
		/// <param name="color">Head color</param>
		/// <param name="headTile">Head tile</param>
		public void DrawHead(ConsoleColor color, char headTile)
		{
			Console.SetCursorPosition(this.PosX + Game.field.OffsetX + Game.field.BorderWidth, this.PosY + Game.field.OffsetY + Game.field.BorderWidth);
			Console.BackgroundColor = color;
			Console.WriteLine(headTile);
		}

		/// <summary>
		/// Animation of a dead snake tile.
		/// </summary>
		/// <param name="color"></param>
		public void Explode(ConsoleColor color)
		{
			Console.SetCursorPosition(this.PosX + Game.field.OffsetX + Game.field.BorderWidth, this.PosY + Game.field.OffsetY + Game.field.BorderWidth);
			Console.ForegroundColor = color;
			Console.Write(Global.SlighlyDamagedTile);

			Thread.Sleep(8);

			Console.SetCursorPosition(this.PosX + Game.field.OffsetX + Game.field.BorderWidth, this.PosY + Game.field.OffsetY + Game.field.BorderWidth);
			Console.Write(Global.DamagedTile);

			Thread.Sleep(8);

			Console.SetCursorPosition(this.PosX + Game.field.OffsetX + Game.field.BorderWidth, this.PosY + Game.field.OffsetY + Game.field.BorderWidth);
			Console.Write(Global.VeryDamagedTile);

			Thread.Sleep(8);

			Console.SetCursorPosition(this.PosX + Game.field.OffsetX + Game.field.BorderWidth, this.PosY + Game.field.OffsetY + Game.field.BorderWidth);
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(Global.Tile);

			Global.snakeTileDamage.PlaySync();
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public SnakeTile(int posX, int posY)
		{
			this.PosX = posX;
			this.PosY = posY;
		}
	}
}
