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
using FontStashSharp;

namespace Thru
{
	public class CharacterBuilder
	{
		Dictionary<string, FirstName> nameDict;
		public IOController IOController;
		public Location Location;
		public IServiceProvider Services;
		public GraphicsDeviceManager Graphics;
		public SpriteFontBase Font;
		public Character Character;

		public CharacterBuilder(IServiceProvider services, GraphicsDeviceManager graphics, Microsoft.Xna.Framework.Point screenXY, SpriteFontBase font = null)
        {
			
			// TODO config file or relative path
			IOController = new IOController(services, "Content\\DataLists\\first_name_list.json");
			var jsonText = IOController.deserializeFromFile<FirstName>();
			nameDict = jsonText;
			Services = services;
			Graphics = graphics;
			Character = createCharacter(screenXY);
            Dictionary<string, Character> participants = new Dictionary<string, Character>();
            participants[Character.Name] = Character;
			Font = font;
        }

		

		public  Character createCharacter(Microsoft.Xna.Framework.Point screenXY)
        {
			
			Random rand = new Random();

			List<string> names = new List<string>(nameDict.Keys);
			Character character = new Character(Services, Graphics, names[rand.Next(names.Count)], screenXY, Font);
			Console.WriteLine(character.Name);

			if (nameDict[character.Name].most_likely == "male")
            {
				character.Gender = Character.Genders.male;
            } else
            {
				character.Gender = Character.Genders.female;
			}
;
			Console.WriteLine(character.Name + " " + character.Gender );
			return character;
        }

	}

}
