using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Thru
{
    public class Location
    {
        public ArrayList AdjacentLocations;
        public Texture2D Background;
        public string Name;
        public Location(ArrayList adjacentLocations, Texture2D image, string name)
        {
            Background = image;
            AdjacentLocations = adjacentLocations;
            Name = name;
            


        }
        public  void Update(GameTime gameTime)
        {
           

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
           
        }


       
    }

}
