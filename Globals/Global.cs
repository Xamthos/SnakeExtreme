using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using SnakeExtreme.Objects.Scene;

namespace SnakeExtreme.Globals
{
	public class Global
	{
		public static Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();
		public static string Tile = " ";
		public static string SlighlyDamagedTile = "\u2593";
		public static string DamagedTile = "\u2592";
		public static string VeryDamagedTile = "\u2591";
		public static bool gameOn = true;
		public static bool secretMode = false;
		public static string directJoinIP = "";
		public const int PORT = 32000;
		public static IPAddress broadcast;
		public static IPAddress serverAddress;
		public static bool isSocketUsed = false;
		public static string gameName;

		public static TcpClient connectionToServer;
		public static List<TcpClient> connectionToClients = new List<TcpClient>();
		public static Socket socket;
		public static List<Socket> serverSockets = new List<Socket>();

		public static SoundPlayer menuSelect = new SoundPlayer(Properties.Resources.menuSelect);
		public static SoundPlayer menuMove = new SoundPlayer(Properties.Resources.menuMove);
		public static SoundPlayer menuNumber = new SoundPlayer(Properties.Resources.menuNumber);
		public static SoundPlayer eatFruit = new SoundPlayer(Properties.Resources.eatFruit);
		public static SoundPlayer eatPoisonedFruit = new SoundPlayer(Properties.Resources.eatPoisonedFruit);
		public static SoundPlayer snakeTileDamage = new SoundPlayer(Properties.Resources.snakeTileDamage);
		public static SoundPlayer snakeDeath = new SoundPlayer(Properties.Resources.snakeDeath);
	}
}
