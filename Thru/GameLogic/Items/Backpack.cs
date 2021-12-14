using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;

namespace Thru
{
	public class Backpack
	{

		public int size;
		public bool isFlexible;
		public Item pack;
		public InventoryGameBoard receivers;
		public Backpack(IServiceProvider services, GraphicsDeviceManager graphics, Player player, GlobalState globalState)
		{
			ContentManager Content = new ContentManager(services, "Content");
			int[,] itemShape = new int[0, 0];
			pack = new Item(globalState.MouseHandler, Content.Load<Texture2D>("ItemIcons/Backpack-Raptor1-32x32"), new Point(1025, 250), false, 10, 1, 7.5f, itemShape);
			receivers = new InventoryGameBoard(globalState.MouseHandler, graphics, 4, 5, 18, 32, pack.Home);
		}

		public void Update(GameTime gameTime)
		{
			receivers.Update(gameTime);
			pack.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			pack.Draw(spriteBatch);
			receivers.Draw(spriteBatch);
		}
	}

}
