﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Thru
{
	public class Player : ICharacter
	{
		public float morale, money;
		public string Name;
		public Player()
		{
		}
		public State Update(GameTime gameTime) { return State.MainSettings; }
		public void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics) { }
	}
}
