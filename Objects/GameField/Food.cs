namespace SnakeExtreme.Objects.GameField
{
	/// <summary>
	/// Food class.
	/// </summary>
	public class Food
	{
		public int Cooldown { get; set; }
		public int PosX { get; }
		public int PosY { get; }
		public bool IsPoisoned { get; }

		/// <summary>
		/// Updates the food.
		/// </summary>
		public void Update()
		{
			this.Cooldown--;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="posX">The x position of the food.</param>
		/// <param name="posY">The y position of the food.</param>
		/// <param name="cooldown">The cooldown of the food. If the cooldown is -1, its infinity.</param>
		/// <param name="isPoisened">If true, the food removed 1 tile from the snake instead of adding.</param>
		public Food(int posX, int posY, bool isPoisened = false, int cooldown = 60)
		{
			this.PosX = posX;
			this.PosY = posY;
			this.Cooldown = cooldown;
			this.IsPoisoned = isPoisened;
		}
	}
}
