using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeExtreme.Objects.UI
{
	/// <summary>
	/// Integer input for ConsolePanels.
	/// </summary>
	public class ConsoleInt : ConsoleUI
	{
		public int Value { get; set; }
		public int MaxValue { get; set; }
		public int MinValue { get; set; }
		public int Digits { get; set; }

		public override void Draw(int posX, int posY)
		{
			Console.SetCursorPosition(posX, posY);
			Console.Write(this.Name + ": ");

			string digitsPlaceHolder = "";

			for (int i = 0; i < this.Digits; i++)
			{
				digitsPlaceHolder += "0";
			}

			if (this.Value < 0)
			{
				Console.Write("{0:" + digitsPlaceHolder + "}",this.Value);
			}
			else
			{
				Console.Write(" {0:" + digitsPlaceHolder + "}", this.Value);
			}
		}

		public ConsoleInt(string name, int value = 1, int minValue = -99, int maxValue = 99, int digits = 2)
		{
			this.Name = name;
			this.Value = value;
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			this.Digits = digits;
		}
	}
}
