using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Input;
using SnakeExtreme.Globals;
using SnakeExtreme.Objects.Entity;
using SnakeExtreme.Objects.GameField;

namespace SnakeExtreme.Objects.Scene
{
	/// <summary>
	/// Main game class for updating and rendering the gameplay.
	/// </summary>
	public class Game : Scene
	{
		public static List<Snake> snakes;
		private List<Food> foods;
		private int foodFrameCooldown = 0;

		public bool isNetworkGame = false;

		public static Field field;

		public Random rand;

		public List<Thread> playerThreads;

		/// <summary>
		/// Initialize game data.
		/// </summary>
		public override void Init()
		{
			field = new Field(Option.fieldWidth, Option.fieldHeight, 0, 1);

			Snake.snakeCount = 0;

			if (!this.isNetworkGame)
			{
				snakes.Add(new Snake(1, 1, EDirection.RIGHT, ConsoleColor.Red, Key.W, Key.A, Key.S, Key.D));
				if (Option.playerCount > 1) snakes.Add(new Snake(field.Width - 1, 1, EDirection.LEFT, ConsoleColor.DarkGreen, Key.I, Key.J, Key.K, Key.L));
				if (Option.playerCount > 2) snakes.Add(new Snake(1, field.Height - 1, EDirection.RIGHT, ConsoleColor.Blue, Key.Up, Key.Left, Key.Down, Key.Right));
				if (Option.playerCount > 3) snakes.Add(new Snake(field.Width - 1, field.Height - 1, EDirection.LEFT, ConsoleColor.Yellow, Key.NumPad8, Key.NumPad4, Key.NumPad5, Key.NumPad6));
			}
			else
			{
				snakes.Add(new Snake(1, 1, EDirection.RIGHT, ConsoleColor.Red, Key.W, Key.A, Key.S, Key.D));
				int i = 0;
				foreach (TcpClient client in Global.connectionToClients)
				{
					snakes.Add(new Snake(1, 3 * i, EDirection.RIGHT, ConsoleColor.Blue, networkPlayer: true));
					this.playerThreads.Add(new Thread(new ParameterizedThreadStart(this.NetworkPlayerInputReceive)));
					this.playerThreads[i].Start(snakes[i + 1]);
					i++;
				}
			}

			if (Global.secretMode)
			{
				Global.Tile = "ÿ";
			}

			field.DrawBorder();
		}

		/// <summary>
		/// Gets the inputs from connected client and sets the snake direction.
		/// </summary>
		/// <param name="player"></param>
		private void NetworkPlayerInputReceive(object player)
		{
			Snake snake = (Snake)player;
			while(snake != null)
			{
				byte[] data = new byte[256];
				Global.connectionToClients[snake.ID - 2].Client.Receive(data);
				switch(data[0])
				{
					case 1:
						snake.direction = EDirection.UP;
						break;
					case 2:
						snake.direction = EDirection.LEFT;
						break;
					case 3:
						snake.direction = EDirection.DOWN;
						break;
					case 4:
						snake.direction = EDirection.RIGHT;
						break;
				}
			}
		}

