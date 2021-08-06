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
	public class IOController
	{
		JsonSerializerOptions options = new JsonSerializerOptions()
		{
			MaxDepth = 0,
			IgnoreNullValues = true,
			IgnoreReadOnlyProperties = true,
			WriteIndented = true,
			NumberHandling = JsonNumberHandling.AllowReadingFromString |
	 JsonNumberHandling.WriteAsString
		};
		static string fileName = "lololo.json";
		string path = fileName;
		public IOController(IServiceProvider services)
		{
			

		}

		public void Update()
		{

		}

		public void Draw()
		{

		}

		public void serializeToFile(DataBag obj)
        {
			
			string jsonString = JsonSerializer.Serialize<DataBag>(obj, options);

			//using FileStream createStream = File.Create(fileName);
			if (!File.Exists(path))
			{
			Console.WriteLine("Creating file" + GetPath(fileName));

				// Create a file to write to.
				using (StreamWriter sw = File.CreateText(path))
				{
					sw.WriteLine(jsonString);
				}
			} else
            {
				Console.WriteLine("Writing to file " + GetPath(fileName));

				using (StreamWriter sw = File.CreateText(path))
				{
					sw.WriteLine(jsonString);
				}
			}

			// This text is always added, making the file longer over time
			// if it is not deleted.
			
			Console.WriteLine(jsonString);
		}

		public DataBag deserializeFromFile()
        {
			
			string jsonString ="";

			//using FileStream openStream = File.OpenRead(fileName);
			// Open the file to read from.
			using (StreamReader sr = File.OpenText(path))
			{
				string s = "";
				while ((s = sr.ReadLine()) != null)
				{
					jsonString += s;
					Console.WriteLine(s);
				}
			}
			return JsonSerializer.Deserialize<DataBag>(jsonString, options);
		}
		string GetPath(string filePath)
		{
			return Path.GetDirectoryName(Path.GetFullPath(filePath));
		}
	}

}
 