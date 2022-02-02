using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using Newtonsoft.Json;

namespace Thru
{
    public class AnimatedSprite  : IAnimated
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
               public int currentFrame{ get; set; }
        public  int totalFrames{ get; set; }
        public int timeSinceLastFrame{ get; set; }
       public  int millisecondsPerFrame{ get; set; }
        
        public Color Color { get; set; }
        //default constructor
        public AnimatedSprite()
        {
           
        }
        public AnimatedSprite(Texture2D texture, int rows, int columns, Color? color = null)
        {
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 500;
            Texture = texture;
            if (color is null)
                color = Color.White;
            Color = (Color)color;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

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