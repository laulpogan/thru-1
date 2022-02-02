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
    public class CharacterModelSprite 
    {
        public Texture2D Texture { get; set; }
        public int Rows   {
          get {
                 return Model.Rows;
            }
        }
        public int Columns  {
          get {
                 return Model.Columns;
            }
        }
        public int totalFrames  {
          get {
                 return Model.totalFrames;
            }
        }
        
        public Color Color { get; set; }
        public int currentFrame
        {
          get {
                 return Model.currentFrame;
            }
        }

        public CharacterModel Model;

        public int timeSinceLastFrame
        {
            get
            {
                return Model.timeSinceLastFrame;
            }
        }

        public int millisecondsPerFrame
        {
            get
            {
                return Model.millisecondsPerFrame;
            }
        }

        public CharacterModelSprite(Texture2D texture, CharacterModel model, Color? color = null)
        {
            Model = model;
            Texture = texture;
            if (color is null)
                color = Color.White;
            Color = (Color)color;
        }

        public void Update(GameTime gameTime) { }
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