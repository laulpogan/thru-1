using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Text;
using FontStashSharp;

namespace Thru
{
	public class Button
	{
		public Texture2D Texture { get; set; }
		public BState State { get; set; }
		public MouseState mouse_state;
		public string Text;
		public Rectangle Bounds;
		public SpriteFontBase Font;
		public string ID;
		public float Scale;
		public Color TextColor, Color;
		public State stateMachineState { get; set; }
		public delegate void ButtonEventHandler(object sender, EventArgs e);
		public event ButtonEventHandler onClick;
		MouseHandler mouseHandler;
		public Button(MouseHandler mousehandler, Texture2D texture, string text = "", SpriteFontBase font = null, Color? color = null, Color? textColor = null, ButtonEventHandler onclick = null, float scale = .5f)
		{
			mouseHandler = mousehandler;
			Texture = texture;
			Bounds = texture.Bounds;
			Text = text;
			Font = font;
			Scale = scale;
			if (textColor is not null)
				TextColor = (Color)textColor;
			else
				TextColor = Color.White;
			if (color is not null)
				Color = (Color)color;
			else
				Color = Color.White;
		
			if (onclick is not null)
				onClick += onclick;
			State = BState.UP;
		}

		public void Update(GameTime gameTime)
		{
			if (ThruLib.hit_image_alpha(
								Bounds, Texture, mouseHandler.mx, mouseHandler.my))
			{
				State = mouseHandler.getMouseState();
			}
			else
			{
				State = BState.UP;
			}

			if (State == BState.JUST_RELEASED)
			{
			}

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Bounds, Color);
			if (Font != null)
			{
				// todo - smarter buttons (text falls off edge)
				//				spriteBatch.DrawString(Font, Text, new Vector2((Bounds.Width * 0.05f) + Bounds.X, (Bounds.Height * 0.4f) + Bounds.Y), TextColor, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
				spriteBatch.DrawString(Font, Text, new Vector2((Bounds.Width * 0.05f) + Bounds.X, (Bounds.Height * 0.4f) + Bounds.Y), TextColor);
			}
		}
	}



}


