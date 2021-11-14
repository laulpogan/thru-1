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
        public AnimatedSprite spriteBody, spriteEyes, spriteHair, spriteShirt, spritePants, spriteShoes;
        public Color bodyColor, eyeColor, hairColor, shirtColor, pantsColor, shoeColor;
        private ContentManager Content;
        public Vector2 ScreenXY;



        public CharacterModel(IServiceProvider services, GraphicsDeviceManager graphics,Vector2 screenXY)
{
            Content = new ContentManager(services, "Content");
            //work your way down the body from the top
            Texture2D body = Content.Load<Texture2D>("CharacterModels/body-tone-1");
            Texture2D hair = Content.Load<Texture2D>("CharacterModels/hair-orange");
            Texture2D eyes = Content.Load<Texture2D>("CharacterModels/eyes-white");
            Texture2D shirt = Content.Load<Texture2D>("ItemModels/shirt-blue");
            Texture2D pants = Content.Load<Texture2D>("ItemModels/pants-olive");
            Texture2D shoes = Content.Load<Texture2D>("ItemModels/shoes-green");

            bodyColor = Color.White;
            hairColor = Color.White;
            eyeColor = Color.White;
            shirtColor = Color.White;
            pantsColor = Color.White;
            shoeColor = Color.White;


            spriteBody = new AnimatedSprite(body, 1, 2, bodyColor);
            spriteHair = new AnimatedSprite(hair, 1, 2, hairColor);
            spriteEyes = new AnimatedSprite(eyes, 1, 2, eyeColor);
            spriteShirt = new AnimatedSprite(shirt, 1, 2, shirtColor);
            spritePants = new AnimatedSprite(pants, 1, 2, pantsColor);
            spriteShoes = new AnimatedSprite(shoes, 1, 2, shoeColor);

            ScreenXY = screenXY;
        }

        public void Update(GameTime gameTime)
        {
            spriteBody.Color = bodyColor;
            spriteBody.Update(gameTime);
            spriteHair.Color = hairColor;
            spriteHair.Update(gameTime);
            spriteEyes.Color = eyeColor;
            spriteEyes.Update(gameTime);
            spriteShirt.Color = shirtColor;
            spriteShirt.Update(gameTime);
            spritePants.Color = pantsColor;
            spritePants.Update(gameTime);
            spriteShoes.Color = shoeColor;
            spriteShoes.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, float scale )
        {
            spriteBody.Draw(spriteBatch, ScreenXY, scale);
            spriteHair.Draw(spriteBatch, ScreenXY, scale);
            spriteEyes.Draw(spriteBatch, ScreenXY, scale);
            spriteShirt.Draw(spriteBatch, ScreenXY, scale);
            spritePants.Draw(spriteBatch, ScreenXY, scale);
            spriteShoes.Draw(spriteBatch, ScreenXY, scale);

        }

    }


}