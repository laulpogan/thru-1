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
	public class NamedCharacter : ICharacter
	{
		public Stats stats;
		public string Name;
		public string TrailName;
		public DateTime Date;
		public string locationID;
		public string Perk;
		public Location Location, TrailLocation;
		public CharacterModel model;
		public Vector3 MapCoords;
		public Vector2 ScreenXY;
		public bool hasAltArt;
		public enum Genders
        {
			male,
			female
        }
		public Genders Gender;
		public Dictionary<string, Item> Inventory;
		public Dictionary<string, ICharacter> Tramily;
		public NamedCharacter(IServiceProvider services, GraphicsDeviceManager graphics, string name, bool altArt = false)
		{

			Name = name;
			stats = new Stats();
			ScreenXY = new Vector2(0,0);
			hasAltArt = altArt;
            if (hasAltArt)
            {

            }
			model = new CharacterModel(services, graphics, ScreenXY);
		}

		  public void MoveTo(Location location)
        {
			Location = location;
        }

		
		public State Update(GameTime gameTime) {
			/*ScreenXY.X = trailLocation.Coords.X;
			ScreenXY.Y = trailLocation.Coords.Y;*/
			model.ScreenXY = ScreenXY;
			model.Update(gameTime);
			return State.MainSettings; }
		public void Draw(SpriteBatch spriteBatch) {

			model.Draw(spriteBatch, 1);
		}

	}
}

