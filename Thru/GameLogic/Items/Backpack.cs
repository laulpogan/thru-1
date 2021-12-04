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
		public ArrayList receivers;
		public Backpack(IServiceProvider services, GraphicsDeviceManager graphics, Player player, GlobalState globalState)
		{
			ContentManager Content = new ContentManager(services, "Content");
			pack = new Item(globalState.MouseHandler, Content.Load<Texture2D>("ItemIcons/Backpack-Raptor1-32x32"), new Point(1025, 250), false, 10, 1, 7.5f);
			receivers = new ArrayList();
			for (int x = 1050; x <= 1200; x += 50)
			{
				for (int y = 250; y <= 450; y += 50)
				{
					receivers.Add(new DraggableReceiver(globalState.MouseHandler, graphics, new Point(x, y)));
				}
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (DraggableReceiver receiver in receivers)
			{
				receiver.Update(gameTime);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			pack.Draw(spriteBatch);
			foreach (DraggableReceiver receiver in receivers)
			{
				receiver.Draw(spriteBatch);
			}
		}
	}

}
