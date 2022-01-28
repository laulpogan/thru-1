using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Spritesheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Thru
{
    public class CharacterModel : IModel
    {

        public CharacterModelSprite blankSprite, spriteBody,spriteArms,spriteHair, spriteEyes, spriteBackpack, spriteBackpackStraps, spriteSleeves, spriteShirt, spritePants, spriteShoes;
        public Color bodyColor, eyeColor, hairColor, shirtColor, pantsColor, shoeColor;
        private ContentManager Content;
        public Point ScreenXY;
        public float Scale;
        Character Player;
        public SpriteFont Font;
        public int currentFrame;
        public int Rows, Columns;
        public  int totalFrames;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 500;
        public Dictionary<ItemSlot,CharacterModelSprite> EquippedSprites;


        public CharacterModel(IServiceProvider services, GraphicsDeviceManager graphics, Point screenXY, Character player, SpriteFont font = null)
{
            Content = new ContentManager(services, "Content");
            Font = font;
            //work your way down the body from the top
            /*Texture2D body = Content.Load<Texture2D>("CharacterModels/body-tone-1");
            Texture2D hair = Content.Load<Texture2D>("CharacterModels/hair-orange");
            Texture2D eyes = Content.Load<Texture2D>("CharacterModels/eyes-white");
            Texture2D shirt = Content.Load<Texture2D>("ItemModels/shirt-blue");
            Texture2D pants = Content.Load<Texture2D>("ItemModels/pants-olive");
            Texture2D shoes = Content.Load<Texture2D>("ItemModels/shoes-green");*/
                 Texture2D hair = Content.Load<Texture2D>("CharacterModels/hair-orange");
            Texture2D eyes = Content.Load<Texture2D>("CharacterModels/eyes-white");
             Texture2D body = Content.Load<Texture2D>("CharacterModels/CharacterAnimation-body-Sheet");
            Texture2D arms = Content.Load<Texture2D>("CharacterModels/CharacterAnimation-arms-Sheet");
          

            blankSprite = new CharacterModelSprite(ThruLib.makeBlankRect(graphics, 1, 1), 1, 1, this);

            currentFrame = 0;


           EquippedSprites = new Dictionary<ItemSlot, CharacterModelSprite>()
			{
				{ ItemSlot.BackpackStraps, null },
				{ ItemSlot.Backpack, null },
				{ ItemSlot.Hat, null },
				{ ItemSlot.Shirt, null },
				{ ItemSlot.Sleeves, null },
				{ ItemSlot.Pants, null },
				{ ItemSlot.Shoes, null },
				{ ItemSlot.Poles, null },
				{ ItemSlot.Misc1, null },
				{ ItemSlot.Misc2, null },
			};


            bodyColor = Color.White;
            hairColor = Color.White;
            eyeColor = Color.White;
            shirtColor = Color.White;
            pantsColor = Color.White;
            shoeColor = Color.White;

            Player = player;
            spriteBody = new CharacterModelSprite(body, 1, 4, this, bodyColor);
            spriteHair = new CharacterModelSprite(hair, 1, 2, this, hairColor);
            spriteEyes = new CharacterModelSprite(eyes, 1, 2, this, eyeColor);
            spriteArms = new CharacterModelSprite(arms, 1, 4, this, bodyColor);
            ScreenXY = screenXY;
        }


        public void setSpriteClothes()
        {
            spriteShirt =getSprite(ItemSlot.Shirt);
            spriteSleeves = getSprite(ItemSlot.Sleeves);
            spritePants = getSprite(ItemSlot.Pants);
            spriteShoes = getSprite(ItemSlot.Shoes);
            spriteBackpack = getSprite(ItemSlot.Backpack);
            spriteBackpackStraps = getSprite(ItemSlot.BackpackStraps);

        }

        public CharacterModelSprite getSprite(ItemSlot slot)
        {
               if(EquippedSprites[slot] is not null)
                    return EquippedSprites[slot];
               return blankSprite;
        }

        public void Update(GameTime gameTime)
        {
            /*
                        foreach (Item item in Player.Equipped)
                            item.AnimatedSprite.Update(gameTime);
            */  
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
           setSpriteClothes();
            if(spriteBody is not null)
                spriteBody.Update(gameTime);
            if(spriteHair is not null)
                spriteHair.Update(gameTime);
            if (spriteEyes is not null)
                spriteEyes.Update(gameTime);
            if(spriteShirt is not null)
                spriteShirt.Update(gameTime);
            if(spritePants is not null)
                spritePants.Update(gameTime);
            if (spriteShoes is not null)
                spriteShoes.Update(gameTime);
            if(spriteArms is not null)
                spriteArms.Update(gameTime);
            if (spriteBackpack is not null)
                spriteBackpack.Update(gameTime);
            if(spriteBackpackStraps is not null)
                spriteBackpackStraps.Update(gameTime);
            if(spriteSleeves is not null)
                spriteSleeves.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, float scale = 1f )
        {

            Point temp =new Point(68,55);
           ScreenXY = new Point(ScreenXY.X - spriteBody.Texture.Bounds.X / 2, ScreenXY.Y - spriteBody.Texture.Bounds.Y / 2);
            if (spriteBackpack is not null)
                spriteBackpack.Draw(spriteBatch, ScreenXY, scale);
            if(spriteBody is not null)
                spriteBody.Draw(spriteBatch, ScreenXY, scale);
            if(spriteHair is not null)
                spriteHair.Draw(spriteBatch, ScreenXY - temp, scale);
            if (spriteEyes is not null)
                spriteEyes.Draw(spriteBatch, ScreenXY - temp, scale);
            if(spriteShirt is not null)
                spriteShirt.Draw(spriteBatch, ScreenXY, scale);
            if(spriteArms is not null)
                spriteArms.Draw(spriteBatch, ScreenXY, scale);
            if(spriteSleeves is not null)
                spriteSleeves.Draw(spriteBatch, ScreenXY, scale);
            if(spriteBackpackStraps is not null)
                spriteBackpackStraps.Draw(spriteBatch, ScreenXY, scale);
            if(spritePants is not null)
                spritePants.Draw(spriteBatch, ScreenXY, scale);
            if (spriteShoes is not null)
                spriteShoes.Draw(spriteBatch, ScreenXY, scale);
            if(Font is not null)
                spriteBatch.DrawString(Font, "TEST", ScreenXY.ToVector2(), Color.Black);
            /*foreach (Item item in Player.Equipped)
                item.AnimatedSprite.Draw(spriteBatch, ScreenXY, scale);*/
        }

    }


}