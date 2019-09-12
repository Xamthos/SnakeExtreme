using System;
using SnakeExtreme.Objects.UI;

namespace SnakeExtreme.Objects.Scene
{
	/// <summary>
	/// Scene for the network menu.
	/// </summary>
	public class NetworkGameMenu : Scene
	{

		private ConsolePanel networkMenuBox;

		public override void Init()
		{
			networkMenuBox.Reset();
		}

		public override void Update()
		{
			this.networkMenuBox.Update();

			if (this.networkMenuBox.Confirmed)
			{
				switch (this.networkMenuBox.Index)
				{
					case 0:
						this.ChangeScene("GameSearch");
						break;
					case 1:
						this.ChangeScene("HostMenu");
						break;
					case 2:
						this.ChangeScene("Menu");
						break;
					default:
						this.networkMenuBox.Confirmed = false;
						break;
				}
			}
		}

		public NetworkGameMenu()
		{
			this.networkMenuBox = new ConsolePanel(Console.WindowWidth / 2, Console.WindowHeight / 2, 10, 4, "Network Game", new ConsoleButton("Join"), new ConsoleButton("Host"), new ConsoleButton("Menu"));
		}
	}
}
