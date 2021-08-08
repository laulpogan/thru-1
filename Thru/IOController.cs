using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Text;

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
		static string fileName;
		string path;
		public IOController(IServiceProvider services, string filename)
		{
			fileName = filename;
			path = fileName;
		}
		public void serializeToFile<T>(T[] obj)
        {
			
			string jsonString = JsonSerializer.Serialize<T[]>(obj, options);

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
					T[] temp = deserializeFromFile<T[]>();
					T[] dataBags = new T[temp.Length + obj.Length];
					temp.CopyTo( dataBags,0);
					obj.CopyTo(dataBags, temp.Length);		
				
					jsonString = JsonSerializer.Serialize<T[]>(ThruLib.deDupeArray(dataBags), options);
					sw.WriteLine(jsonString);
				}
			}


			
			Console.WriteLine(jsonString);
		}

		public  T deserializeFromFile<T>()
        {
			
			StringBuilder jsonString = new StringBuilder("");

			// Open the file to read from.
			using (StreamReader sr = File.OpenText(path))
			{
				string s = "";
				while ((s = sr.ReadLine()) != null)
				{
					jsonString.Append(s);
				}
			}
			return JsonSerializer.Deserialize<T>(jsonString.ToString(), options);
		}
		string GetPath(string filePath)
		{
			return Path.GetDirectoryName(Path.GetFullPath(filePath));
		}
	}

}
 