using System;
using System.Net;
using System.Net.Sockets;
using SnakeExtreme.Globals;
using SnakeExtreme.Objects.UI;

namespace SnakeExtreme.Objects.Scene
{
	/// <summary>
	/// Scene for direct join to a open game (Not yet implemented).
	/// </summary>
	public class DirectJoin : Scene
	{
		public ConsolePanel panel;
		public override void Init()
		{
			this.panel.Reset();
			((ConsoleString)this.panel.Elements[0]).Value = Global.directJoinIP;
		}

		public override void Update()
		{
			this.panel.Update();

			if (this.panel.Confirmed)
			{
				switch (this.panel.Index)
				{
					case 0:
						this.ChangeScene("IPInput");
						break;
					case 1:
						break;
					case 2:
						this.ChangeScene("NetworkGameMenu");
						break;
					default:
						this.panel.Confirmed = false;
						break;
				}
			}
		}

		public DirectJoin()
		{
			this.panel = new ConsolePanel(Console.WindowWidth / 2, Console.WindowHeight / 2, 5, 4, "Join Game", new ConsoleString("IP"), new ConsoleButton("Connect"), new ConsoleButton("Cancel"));
		}
	}
}
