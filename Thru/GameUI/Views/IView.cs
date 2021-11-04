using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static Thru.GameViewStateMachine;

namespace Thru
{
	public interface IView
	{
		public  abstract State Update(GameTime gameTime);

		public abstract void Draw( GraphicsDeviceManager _graphics);
	}


	
	}

