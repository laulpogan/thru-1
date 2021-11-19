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

		public MouseHandler()
		{
			mpressed = mouseState.LeftButton == ButtonState.Pressed;

		}


		public  BState getMouseState()
		{
			BState State = new BState();
			State = BState.UP;
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