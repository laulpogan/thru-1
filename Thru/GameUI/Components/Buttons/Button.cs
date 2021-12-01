using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace Thru
{
	public class Button
	{
		public Texture2D Texture { get; set; }
		public BState State { get; set; }
		public MouseState mouse_state;
		public string Text;
		public int mx, my;
		public Rectangle Bounds;
		bool mpressed, prev_mpressed = false;
		public double timer = 0;
		public SpriteFont Font;
		public string ID;
		private Color _textColor;
		public State stateMachineState { get; set; }
		public delegate void ButtonEventHandler(object sender, EventArgs e);
		public event ButtonEventHandler onClick;
		public Button(Texture2D texture, string text = "", SpriteFont font = null,Color? textColor=null, ButtonEventHandler onclick = null)
		{
			Texture = texture;
			Bounds = texture.Bounds;
			Text = text;
			Font = font;
			_textColor = Color.White;
			if (textColor is not null)
            {
				_textColor = (Color)textColor;
			}
			if (onclick is not null)
            {
				onClick += onclick;
            }
			State = BState.UP;
		}

		public void Update(GameTime gameTime)
		{
			// update mouse variables
			MouseState mouse_state = Mouse.GetState();
			mx = mouse_state.X;
			my = mouse_state.Y;
			prev_mpressed = mpressed;
			mpressed = mouse_state.LeftButton == ButtonState.Pressed;
			update_button();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture,Bounds, Color.White);
			if(Font != null)
            {
				// todo - smarter buttons (text falls off edge)
				spriteBatch.DrawString(Font, Text, new Vector2((Bounds.Width * 0.1f) + Bounds.X, (Bounds.Height * 0.4f)+Bounds.Y), _textColor);
			}
		}

		// determine state and color of button
		void update_button()
		{
			
		

			if (ThruLib.hit_image_alpha(
					Bounds, Texture, mx, my))
				{
					if (mpressed)
					{
						// mouse is currently down
						State = BState.DOWN;
					}
					else if (!mpressed && prev_mpressed)
					{
						// mouse was just released
						if (State == BState.DOWN)
						{
							// button i was just down
							State = BState.JUST_RELEASED;
						if (onClick != null && onClick.GetInvocationList().Length > 0)
						{
							onClick(new EventArgs(), null);
						}
					}
					}
					else
					{
						State = BState.HOVER;
					}
				}
				else
				{
					State = BState.UP;
				}

				if (State == BState.JUST_RELEASED)
				{
				}
			}
		
	}

}


