using System;
using SnakeExtreme.Globals;

namespace SnakeExtreme.Objects.Scene
{
	/// <summary>
	/// The main class for all scenes.
	/// </summary>
	public class Scene
	{
		
		private bool on = false;
		private bool enabled = true;
		
		public void ChangeScene(String sceneName)
		{
			this.Stop();
			Global.scenes[sceneName].Start();
		}

		/// <summary>
		/// Returns true if the scene is running.
		/// </summary>
		/// <returns></returns>
		public bool IsRunning()
		{
			return this.on;
		}

		/// <summary>
		/// Enableds the scene.
		/// </summary>
		public void Enable()
		{
			this.enabled = true;
		}

		/// <summary>
		/// Stops and disabled the scene.
		/// </summary>
		public void Disable()
		{
			this.Stop();
			this.enabled = false;
		}

		/// <summary>
		/// Returns true if the scene is enabled.
		/// </summary>
		/// <returns></returns>
		public bool IsEnabled()
		{
			return enabled;
		}

		/// <summary>
		/// Initialize and runs the scene.
		/// </summary>
		public void Start()
		{
			this.Init();
			this.on = true;
		}

		/// <summary>
		/// Runs the scene.
		/// </summary>
		public void Run()
		{
			this.on = true;
		}

		/// <summary>
		/// Stops the scene.
		/// </summary>
		public void Stop()
		{
			this.on = false;
			Console.Clear();
		}

		/// <summary>
		/// Initialization.
		/// </summary>
		public virtual void Init()
		{
		}

		/// <summary>
		/// Update.
		/// </summary>
		public virtual void Update()
		{
			
		}
	}
}
