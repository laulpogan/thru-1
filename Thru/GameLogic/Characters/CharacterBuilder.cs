using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GeoJSON;
using Newtonsoft.Json;
using GeoJSON.Net.Geometry;
using GeoJSON.Net.Feature;

namespace Thru
{
	public class CharacterBuilder
	{
		Dictionary<string, FirstName> nameDict;
		public IOController IOController;
		public Location Location;
		public CharacterBuilder(IServiceProvider services, GraphicsDeviceManager graphics, Location location)
        {
			
			// TODO config file or relative path
			IOController = new IOController(services, "Content\\first_name_list.json");
			var jsonText = IOController.deserializeFromFile<FirstName>();
			nameDict = jsonText;
			Location = location;
			Player boo = createCharacter();
            Dictionary<string, Player> participants = new Dictionary<string, Player>();
            participants[boo.Name] = boo;
        }

		

		public  Player createCharacter()
        {
			
			Random rand = new Random();

			List<string> names = new List<string>(nameDict.Keys);
			Player character = new Player(names[rand.Next(names.Count)], Location);
			Console.WriteLine(character.Name);

			if (nameDict[character.Name].most_likely == "male")
            {
				character.Gender = Player.Genders.male;
            } else
            {
				character.Gender = Player.Genders.female;
			}
;
			Console.WriteLine(character.Name + " " + character.Gender );
			return character;
        }

	}

}
