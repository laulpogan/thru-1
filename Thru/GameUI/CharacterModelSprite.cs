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
    public class CharacterModelSprite : AnimatedSprite
    {
        public int currentFrame
        {
          get {
                if(Model is not null)
                 return Model.currentFrame;
                else
                    return 0;
            }
        }

        public CharacterModel Model;


        public CharacterModelSprite(Texture2D texture, int rows, int columns, CharacterModel model, Color? color = null)
        {
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 500;
            Model = model;
            Texture = texture;
            if (color is null)
                color = Color.White;
            Color = (Color)color;
            Rows = rows;
            Columns = columns;
            totalFrames = Rows * Columns;
        }


    }
}