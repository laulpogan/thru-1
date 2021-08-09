using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;


namespace Thru
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Location
    {
         [JsonIgnore]
        public Dictionary<string,Location> AdjacentLocations;
        [JsonIgnore]
        public Texture2D Background;
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name;

        [JsonProperty(PropertyName = "Id")]
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
