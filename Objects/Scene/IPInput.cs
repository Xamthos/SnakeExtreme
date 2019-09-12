using System;
using SnakeExtreme.Globals;
using SnakeExtreme.Objects.UI;

namespace SnakeExtreme.Objects.Scene
{
	public class IPInput : Scene
	{
		public override void Init()
		{
		}

		public override void Update()
		{
			Console.Write("IP:\t");
			Global.directJoinIP = Console.ReadLine();
			this.ChangeScene("DirectJoin");
		}
	}
}
