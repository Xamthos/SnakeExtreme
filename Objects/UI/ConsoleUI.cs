using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeExtreme.Objects.UI
{
	/// <summary>
	/// Main class for all UI Elements.
	/// </summary>
	public class ConsoleUI
	{
		public string Name { get; set; }
		public bool Show { get; set; }
		public bool Enabled { get; set; }

		public virtual void Update()
		{
			
		}

		public virtual void Draw(int posX, int posY)
		{

		}
	}
}