using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class DraggableReceiver
    {

        public Vector2 ScreenXY;

        public DraggableReceiver()
        {

        }

        public GameState Update(GameTime gameTime)
        {
            return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }


}