using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SnakeExtreme.Globals;
using SnakeExtreme.Input;
using SnakeExtreme.Objects.Scene;

namespace SnakeExtreme.Objects.Entity
{
	/// <summary>
	/// Main class for the Snake.
	/// </summary>
	public class Snake
	{
		public char headTile = '\u2592';
		public bool isDeath = false;
		public int ID { get; }
		public static int snakeCount = 0;
		public Queue<SnakeTile> tiles;
		public ConsoleColor Color { get; set; }

		public EDirection direction;

		public KeyMap controlls;

		public bool NetworkPlayer { get; set; }

		/// <summary>
		/// Returns the head tile.
		/// </summary>
		/// <returns></returns>
		public SnakeTile GetHeadTile()
		{
			return this.tiles.Last();
		}

		/// <summary>
		/// Removes the last tile of the snake and returns it.
		/// </summary>
		/// <returns></returns>
		public SnakeTile RemoveTile()
		{
			SnakeTile removedTile = this.tiles.Dequeue();
			return removedTile;
		}

		/// <summary>
		/// Adds a tile to the snake.
		/// </summary>
		/// <param name="tile"></param>
		public SnakeTile AddTile()
		{
			SnakeTile lastTile = this.tiles.Last();
			SnakeTile newTile = new SnakeTile(lastTile.PosX, lastTile.PosY);

			if (this.direction == EDirection.UP)
			{
				newTile.PosY--;
			}

			if (this.direction == EDirection.RIGHT)
			{
				newTile.PosX++;
			}

			if (this.direction == EDirection.DOWN)
			{
				newTile.PosY++;
			}

			if (this.direction == EDirection.LEFT)
			{
				newTile.PosX--;
			}

			this.tiles.Enqueue(newTile);

			if (Game.field.GetTile(newTile.PosX, newTile.PosY) > 0)
			{
				this.isDeath = true;
			}
			
			return newTile;
		}

		/// <summary>
		/// Returns the length of the snake.
		/// </summary>
		/// <returns></returns>
		public int Length()
		{
			return tiles.Count;
		}

		/// <summary>
		/// Updates the snake.
		/// </summary>
		public void Update()
		{
			if (!this.NetworkPlayer)
			{
				if (this.controlls.IsActionPressed("left") && direction != EDirection.RIGHT)
				{
					this.direction = EDirection.LEFT;
				}

				if (this.controlls.IsActionPressed("right") && direction != EDirection.LEFT)
				{
					this.direction = EDirection.RIGHT;
				}

				if (this.controlls.IsActionPressed("up") && direction != EDirection.DOWN)
				{
					this.direction = EDirection.UP;
				}

				if (this.controlls.IsActionPressed("down") && direction != EDirection.UP)
				{
					this.direction = EDirection.DOWN;
				}
			}

			SnakeTile newTile = this.AddTile();

			if(Game.field.GetTile(newTile.PosX, newTile.PosY) > 0)
			{
				int id = Game.field.GetTile(newTile.PosX, newTile.PosY);

				foreach (Snake snake in Game.snakes)
				{
					
					if (snake.ID == id)
					{
						if (snake.direction == EDirection.DOWN && this.direction == EDirection.UP ||
							snake.direction == EDirection.UP && this.direction == EDirection.DOWN ||
							snake.direction == EDirection.LEFT && this.direction == EDirection.RIGHT ||
							snake.direction == EDirection.RIGHT && this.direction == EDirection.LEFT
						)
						{
							if (this.Length() < snake.Length())
							{
								this.isDeath = true;
								snake.isDeath = false;
								return;
							}
							else
							{
								snake.isDeath = true;
								this.isDeath = false;
								return;
							}
						}
					}
				}
			}

			Console.ForegroundColor = ConsoleColor.Gray;
			this.tiles.Last().DrawHead(this.Color, this.headTile);
			if (this.tiles.Count > 1)
			{
				this.tiles.ElementAt(this.tiles.Count - 2).Draw(this.Color);
			}

			if (Game.field.GetTile(newTile.PosX, newTile.PosY) != -1)
			{
				SnakeTile removedTile = this.RemoveTile();
				removedTile.Draw(ConsoleColor.Black);
				Game.field.SetTile(removedTile.PosX, removedTile.PosY, 0);
				if (Game.field.GetTile(newTile.PosX, newTile.PosY) == -2)
				{
					removedTile = this.RemoveTile();
					removedTile.Draw(ConsoleColor.Black);
					Game.field.SetTile(removedTile.PosX, removedTile.PosY, 0);
					Global.eatPoisonedFruit.Play();
				}
			}
			else
			{
				Global.eatFruit.Play();
			}

			Game.field.SetTile(newTile.PosX, newTile.PosY, this.ID);

			
		}

		/// <summary>
		/// Creates a new snake with a color.
		/// </summary>
		/// <param name="color">Color of the snake.</param>
		public Snake(int posX, int posY, EDirection direction, ConsoleColor color = ConsoleColor.White, Key keyUp = Key.W, Key keyLeft = Key.A, Key keyDown = Key.S, Key keyRight = Key.D, bool networkPlayer = false)
		{
			this.Color = color;
			this.tiles = new Queue<SnakeTile>();
			this.tiles.Enqueue(new SnakeTile(posX, posY));
			this.controlls = new KeyMap();
			this.controlls.AddKeyBind("up", keyUp);
			this.controlls.AddKeyBind("left", keyLeft);
			this.controlls.AddKeyBind("down", keyDown);
			this.controlls.AddKeyBind("right", keyRight);
			this.direction = direction;
			snakeCount++;
			this.ID = snakeCount;
			this.NetworkPlayer = networkPlayer;
		}
	}
}
