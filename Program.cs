using System;
using System.Threading;
using SnakeExtreme.Globals;
using SnakeExtreme.Input;
using SnakeExtreme.Objects.Scene;

namespace SnakeExtreme
{
	class Program
	{
		private static int ups = 5;
		
		[STAThread]
		static void Main(string[] args)
		{

			//Adds all existing scenes.
			Global.scenes.Add("Game", new Game());
			Global.scenes.Add("Menu", new Menu());
			Global.scenes.Add("Options", new Options());
			Global.scenes.Add("GameOver", new GameOver());
			Global.scenes.Add("Win", new Win());
			Global.scenes.Add("NetworkGameMenu", new NetworkGameMenu());
			Global.scenes.Add("HostMenu", new HostMenu());
			Global.scenes.Add("Hosting", new Hosting());
			Global.scenes.Add("GameSearch", new GameSearch());
			Global.scenes.Add("IPInput", new IPInput());
			Global.scenes.Add("PlayNetworkGame", new PlayNetworkGame());

			//Sets the KeyBinds.
			Controls.Init();

			//Activates the menu scene and start the initialization.
			Global.scenes["Menu"].Start();

			//Updates all active scenes. ^[0-9]*[,]?[0-9]+$
			long timeCounter = 0;
			while (Global.gameOn)
			{
				long timeNow = DateTime.UtcNow.Ticks;

				Console.CursorVisible = false;

				timeCounter = 0;

				foreach (Scene scene in Global.scenes.Values)
				{
					if (scene.IsRunning())
					{
						scene.Update();
					}
				}

				Thread.Sleep(1000 / (ups * Option.gameSpeed));

				long timeSinceLastFrame = DateTime.UtcNow.Ticks - timeNow;
				timeCounter += timeSinceLastFrame;
			}
		}
	}
}
