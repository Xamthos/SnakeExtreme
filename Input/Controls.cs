using System.Windows.Input;

namespace SnakeExtreme.Input
{
	/// <summary>
	/// Global class for all keybinds in the game.
	/// </summary>
	public class Controls
	{
		public static readonly KeyMap UI = new KeyMap();
		public static readonly KeyMap NetGame = new KeyMap();

		public static void Init()
		{
			UI.AddKeyBind("UI_Up", Key.W, Key.Up);
			UI.AddKeyBind("UI_Left", Key.A, Key.Left);
			UI.AddKeyBind("UI_Down", Key.S, Key.Down);
			UI.AddKeyBind("UI_Right", Key.D, Key.Right);
			UI.AddKeyBind("UI_Confirm", Key.Space, Key.Enter);
			UI.AddKeyBind("UI_Reload", Key.F5);
			UI.AddKeyBind("UI_Back", Key.Escape, Key.Back);

			NetGame.AddKeyBind("Up", Key.W, Key.Up);
			NetGame.AddKeyBind("Left", Key.A, Key.Left);
			NetGame.AddKeyBind("Down", Key.S, Key.Down);
			NetGame.AddKeyBind("Right", Key.D, Key.Right);
		}
	}
}
