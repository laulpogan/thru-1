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
		double frameTime;
		public SpriteFont Font;


		public Button(Texture2D texture)
		{
			Texture = texture;
			Bounds = texture.Bounds;
			State = BState.UP;
		}

		public void Update(GameTime gameTime)
		{
			frameTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0;

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
				spriteBatch.DrawString(Font ?? null, Text, new Vector2(Bounds.Width/2,Bounds.Height/2), Color.White);


		}

		// determine state and color of button
		void update_button()
		{
			

				if (ThruLib.hit_image_alpha(
					Bounds, Texture, mx, my))
				{
					timer = 0.0;
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
					if (timer > 0)
					{
						timer = timer - frameTime;
					}
				
				}

				if (State == BState.JUST_RELEASED)
				{
					onClick();
				}
			}
		protected void onClick() { }

	}

}


