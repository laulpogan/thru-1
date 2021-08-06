using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Thru
{
    public class Location
    {
        [JsonIgnore]
        public ArrayList AdjacentLocations;
        [JsonInclude]
        public Texture2D Background;
        [JsonInclude]
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
