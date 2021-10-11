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
    public class DataBag
    {
        [JsonInclude]
        public string Name{ get; set; }

        public DataBag( string name)
        {
            Name = name;
        }
        public override string ToString() => $"({Name})";
    }

}
