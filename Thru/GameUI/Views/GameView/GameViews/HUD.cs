using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using CDO;
using FontStashSharp;

namespace Thru
{
    public class HUD
    {
        public Button mainMenuButton, mapButton, snackButton, inventoryButton;
        private ContentManager Content;
        ButtonGroup buttonGroup;
        private PlayerStatsDisplay _playerStats;
        Character Player;

        public HUD(IServiceProvider services, GraphicsDeviceManager graphics, Character player, GlobalState globalState)
        {
            Content = new ContentManager(services, "Content");
            Player = player;
            Texture2D buttonImage = Content.Load<Texture2D>("square_button");
            SpriteFontBase font =  globalState.Font;
            ArrayList buttonList = new ArrayList();
            Content.RootDirectory = "Content";
            mainMenuButton = new Button(globalState.MouseHandler, buttonImage, "Main Menu", font);
            mapButton = new Button(globalState.MouseHandler, buttonImage, "Map", font);
            snackButton = new Button(globalState.MouseHandler, buttonImage, "Eat 1 Snack for 5 Energy", font);
            inventoryButton = new Button(globalState.MouseHandler, buttonImage, "Inventory", font);
            buttonList.Add(mainMenuButton);
            buttonList.Add(mapButton);
            buttonList.Add(snackButton);
            buttonList.Add(inventoryButton);
            buttonGroup = new ButtonGroup(buttonList, new Vector2(1700, 10));
            _playerStats = new PlayerStatsDisplay(services, graphics,player, globalState.Font);
        }

        public void Update(GameTime gameTime)
        {
            buttonGroup.Update(gameTime);
            _playerStats.Update(gameTime);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            buttonGroup.Draw(spriteBatch);
            _playerStats.Draw(spriteBatch);
        }

    }


}