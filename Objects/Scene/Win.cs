using System;
using SnakeExtreme.Globals;
using SnakeExtreme.Objects.UI;

namespace SnakeExtreme.Objects.Scene
{
	public class Win : Scene
	{

		public ConsolePanel winBox;
		public override void Init()
		{
			this.winBox.Reset();
		}

		public override void Update()
		{
			this.winBox.Update();

			if (this.winBox.Confirmed)
			{
				switch (this.winBox.Index)
				{
					case 0:
						this.ChangeScene("Game");
						break;
					case 1:
						this.ChangeScene("Menu");
						break;
					default:
						this.winBox.Confirmed = false;
						break;
				}
			}
		}

		public Win()
		{
			this.winBox = new ConsolePanel(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2, 6, 3, "Win",
				new ConsoleButton("Retry"),
				new ConsoleButton("Menu")
			);
		}
	}
}
