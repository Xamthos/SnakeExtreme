using System.Text;

namespace SnakeExtreme.Objects.Network
{
	/// <summary>
	/// A network game for the game search scene.
	/// </summary>
	public class NetworkGame
	{
		public string Name { get; set; }
		public string Ip { get; set; }

		private int pointer = 0;

		/// <summary>
		/// Sets the properties with the byte array from the host.
		/// </summary>
		/// <param name="data"></param>
		public void SetData(byte[] data)
		{
			byte[] nameData = this.GetNextData(data);
			byte[] ipData = this.GetNextData(data);

			this.Name = Encoding.ASCII.GetString(nameData);
			this.Ip = Encoding.ASCII.GetString(ipData);
		}

		/// <summary>
		/// Gets the next property from the byte array from the host.
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		private byte[] GetNextData(byte[] bytes)
		{
			int length = bytes[this.pointer];
			this.pointer++;
			byte[] data = new byte[length];
			for (int i = 0; i < length; i++)
			{
				data[i] = bytes[this.pointer];
				this.pointer++;
			}
			return data;
		}

		public NetworkGame()
		{

		}
	}
}