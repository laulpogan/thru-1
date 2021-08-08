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
        public Dictionary<string,Location> AdjacentLocations;
        [JsonInclude]
        public Texture2D Background;
        [JsonInclude]
        public string Name;
        public string ID;
        public Location(Dictionary<string,Location> adjacentLocations, Texture2D image, string name)
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
