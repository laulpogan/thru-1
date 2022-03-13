using Microsoft.AspNetCore.Routing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Thru
{
    public class InventoryGameView : IGameView
    {

        public SpriteBatch spriteBatch;
        public InventoryGameBoard GameBoard;
        public BackgroundModel Background;

        public InventoryGameView(IServiceProvider services, GraphicsDeviceManager graphics, Character player, GlobalState globalState)
{
            GameBoard = new InventoryGameBoard(services, globalState.MouseHandler, graphics, player, 5,4, 18, 32, new Point(1025, 250), globalState);
            Background = new BackgroundModel(services, graphics, 0, 0);
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
        }

        public  GameState Update(GameTime gameTime)
        {
           // Player.Update(gameTime);
            GameBoard.Update(gameTime);

            Background.Update(gameTime);
            
            return GameState.Inventory;
        }

        public  void Draw(GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin();
            Background.Draw(graphics);
            GameBoard.Draw(spriteBatch);
            spriteBatch.End();

        }
    }

     
}                      