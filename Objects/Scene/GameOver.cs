using System;
using SnakeExtreme.Objects.UI;
using SnakeExtreme.Globals;

namespace SnakeExtreme.Objects.Scene
{
	public class GameOver : Scene
	{
		ConsolePanel gameOverBox;
		
		public override void Init()
		{
			this.gameOverBox.Reset();
		}

		public override void Update()
		{
			this.gameOverBox.Update();

			if (this.gameOverBox.Confirmed)
			{
				switch (this.gameOverBox.Index)
				{
					case 0:
						this.ChangeScene("Game");
						break;
					case 1:
						this.ChangeScene("Menu");
						break;
					default:
						this.gameOverBox.Confirmed = false;
						break;
				}
			}
		}

		public GameOver()
		{
			gameOverBox = new ConsolePanel(Console.WindowWidth / 2 - 3, Console.WindowHeight / 2, 6, 3, "Game Over", new ConsoleButton("Retry"), new ConsoleButton("Menu"));
		}
	}
}