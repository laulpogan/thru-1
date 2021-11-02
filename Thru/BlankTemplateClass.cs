using Newtonsoft.Json;
using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{
    public class BlankTemplateClass
    {
        private ContentManager Content;

        public BlankTemplateClass(int clientWidth, int clientHeight, IServiceProvider services, GraphicsDeviceManager graphics)
        {
            Content = new ContentManager(services, "Content");

            Content.RootDirectory = "Content";

        }

        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
        }

    }


}