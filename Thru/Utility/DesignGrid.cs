using Newtonsoft.Json;
using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using MonoGame;
namespace Thru
{
    public class DesignGrid
    {
        private ContentManager Content;
        int ClientWidth, ClientHeight;
        GraphicsDeviceManager Graphics;
        public DesignGrid(IServiceProvider services, GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
           
            Content = new ContentManager(services, "Content");

            Content.RootDirectory = "Content";

        }

        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            ClientWidth = Graphics.GraphicsDevice.Viewport.Width;
            ClientHeight = Graphics.GraphicsDevice.Viewport.Height;

            for (int i = 0; i<ClientWidth; i += 100)
            {
             spriteBatch.DrawLine(new Vector2(i, 0), new Vector2(i, ClientHeight), Color.Red);
            }
            for (int z = 0; z < ClientHeight; z += 100)
            {
            spriteBatch.DrawLine(new Vector2(0, z), new Vector2(ClientWidth,z), Color.Red);
            }
        }

    }


}