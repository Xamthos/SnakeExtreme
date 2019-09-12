using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using SnakeExtreme.Input;
using SnakeExtreme.Globals;

namespace SnakeExtreme.Objects.Scene
{
	/// <summary>
	/// Scene for clients in networkgames.
	/// </summary>
	public class PlayNetworkGame : Scene
	{
		private Thread recieveThread;
		public override void Init()
		{
			this.recieveThread.Start();
		}

		public override void Update()
		{
			this.Send();
		}

		/// <summary>
		/// Sends inputs to the host.
		/// </summary>
		private void Send()
		{
			byte data = 0;
			if (Controls.NetGame.IsActionPressed("Up"))
			{
				data = 1;
			}
			else if (Controls.NetGame.IsActionPressed("Left"))
			{
				data = 2;
			}
			else if (Controls.NetGame.IsActionPressed("Down"))
			{
				data = 3;
			}
			else if (Controls.NetGame.IsActionPressed("Right"))
			{
				data = 4;
			}
			Global.connectionToServer.Client.Send(new byte[] { data });
			//Console.WriteLine($"Sending data [{data}].");
		}

		/// <summary>
		/// Recieive game data from the host. (Doesn't work yet)
		/// </summary>
		private void Receive()
		{
			while(true)
			{
				byte[] data = new byte[512];
				NetworkStream stream = Global.connectionToServer.GetStream();
				stream.Read(data, 0, data.Length);

				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;

				int snakeCount = data[0];
				int pointer = 1;
				while (pointer < data.Length)
				{
					int snakeID = data[pointer];
					pointer++;
					int snakeLength = data[pointer];
					pointer++;
					for (int i = 0; i < snakeLength; i++)
					{
						int x = data[pointer];
						pointer++;
						int y = data[pointer];
						pointer++;
						Console.SetCursorPosition(x + 1, y + 1);
						Console.Write(Global.Tile);
						pointer++;
					}
				}
				Console.Clear();
			}
		}

		public PlayNetworkGame()
		{
			this.recieveThread = new Thread(new ThreadStart(this.Receive));
		}
	}
}
