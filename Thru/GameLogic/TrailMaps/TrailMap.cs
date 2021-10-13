using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GeoJSON.Net.Feature;

namespace Thru
{
	public class TrailMap
	{
		public ArrayList Locations;
		public AnimatedSprite sprite;
		public ArrayList Characters;
		public ArrayList Trails;
		public String Name;
		public Vector2 Origin;
		public TrailMap(ArrayList nodes, ArrayList edges,   String name, Texture2D texture, Vector2 origin)
		{
			Locations = nodes;
			Trails = edges;
			Name = name;
			Origin = new Vector2(0,0);
			sprite = new AnimatedSprite(texture, 2, 2);
			/*foreach(Location node in Locations)
            {

            }
			foreach(Trail edge in Trails)
            {

            }*/
		}

		public void Update(GameTime gameTime)
		{

			foreach (Location node in Locations)
			{
				node.Update(gameTime);
			}
			foreach (Trail edge in Trails)
			{
				edge.Update(gameTime);
			}

		}

		public void Draw(SpriteBatch spriteBatch)
		{

			foreach (Location node in Locations)
			{
				node.Draw(spriteBatch);
			}
			foreach (Trail edge in Trails)
			{
				edge.Draw(spriteBatch);
			}
		}
	}


}
