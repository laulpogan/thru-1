using System;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{
	//public GameState Gamestate;
	public class ioHandler
	{

		public ioHandler()
		{
		}

		public void Update()
		{

		}

		public void Draw()
		{

		}

		public void serializeToFile(string json)
        {
			string fileName = "WeatherForecast.json";
			using FileStream createStream = File.Create(fileName);
			var options = new JsonSerializerOptions { WriteIndented = true };
			string jsonString = JsonSerializer.Serialize(json, options);
			File.WriteAllText(fileName, jsonString);


			Console.WriteLine(File.ReadAllText(fileName));
		}

		public string deserializeFromFile(string json)
        {
			string fileName = "WeatherForecast.json";
			using FileStream openStream = File.OpenRead(fileName);
			string jsonString = File.ReadAllText(fileName);
			return JsonSerializer.Deserialize<string>(jsonString);
		}
	}

}
 