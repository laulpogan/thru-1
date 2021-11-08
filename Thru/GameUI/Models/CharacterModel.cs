using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Reflection.Metadata;

namespace Thru
{
    public class CharacterModel : IModel
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name;
        [JsonProperty(PropertyName = "ID")]
        public string ID;
        public string effectedStat;
        public int effect;
        public AnimatedSprite spriteModel;
        private ContentManager Content;
        public SpriteBatch spriteBatch;
        public Vector2 ScreenXY;



        public CharacterModel(IServiceProvider services, GraphicsDeviceManager graphics, Player player)
{
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            Content = new ContentManager(services, "Content");
            Texture2D spriteSheet = Content.Load<Texture2D>("mannequin-skin-tone-1");
            spriteModel = new AnimatedSprite(spriteSheet, 2, 2);
            ScreenXY = new Vector2(500, 500);
        }

        public void Update(GameTime gameTime)
        {
            spriteModel.Update(gameTime);
        }
        public void Draw(GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin();
            spriteModel.Draw(spriteBatch, ScreenXY, 1f);
            spriteBatch.End();

        }

    }


}