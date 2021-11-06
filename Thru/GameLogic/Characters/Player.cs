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
		public Location location;

		public enum Genders
        {
			male,
			female
        }
		public Genders Gender;
		public Dictionary<string, Item> Inventory;
		public Dictionary<string, ICharacter> Tramily;
		public Player(string name, Location Location = null)
		{

			if(Location is not null)
            {
				location = Location;
			}
			Name = name;
			stats = new Stats();
		}


		
		public State Update(GameTime gameTime) { return State.MainSettings; }
		public void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics) { }

	}
}

