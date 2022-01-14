using Newtonsoft.Json;
using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Thru
{
	public class MouseHandler : IDraggableContainer
	{


		public MouseState mouseState;
		public bool mpressed, prev_mpressed = false;
		public bool rpressed, prev_rpressed = false;
		public ItemIconDraggable iconHeld { get; set; }
		public Point BoardHome { get; set; }
		public Point ScreenHome
        {
            get
            {
				return mXY;
            }
			set { }
        }

		public Item ItemDragged
        {
			get
            {
				return iconHeld is not null ? iconHeld.Group.Item : null;
            }
			set { }
        }

		
		public int mx, my;
		public Point mXY
		{
			get { return new Point(mx,my); }
			set
			{
				mx = value.X;
				my = value.Y;
			}
		}
		public BState State, RState;
		public bool isDragging
        {
			get
            {
				return ItemDragged is not null;
            }
        }


		public MouseHandler()
		{
			iconHeld = null;
			State = new BState();
			mouseState = Mouse.GetState();
		}


		public void Update(GameTime gameTime)
        {
			mouseState = Mouse.GetState();
			mx = mouseState.X;
			my = mouseState.Y;
			State = getMouseState();
			RState = getRMouseState();
			prev_mpressed = mpressed;
			prev_rpressed = rpressed;
			mpressed = mouseState.LeftButton == ButtonState.Pressed;
			rpressed = mouseState.RightButton == ButtonState.Pressed;
            switch (State)
            {
                case BState.JUST_RELEASED:
                    break;
				case BState.DOWN:
					break;
				case BState.HOVER:
					break;

            }
			
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

		public BState getRMouseState()
		{
			if (rpressed)
			{
				// mouse is currently down
				RState = BState.DOWN;
			}
			else if (!rpressed && prev_rpressed)
			{
				// mouse was just released
				if (RState == BState.DOWN)
				{
					// button i was just down
					RState = BState.JUST_RELEASED;
				}
			}
			else
			{
				RState = BState.HOVER;
			}

			return RState;
		}
	}


}