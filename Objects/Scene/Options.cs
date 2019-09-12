using System;
using SnakeExtreme.Globals;
using SnakeExtreme.Objects.UI;

namespace SnakeExtreme.Objects.Scene
{
	public class Options : Scene
	{
		public ConsolePanel options;
		
		public override void Init()
		{
			this.options.Reset();
		}

		public override void Update()
		{
			this.options.Update();

			if (this.options.Confirmed)
			{
				switch (this.options.Index)
				{
					case 4:
						Option.playerCount = ((ConsoleInt)this.options.Elements[0]).Value;
						Option.fieldWidth = ((ConsoleInt)this.options.Elements[1]).Value;
						Option.fieldHeight = ((ConsoleInt)this.options.Elements[2]).Value;
						Option.gameSpeed = ((ConsoleInt)this.options.Elements[3]).Value;
//						Option.maxFruits = ((ConsoleInt)this.options.Elements[4]).Value;
//						Option.fruitLifeTime = ((ConsoleInt)this.options.Elements[5]).Value;
						this.ChangeScene("Menu");
						break;
					default:
						this.options.Confirmed = false;
						break;
				}
			}
		}

		public Options()
		{
			options = new ConsolePanel(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2, 23, 8, "Options",
			new ConsoleInt("Player", Option.playerCount, 1, 4, 1),
			new ConsoleInt("Field Width", Option.fieldWidth, 16, 99, 2),
			new ConsoleInt("Field Height", Option.fieldHeight, 16, 99, 2),
			new ConsoleInt("Game Speed", 6, 1, 12, 2),
//			new ConsoleInt("Max Fruit", 10, 1, 24, 2),
//			new ConsoleInt("Fruit Life", 500, 1, 999, 3),
			new ConsoleButton("Done"));
		}
	}
}
