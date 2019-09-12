using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SnakeExtreme.Globals;
using SnakeExtreme.Objects.UI;

namespace SnakeExtreme.Objects.Scene
{
	/// <summary>
	/// Scene for the game hosting.
	/// </summary>
	public class Hosting : Scene
	{
		private ConsolePanel panel;
		
		private Socket socket;

		private TcpListener server;
		private List<TcpClient> clients;
		private IPAddress iPAddress;
		private IPAddress broadcast;

		private Thread connectionThread;

		private string gameName;

		private bool gameStarts = false;

		/// <summary>
		/// Initializaition
		/// </summary>
		public override void Init()
		{
			this.panel.Reset();
			this.gameName = Global.gameName;
			Console.SetCursorPosition(0,10);
			Console.WriteLine(broadcast);
			Console.WriteLine("Gamename: " + this.gameName);
			Console.WriteLine("Waiting for players...");
			connectionThread.Start();
		}

		public override void Update()
		{
			this.panel.Update();

			if (this.panel.Confirmed)
			{
				switch(this.panel.Index)
				{
					case 0:
						((Game)Global.scenes["Game"]).isNetworkGame = true;
						Global.connectionToClients = this.clients;
						Global.socket = this.socket;
						Global.broadcast = this.broadcast;
						this.ChangeScene("Game");
						this.gameStarts = true;
						break;
					case 1:
						this.server.Stop();
						this.ChangeScene("HostMenu");
						break;
					default:
						this.panel.Confirmed = false;
						break;
				}
			}

			SendServerDataToClients();

		}

		/// <summary>
		/// Sends server data to clients.
		/// </summary>
		public void SendServerDataToClients()
		{
			List<byte> bytes = new List<byte>();

			bytes.Add((byte)this.gameName.Length);

			bytes.AddRange(Encoding.ASCII.GetBytes(this.gameName));

			bytes.Add((byte)this.iPAddress.ToString().Length);

			bytes.AddRange(Encoding.ASCII.GetBytes(this.iPAddress.ToString()));

			byte[] sendbuf = bytes.ToArray();

			IPEndPoint ep = new IPEndPoint(this.broadcast, Global.PORT);

			this.socket.SendTo(sendbuf, ep);
		}

		/// <summary>
		/// Recives client data.
		/// </summary>
		public void ReciveClientDataForConnecting()
		{
			try
			{
				this.server.Start();
			}
			catch(SocketException)
			{
				Console.Write("You can only have one hosted game... sorry.");
				return;
			}

			while (!this.gameStarts)
			{
				byte[] data = new byte[256];

				TcpClient client = server.AcceptTcpClient();
				this.clients.Add(client);

				Console.WriteLine("New client connected");
			}
		}

		/// <summary>
		/// Gets the local IPAdress.
		/// </summary>
		/// <returns></returns>
		private IPAddress GetLocalAdress()
		{
			IPAddress[] host = Dns.GetHostAddresses(Dns.GetHostName());
			foreach (IPAddress ip in host)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip;
				}
			}
			return null;
		}

		public Hosting()
		{
			this.iPAddress = GetLocalAdress();
			this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			byte[] adressBytes = this.iPAddress.GetAddressBytes();
			adressBytes[3] = 255;
			this.broadcast = new IPAddress(adressBytes);
			this.server = new TcpListener(this.iPAddress, Global.PORT);
			this.clients = new List<TcpClient>();
			this.connectionThread = new Thread(new ThreadStart(this.ReciveClientDataForConnecting));
			this.panel = new ConsolePanel(0, 0, 5, 4, "NetGame", new ConsoleButton("Start"), new ConsoleButton("Cancel"));
		}
	}
}
