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
	public class GameStateController
	{
		public record FirstName
        {
			[JsonProperty(PropertyName = "female")]
			public int female;
			[JsonProperty(PropertyName = "male")] 
			public int male;
			[JsonProperty(PropertyName = "most_likely")] 
			public string most_likely;
        }
	/*	public record Feature
        {
			LineString geometry;
        }
		public record FeatureCollection
		{
			Feature[] features;

        }*/
		Dictionary<string, FirstName> nameDict;
		Player Player;
		MapMenu Map;
		MapView mapView;
		Location Location;
		public IOController IOController;
		public Encounter Encounter;

		public GameStateController(IServiceProvider services, DisplayWindow displayWindow)
        {
			
			/*IOController mapData = new IOController(services, "G:\\Users\\thein\\Downloads\\PacificCrestTrail.json");
			Dictionary<string, Point> mapDict = mapData.deserializeFromFile<Point>();

			foreach (string key in mapDict.Keys)
			{
				Console.WriteLine(mapDict[key]);
			}*/
			IOController = new IOController(services, "G:\\Users\\thein\\source\\repos\\thru\\Thru\\Content\\first_name_list.json");
			var jsonText = IOController.deserializeFromFile<FirstName>();
			nameDict = jsonText;

			Player boo = createCharacter();
            Dictionary<string, Player> participants = new Dictionary<string, Player>();
            participants[boo.Name] = boo;
            Encounter = new Encounter(participants, "luck", 12, "Boo", "You whore", displayWindow, services);

        }

		

		public State Update(GameTime gameTime)
        {
			return Encounter.Update(gameTime );
        }
		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
			
			Encounter.Draw(spriteBatch, gameTime);
        }
		
		public  Player createCharacter()
        {
			
			Random rand = new Random();

			List<string> names = new List<string>(nameDict.Keys);
			Player character = new Player(names[rand.Next(names.Count)]);
			Console.WriteLine(character.Name);

			if (nameDict[character.Name].most_likely == "male")
            {
				character.Gender = Player.Genders.male;
            } else
            {
				character.Gender = Player.Genders.female;
			}
;
			Console.WriteLine(character.Name + " " + character.Gender + " Morale "+ character.stats["morale"] + " Miles " + character.stats["miles"] + " Money " + character.stats["money"] + " Age " + character.stats["age"] + " Speed " + character.stats["speed"] + " Charisma " + character.stats["charisma"] + " Hardiness " + character.stats["hardiness"] + " Intelligence" +
				" " + character.stats["intelligence"] + " Strength " + character.stats["strength"] + " Luck " + character.stats["luck"]);
			return character;
        }

	}

}
