using System;
using SnakeExtreme.Globals;
using SnakeExtreme.Objects.UI;

namespace SnakeExtreme.Objects.Scene
{
	public class Menu : Scene
	{
		ConsolePanel startBox;

		/// <summary>
		/// Initialize the Menu.
		/// </summary>
		public override void Init()
		{
			this.startBox.Reset();
		}

		/// <summary>
		/// Updates the Menu.
		/// </summary>
		public override void Update()
		{
			this.startBox.Update();
			
			if (this.startBox.Confirmed)
			{
				switch (this.startBox.Index)
				{
					case 0:
						this.ChangeScene("Game");
						break;
					case 1:
						this.ChangeScene("Options");
						break;
					case 2:
						this.ChangeScene("NetworkGameMenu");
						break;
					case 3:
						this.Stop();
						Global.gameOn = false;
						break;
					default:
						this.startBox.Confirmed = false;
						break;
				}
			}
		}

		public Menu()
		{
			startBox = new ConsolePanel(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 3, 8, 5, "Menu", new ConsoleButton("Start"), new ConsoleButton("Options"), new ConsoleButton("Network Game"), new ConsoleButton("End"));
		}
	}
}
