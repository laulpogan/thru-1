using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Thru
{
	public interface ICharacter
	{
		//public abstract 
		public abstract State Update(GameTime gameTime);
		public abstract void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics);

	}



}

