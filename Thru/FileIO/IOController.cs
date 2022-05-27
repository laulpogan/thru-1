using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using GeoJSON;

namespace Thru
{
    public class IOController
    {/*
		JsonSerializerOptions options = new JsonSerializerOptions()
		{
			MaxDepth = 0,
			IgnoreNullValues = true,
			IgnoreReadOnlyProperties = true,
			IncludeFields = true,
			WriteIndented = true,
			NumberHandling = JsonNumberHandling.AllowReadingFromString |
	 JsonNumberHandling.WriteAsString
		};*/

        JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore

        };
        static string fileName;
        string path;
        public IOController(IServiceProvider services, string filename)
        {

            path = Path.GetFullPath("../../../") + filename;
        }
        public void serializeToFile<T>(Dictionary<string, T> obj)
        {



            string jsonString = JsonConvert.SerializeObject(obj, Settings);

            if (!File.Exists(path))
            {
                Console.WriteLine("Creating file " + path);

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    Console.WriteLine(jsonString);
                    sw.WriteLine(jsonString);
                    sw.Close();
                }
            }
            else
            {
                var temp = deserializeFromFile<T>();
                Console.WriteLine("Writing to file " + path);
                temp.ToList().ForEach(x => obj[x.Key] = x.Value);
                jsonString = JsonConvert.SerializeObject(temp, typeof(T), Settings);
                using (StreamWriter sw = File.CreateText(path))
                {
                    Console.WriteLine(jsonString);
                    sw.WriteLine(jsonString);
                    sw.Close();
                }
            }



        }

        public Dictionary<string, T> deserializeFromFile<T>()
        {

            StringBuilder jsonString = new StringBuilder("");
            Console.WriteLine("Reading from file " + path);

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {

                    jsonString.Append(s);
                }
                sr.Close();
            }

            return JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonString.ToString(), Settings);
        }

        public void loadManifests()
        {
            Dictionary<string, RecordManifestData> manifests = this.deserializeFromFile<RecordManifestData>();
        }

        public void loadEncounters()
        {

        }

        public void loadItems() { }
        public void loadCharacters() { }


        public void saveCharacter()
        {

        }

        public void saveItem()
        {

        }

        public void saveGlobalState()
        {

        }

        /*public class InfoToStringConverter : JsonConverter<string>
			{
				public override string Read(
		ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			using (var jsonDoc = JsonDocument.ParseValue(ref reader))
			{
				return jsonDoc.RootElement.GetRawText();
			}
		}

			public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
			{
				using (JsonDocument document = JsonDocument.Parse(value))
				{
					document.RootElement.WriteTo(writer);
				}
			}
		}*/

    }

}
