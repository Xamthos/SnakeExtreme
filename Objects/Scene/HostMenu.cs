using System;
using SnakeExtreme.Globals;
using SnakeExtreme.Objects.UI;

namespace SnakeExtreme.Objects.Scene
{
	/// <summary>
	/// Menu for configure and start a network game.
	/// </summary>
	public class HostMenu : Scene
	{

		public ConsolePanel panel;
		public override void Init()
		{
			this.panel.Reset();
		}

		public override void Update()
		{
			this.panel.Update();

			if (this.panel.Confirmed)
			{
				switch (this.panel.Index)
				{
					case 3:
						((ConsoleString)this.panel.GetSelectedObject()).WriteMode = true;
						this.panel.Confirmed = false;
						break;
					case 4:
						Global.gameName = ((ConsoleString)this.panel.Elements[3]).Value;
						this.ChangeScene("Hosting");
						break;
					case 5:
						Option.fieldWidth = ((ConsoleInt)this.panel.Elements[0]).Value;
						Option.fieldHeight = ((ConsoleInt)this.panel.Elements[1]).Value;
						Option.gameSpeed = ((ConsoleInt)this.panel.Elements[2]).Value;
						this.ChangeScene("NetworkGameMenu");
						break;
					default:
						this.panel.Confirmed = false;
						break;
				}
			}
		}


		public HostMenu()
		{
			this.panel = new ConsolePanel(Console.WindowWidth / 2, Console.WindowHeight / 2, 5, 7, "Game Options",
			new ConsoleInt("Field Width", Option.fieldWidth, 16, 99, 2),
			new ConsoleInt("Field Height", Option.fieldHeight, 16, 99, 2),
			new ConsoleInt("Speed", Option.gameSpeed, 1, 12, 2),
			new ConsoleString("GameName"),
			new ConsoleButton("Start"),
			new ConsoleButton("Back")
			);
		}
	}
}