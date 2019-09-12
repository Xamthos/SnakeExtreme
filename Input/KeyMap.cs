using System;
using System.Windows.Input;
using System.Collections.Generic;

using SnakeExtreme.Globals;

namespace SnakeExtreme.Input
{
	/// <summary>
	/// Class for keybinds.
	/// </summary>
	public class KeyMap
	{
		public int delayTimer = 0;
		
		private Dictionary<string, Key[]> keyBinds;

		/// <summary>
		/// Adds a new keybind with multiple keys.  At least one must be pressed to activate the action.
		/// </summary>
		/// <param name="action">The name of the keybind.</param>
		/// <param name="keys">A list of the keys.</param>
		public void AddKeyBind(string action, params Key[] keys)
		{
			keyBinds.Add(action, keys);
		}

		/// <summary>
		/// Returns the keys from the action name.
		/// </summary>
		/// <param name="action">The action name</param>
		/// <returns></returns>
		public Key[] GetKeysFromAction(string action)
		{
			return this.keyBinds[action];
		}

		/// <summary>
		/// Checks if at least one key is pressed and returns true when it is.
		/// </summary>
		/// <param name="action">The action to check.</param>
		/// <returns></returns>
		public bool IsActionPressed(string action, int delay = 0)
		{

			if (this.delayTimer > 0)
			{
				this.delayTimer--;
				return false;
			}
			
			Key[] keys = new Key[0];

			try
			{
				keys = this.keyBinds[action];
			}
			catch(KeyNotFoundException e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("[ERROR]:The action \"" + action + "\" doesn't exists.\n" + e.StackTrace);
			}

			foreach (Key key in keys)
			{
				if (Keyboard.IsKeyDown(key))
				{
					this.delayTimer = delay;
					return true;
				}
			}

			return false;
		}

		public KeyMap()
		{
			this.keyBinds = new Dictionary<string, Key[]>();
		}
		
	}
}
