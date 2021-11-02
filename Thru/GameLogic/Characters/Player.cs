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
		public Stats stats;
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


		public Stats buildStats()
        {

			Random rand = new Random();
			Stats stats = new Stats();
			stats.Age = rand.Next(20) + 15;
			stats.Charisma = rand.Next(20);
			stats.Chillness = rand.Next(20);
			stats.Cleverness = rand.Next(20);
			stats.Fitness = rand.Next(20);
			stats.Luck = rand.Next(20);
			stats.Miles = rand.Next(20);
			stats.Money = rand.Next(20000) + 10000;
			stats.Morale = rand.Next(20);
			stats.Outdoorsyness = rand.Next(20);
			stats.Snacks = rand.Next(100) + 25;
			stats.Speed = rand.Next(20);
			return stats;
		}
		public State Update(GameTime gameTime) { return State.MainSettings; }
		public void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics) { }

	}
}

