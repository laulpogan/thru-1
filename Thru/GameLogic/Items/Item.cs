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
		public ItemIconDraggableGroup DraggableGroup;
		public ItemSlot ItemSlot;
		public int[,] trueShape;
		public string Name, Description;

		public Item(MouseHandler mouseHandler, Texture2D icon, Point point, Point boardOrigin, bool isflexible, float bulk, float weight, float scale, int[,] itemShape, ItemSlot itemSlot, SpriteFont font = null)
		{
			trueShape = ThruLib.emptyBoard(itemShape.GetLength(0), itemShape.GetLength(1));
			Icon = icon;
			Home = point;
			renderScale = scale;
			Bulk = bulk;
			Weight = weight;
			isFlexible = isflexible;
			ItemSlot = itemSlot;
			DraggableGroup = new ItemIconDraggableGroup(mouseHandler, Icon,itemShape,Home, boardOrigin,this,  font);
		}

		public void Update(GameTime gameTime)
		{
			DraggableGroup.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			DraggableGroup.Draw(spriteBatch);
			//spriteBatch.Draw(Icon, new Vector2( Home.X, Home.Y), null,Color.White,0f, Vector2.Zero,renderScale,SpriteEffects.None,0f) ;
		}
	}

}
