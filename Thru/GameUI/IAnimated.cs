using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Thru
{
    public interface IAnimated
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame{ get; set; }
        public  int totalFrames{ get; set; }
        public int timeSinceLastFrame{ get; set; }
        public int millisecondsPerFrame{ get; set; }
        public Color Color{ get; set; }



        public void Update(GameTime gameTime)
        {


            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.Point location, float scale = 1f)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture,location.ToVector2(), sourceRectangle, Color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        
    }
}