		/// <summary>
		/// Updates the game.
		/// </summary>
		public override void Update()
		{

			Console.SetCursorPosition(0,0);
			Console.BackgroundColor = ConsoleColor.Black;
			for (int i = 0; i < Option.playerCount; i++)
			{
				int length = 0;

				try
				{
					length = snakes[i].Length();
				}
				catch (ArgumentOutOfRangeException)
				{
					length = 0;
				}

				Console.Write("Player" + (i + 1)+ ":" + length + "\t");
			}

			if (snakes.Count == 0 && !this.isNetworkGame)
			{
				field.Clear();
				this.ChangeScene("GameOver");
				return;
			}

			if (Option.playerCount > 1 && !this.isNetworkGame)
			{
				if (snakes.Count == 1)
				{
					field.Clear();
					((Win)Global.scenes["Win"]).winBox.Name = "Player" + snakes[0].ID + " Wins!";
					snakes.Clear();
					this.ChangeScene("Win");
					return;
				}
			}

			if (this.isNetworkGame)
			{
				if(snakes.Count == 1)
				{
					field.Clear();
					snakes.Clear();
					this.playerThreads.Clear();
					this.ChangeScene("Hosting");
				}
			}

			// Snake death updating
			for (int i = snakes.Count - 1; i >= 0; i--)
			{

				if (snakes[i].isDeath ||
				snakes[i].Length() == 0 ||
				snakes[i].GetHeadTile().PosX < 0||
				snakes[i].GetHeadTile().PosY < 0||
				snakes[i].GetHeadTile().PosX > field.Width||
				snakes[i].GetHeadTile().PosY > field.Height - 1
				)
				{
					int length = snakes[i].Length();
					for (int j = 0; j < length; j++)
					{
						SnakeTile removedTile = snakes[i].RemoveTile();
						removedTile.Explode(snakes[i].Color);
						field.SetTile(removedTile.PosX, removedTile.PosY, 0);
					}
					snakes.RemoveAt(i);
					Global.snakeDeath.Play();
					field.DrawBorder();
				}
			}

			//Snake updating
			foreach (Snake snake in snakes)
			{	
				snake.Update();
			}

			//// Food updating
			//for (int i = this.foods.Count - 1; i >= 0; i--)
			//{
			//	foods[i].Update();
			//	if (foods[i].Cooldown <= 0)
			//	{
			//		Console.ForegroundColor = ConsoleColor.Black;
			//		Console.SetCursorPosition(foods[i].PosX + field.OffsetX + field.BorderWidth, foods[i].PosY + field.OffsetY + field.BorderWidth);
			//		Console.Write(Global.Tile);
			//		foods.RemoveAt(i);
			//	}
			//}
			
			foodFrameCooldown++;

			if(foodFrameCooldown == Option.fruitLifeTime / Option.maxFruits)
			{
				Food food = RandomFood();
				this.foods.Add(food);

				if (food.IsPoisoned)
				{
					Console.BackgroundColor = ConsoleColor.DarkYellow;
				}
				else
				{
					Console.BackgroundColor = ConsoleColor.Green;
				}
				Console.SetCursorPosition(food.PosX + field.OffsetX + field.BorderWidth, food.PosY + field.OffsetY + field.BorderWidth);

				Console.Write(Global.Tile);

				foodFrameCooldown = 0;
			}

			if (this.isNetworkGame)
			{
				List<byte> snakedata = new List<byte>();

				snakedata.Add((byte)snakes.Count);

				foreach (Snake snake in snakes)
				{

					snakedata.Add((byte)snake.tiles.Count);
					snakedata.Add((byte)(snake.Length() * 2));
					foreach (SnakeTile tile in snake.tiles)
					{
						snakedata.Add((byte)tile.PosX);
						snakedata.Add((byte)tile.PosY);
					}
				}

				SendDataToAllClients(snakedata.ToArray());

			}
		}

		public void SendDataToAllClients(byte[] data)
		{
			foreach (TcpClient client in Global.connectionToClients)
			{
				Global.socket.SendTo(data, client.Client.LocalEndPoint);
			}
		}

		public Food RandomFood()
		{
			int posX = rand.Next(field.Width);
			int posY = rand.Next(field.Height);

			while (field.GetTile(posX, posY) != 0)
			{
				posX = rand.Next(field.Width);
				posY = rand.Next(field.Height);
			}

			bool isPoisoned = rand.Next() % 2 == 0;

			if (isPoisoned)
			{
				field.SetTile(posX, posY, -2);
			}
			else
			{
				field.SetTile(posX, posY, -1);
			}

			return new Food(posX, posY, isPoisoned, Option.fruitLifeTime);
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public Game()
		{
			this.foods = new List<Food>();
			snakes = new List<Snake>();
			this.rand = new Random();
			this.playerThreads = new List<Thread>();
		}
	}
}
