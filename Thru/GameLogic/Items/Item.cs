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
		public ItemIconDraggableGroup Draggable;
		public int[,] trueShape;

		public int[,] ItemShape
		{
			get
			{
				foreach (ItemIconDraggable draggable in Draggable.Draggables)
					if (draggable is not null)
						trueShape[draggable.ShapeHome.X, draggable.ShapeHome.Y] = 1;

				return trueShape;
			}
			set { trueShape = value; }
		}
		public Item(MouseHandler mouseHandler, Texture2D icon, Point point, bool isflexible, float bulk, float weight, float scale, int[,] itemShape)
		{
			trueShape = ThruLib.emptyBoard(itemShape.GetLength(0), itemShape.GetLength(1));
			Icon = icon;
			Home = point;
			renderScale = scale;
			Bulk = bulk;
			Weight = weight;
			isFlexible = isflexible;
			Draggable = new ItemIconDraggableGroup(mouseHandler, Icon,itemShape,Home, this);
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
