using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{
	public class Item
	{

		public Texture2D Icon;
		public Point Home;
		public float Bulk, Weight, renderScale;
		public bool isFlexible;
		public DraggableGroup Draggable;
		public Item(MouseHandler mouseHandler, Texture2D icon, Point point, bool isflexible, float bulk, float weight, float scale)
		{
			Icon = icon;
			Home = point;
			renderScale = scale;
			Bulk = bulk;
			Weight = weight;
			isFlexible = isflexible;
			Draggable = new DraggableGroup(mouseHandler, Icon,Home, this);

		}

		public void Update(GameTime gameTime)
		{
			Draggable.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Draggable.Draw(spriteBatch);
			spriteBatch.Draw(Icon, new Vector2( Home.X, Home.Y), null,Color.White,0f, Vector2.Zero,renderScale,SpriteEffects.None,0f) ;
		}
	}

}
