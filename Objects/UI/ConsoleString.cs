using System;
using System.Threading;

namespace SnakeExtreme.Objects.UI
{
	/// <summary>
	/// String Input for ConsolePanels.
	/// </summary>
	public class ConsoleString : ConsoleUI
	{
		public string Value { get; set; }
		public bool WriteMode { get; set; }

		public override void Update()
		{
			if (this.WriteMode)
			{
				Console.CursorVisible = true;
				Thread.Sleep(50);
				this.Value = Console.ReadLine();
				this.WriteMode = false;
			}
		}

		public override void Draw(int posX, int posY)
		{
			Console.SetCursorPosition(posX, posY);
			Console.Write(this.Name + ": " + this.Value);
		}

		public ConsoleString(string name)
		{
			this.Name = name;
		}
	}
}
