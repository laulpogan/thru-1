using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Reflection.Metadata;

namespace Thru
{
    public class BackgroundModel : IModel
    {

        public AnimatedSprite background, weather;
        public Color backgroundColor, weatherColor;
        private ContentManager Content;
        public SpriteBatch spriteBatch;
        public Point ScreenXY;



        public BackgroundModel(IServiceProvider services, GraphicsDeviceManager graphics, int x, int y)
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            Content = new ContentManager(services, "Content");
            Texture2D backgroundAsset = Content.Load<Texture2D>("Backgrounds/trees2");
            Texture2D weatherAsset = Content.Load<Texture2D>("Backgrounds/fog2");
            backgroundColor = Color.White;
            weatherColor = Color.White *.75f;


            background = new AnimatedSprite(backgroundAsset, 24, 1, backgroundColor);
            weather = new AnimatedSprite(weatherAsset, 24, 1, weatherColor);
           

            ScreenXY = new Point(x, y);
        }

        public void Update(GameTime gameTime)
        {
            background.Color = backgroundColor;
            background.Update(gameTime);
            weather.Color = weatherColor;
            weather.Update(gameTime);
        }
        public void Draw(GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch, ScreenXY, 3.5f);
            weather.Draw(spriteBatch, ScreenXY, 3.5f);
            spriteBatch.End();

        }

    }


}