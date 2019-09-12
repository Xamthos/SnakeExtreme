using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SnakeExtreme.Input;
using SnakeExtreme.Objects.Network;
using SnakeExtreme.Objects.UI;
using SnakeExtreme.Globals;

namespace SnakeExtreme.Objects.Scene
{
	public class GameSearch : Scene
	{
		private TcpClient tcpClient;
		private UdpClient udpClient;
		private IPEndPoint groupEP;
		private ConsolePanel panel;
		private List<string> ips;
		private Thread gameSearchThread;
		private bool gameSearch;

		public override void Init()
		{
			this.panel.Reset();
			this.panel.Elements.Clear();
			this.udpClient = new UdpClient(Global.PORT);
			this.gameSearchThread = new Thread(new ThreadStart(this.GameSearchUpdate));
			this.gameSearch = true;
			this.gameSearchThread.Start();
		}

		/// <summary>
		/// Updating
		/// </summary>
		public override void Update()
		{
			this.panel.Update();

			if (Controls.UI.IsActionPressed("UI_Reload"))
			{
				this.ips.Clear();
				this.panel.Reset();
				this.panel.Elements.Clear();
			}
			else if (Controls.UI.IsActionPressed("UI_Back"))
			{
				this.gameSearch = false;
				this.udpClient.Close();
				this.ChangeScene("NetworkGameMenu");
			}

			if (this.panel.Confirmed)
			{
				try
				{
					this.tcpClient = new TcpClient();
					Global.serverAddress = IPAddress.Parse(this.ips[this.panel.Index]);
					this.tcpClient.Connect(Global.serverAddress, Global.PORT);
					Global.connectionToServer = this.tcpClient;
					this.ChangeScene("PlayNetworkGame");
				}
				catch(SocketException)
				{
					Console.Write("You cannot connect to yourself... sorry.");
				}

				this.panel.Confirmed = false;
			}
		}

		/// <summary>
		/// Updates the aviable network games in the network.
		/// </summary>
		public void GameSearchUpdate()
		{
			while(this.gameSearch)
			{
				try
				{
					byte[] bytes = udpClient.Receive(ref groupEP);
					NetworkGame netGame = new NetworkGame();
					netGame.SetData(bytes);

					if (!ips.Contains(netGame.Ip))
					{
						this.panel.Elements.Add(new ConsoleButton(netGame.Name + "\t" + netGame.Ip));
						ips.Add(netGame.Ip);
					}

				}
				catch (SocketException e)
				{
					//Console.WriteLine(e);
				}
			}
		}

		public GameSearch()
		{
			this.ips = new List<string>();
			this.groupEP = new IPEndPoint(IPAddress.Any, Global.PORT);
			this.panel = new ConsolePanel(0, 0, 10, 10, "Games");
		}
	}
}
