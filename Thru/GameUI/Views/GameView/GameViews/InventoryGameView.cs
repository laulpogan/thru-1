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

        public SpriteBatch spriteBatch, hudBatch;
        public InventoryGameBoard GameBoard;
        public BackgroundModel Background;
        public HUD hud;


        public InventoryGameView(IServiceProvider services, GraphicsDeviceManager graphics, Character player, GlobalState globalState)
        {
            GameBoard = new InventoryGameBoard(services, globalState.MouseHandler, graphics, player, 5, 4, 18, 32, new Point(1025, 250), globalState);
            Background = new BackgroundModel(services, graphics, 0, 0);
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            hudBatch = new SpriteBatch(graphics.GraphicsDevice);
            hud = new HUD(services, graphics, globalState.Player, globalState);

        }

        public GameState Update(GameTime gameTime)
        {
            // Player.Update(gameTime);
            GameBoard.Update(gameTime);
            GameState returnState = GameState.Inventory;
            Background.Update(gameTime);
            if (hud.mainMenuButton.State == BState.JUST_RELEASED)
                returnState = GameState.Play;
            if (hud.mapButton.State == BState.JUST_RELEASED)
                returnState = GameState.Map;
            if (hud.inventoryButton.State == BState.JUST_RELEASED)
                returnState = GameState.Inventory;
            return returnState;
        }

        public void Draw(GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin();
            Background.Draw(graphics);
            GameBoard.Draw(spriteBatch);
            spriteBatch.End();

            hudBatch.Begin();
            hud.Draw(hudBatch);
            hudBatch.End();
        }
    }


}