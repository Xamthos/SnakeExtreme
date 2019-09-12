using System;
using System.Collections.Generic;
using SnakeExtreme.Globals;
using SnakeExtreme.Input;

namespace SnakeExtreme.Objects.UI
{
	/// <summary>
	/// Panel for the console for menu boxes.
	/// </summary>
	public class ConsolePanel : ConsoleUI
	{
		public List<ConsoleUI> Elements { get; set; }
		public int Index { get; set; }
		public bool Confirmed { get; set; } = false;
		public int PosX { get; set; }
		public int PosY { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		/// <summary>
		/// Resets the panel.
		/// </summary>
		public void Reset()
		{
			this.Index = 0;
			this.Confirmed = false;
		}

		/// <summary>
		/// Select the next button.
		/// </summary>
		public void Down()
		{
			if (this.Index < Elements.Count - 1)
			{
				this.Index++;
				this.ClearInputStream();
				Global.menuMove.Play();
			}
		}

		/// <summary>
		/// Select the previous button.
		/// </summary>
		public void Up()
		{
			if (this.Index > 0)
			{
				this.Index--;
				this.ClearInputStream();
				Global.menuMove.Play();
			}
		}

		/// <summary>
		/// Confirms the current selected element and play the confirm sound.
		/// </summary>
		public void Confirm()
		{
			this.Confirmed = true;
			Global.menuSelect.Play();
		}

		/// <summary>
		/// Clears the input stream.
		/// </summary>
		public void ClearInputStream()
		{
			while (Console.KeyAvailable)
			{
				Console.ReadKey(true);
			}
		}

		/// <summary>
		/// Gets a selected element from the panel.
		/// </summary>
		/// <returns></returns>
		public ConsoleUI GetSelectedObject()
		{
			if (this.Elements.Count != 0)
			{
				return this.Elements[this.Index];
			}
			return null;
		}

		/// <summary>
		/// Draws the box with buttons.
		/// </summary>
		public override void Update()
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(this.PosX, this.PosY);
			Console.Write(this.Name);

			Console.SetCursorPosition(this.PosX, this.PosY + 1);
			for (int i = 0; i < this.Width; i++)
			{
				Console.Write("-");
			}

			Console.SetCursorPosition(this.PosX, this.PosY + this.Height + 1);
			for (int i = 0; i < this.Width; i++)
			{
				Console.Write("-");
			}

			if (Controls.UI.IsActionPressed("UI_Up", 10))
			{
				this.Up();
			}
			else if (Controls.UI.IsActionPressed("UI_Down", 10))
			{
				this.Down();
			}
			
			if (this.GetSelectedObject() != null)
			{
				if (this.Index >= this.Elements.Count)
				{
					this.Index = this.Elements.Count - 1;
				}

				Console.SetCursorPosition(this.PosX + 3 + this.GetSelectedObject().Name.Length, this.PosY + this.Index + 2);
				this.GetSelectedObject().Update();

				if (this.GetSelectedObject().GetType() == typeof(ConsoleButton) || this.GetSelectedObject().GetType() == typeof(ConsoleString))
				{
					if (Controls.UI.IsActionPressed("UI_Confirm", 10))
					{
						this.Confirm();
					}
				}

				if (this.GetSelectedObject().GetType() == typeof(ConsoleInt))
				{
					ConsoleInt consoleInt = (ConsoleInt)this.GetSelectedObject();
					if (Controls.UI.IsActionPressed("UI_Left", 10))
					{
						if (consoleInt.Value > consoleInt.MinValue)
						{
							consoleInt.Value--;
							Global.menuNumber.Play();
						}
					}
					else if (Controls.UI.IsActionPressed("UI_Right", 10))
					{
						if (consoleInt.Value < consoleInt.MaxValue)
						{
							consoleInt.Value++;
							Global.menuNumber.Play();
						}
					}
				}
			}

			for (int i = 0; i < this.Elements.Count; i++)
			{

				if (this.Index == i)
				{
					Console.SetCursorPosition(this.PosX, this.PosY + i + 2);
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write(">");
				}
				else
				{
					Console.SetCursorPosition(this.PosX, this.PosY + i + 2);
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.Write(" " + this.Elements[i].Name);
				}
				this.Elements[i].Draw(this.PosX + 1, this.PosY + i + 2);
			}
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="name">The name above the buttons.</param>
		/// <param name="posX">The x position.</param>
		/// <param name="posY">The y position.</param>
		/// <param name="bg">The background color for the buttons.</param>
		/// <param name="fg">The foreground color for the buttons.</param>
		/// <param name="buttons">The buttons.</param>
		public ConsolePanel(int posX, int posY, int width, int height, string name, params ConsoleUI[] elements)
		{
			this.PosX = posX;
			this.PosY = posY;
			this.Width = width;
			this.Height = height;
			this.Name = name;
			this.Elements = new List<ConsoleUI>();
			this.Elements.AddRange(elements);
		}
	}
}
