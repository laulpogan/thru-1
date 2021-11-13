using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Thru
{
	public class NPC : ICharacter
	{
		public float morale, money;
		public string Name;
		public NPC()
		{
		}
		public State Update(GameTime gameTime) { return State.MainSettings; }
		public void Draw( SpriteBatch spriteBatch) { }
	}
}

