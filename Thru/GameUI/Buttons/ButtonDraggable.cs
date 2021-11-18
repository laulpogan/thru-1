using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class ButtonDraggable : IButton
    {

        public Vector2 ScreenXY;
        public bool isClicked;

        public ButtonDraggable()
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