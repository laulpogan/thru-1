using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Thru
{
	public class Player : ICharacter
	{
		public Dictionary<string, int> stats;
		public string Name;
		public string TrailName;
		public DateTime Date;
		public string locationID;
		public string Perk;

		public enum Genders
        {
			male,
			female
        }
		public Genders Gender;
		public Dictionary<string, Item> Inventory;
		public Dictionary<string, ICharacter> Tramily;
		public Player(string name)
		{
;
			Name = name;
			stats = buildStats();
		}


		public Dictionary<string, int> buildStats()
        {

			Random rand = new Random();
			Dictionary<string, int> stats = new Dictionary<string, int>();
			stats["morale"] = rand.Next(20);
			stats["miles"] = rand.Next(20);
			stats["money"] = rand.Next(20000);
			stats["age"] = rand.Next(20) + 15;
			stats["speed"] = rand.Next(20);
			stats["charisma"] = rand.Next(20);
			stats["Outdoorsyness"] = rand.Next(20);
			stats["intelligence"] = rand.Next(20);
			stats["strength"] = rand.Next(20);
			stats["luck"] = rand.Next(20);
			return stats;
		}
		public State Update(GameTime gameTime) { return State.MainSettings; }
		public void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics) { }

	}
}

