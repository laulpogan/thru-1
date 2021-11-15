using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class InventoryGameView : IGameView
    {



        public InventoryGameView()
        {

        }

        public  GameState Update(GameTime gameTime)
        {
            return GameState.Inventory;
        }

        public  void Draw(GraphicsDeviceManager _graphics)
        {

        }
    }

     
}