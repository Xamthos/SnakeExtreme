using System;

namespace SnakeExtreme.Objects.UI
{
	/// <summary>
	/// Button class for ConsolePanels.
	/// </summary>
	public class ConsoleButton : ConsoleUI
	{
		public override void Draw(int posX, int posY)
		{
			Console.SetCursorPosition(posX, posY);
			Console.Write(this.Name);
		}

		public ConsoleButton(string name = "Button")
		{
			this.Name = name;
		}
	}
}