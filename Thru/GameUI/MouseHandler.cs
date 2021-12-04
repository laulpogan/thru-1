using Newtonsoft.Json;
using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Thru
{
	public class MouseHandler
	{


		public MouseState mouseState;
		bool mpressed, prev_mpressed = false;
		public Item dragged;
		public int mx, my;
		public BState State;
		public bool isDragging;


		public MouseHandler()
		{
			State = new BState();
			mouseState = Mouse.GetState();
		}


		public void Update(GameTime gameTime)
        {
			mouseState = Mouse.GetState();
			mx = mouseState.X;
			my = mouseState.Y;
			State = getMouseState();
			prev_mpressed = mpressed;
			mpressed = mouseState.LeftButton == ButtonState.Pressed;

		}
		public  BState getMouseState()
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
				}
			}
			else
			{
				State = BState.HOVER;
			}

			return State;
		}
	}


}