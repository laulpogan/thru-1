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

namespace Thru
{
	public class GameStateController
	{
		public record FirstName
        {
			[JsonInclude]
			public int female;
			[JsonInclude]
			public int male;
			[JsonInclude]
			public string most_likely;
        }
		Dictionary<string, FirstName> jsonText;
		Player Player;
		Map Map;
		MainGameView MainGameView;
		Location Location;
		public IOController IOController;
		public Encounter Encounter;

        public GameStateController(IServiceProvider services, DisplayWindow displayWindow)
        {
            IOController = new IOController(services, "C:\\Users\\thein\\source\\repos\\thru\\Thru\\Content\\first_name_list.json");
            jsonText = IOController.deserializeFromFile<Dictionary<string, FirstName>>();

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

			List<string> names = new List<string>(jsonText.Keys);
			Player character = new Player(names[rand.Next(names.Count)]);
			Console.WriteLine(character.Name);

			if (jsonText[character.Name].most_likely == "male")
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
