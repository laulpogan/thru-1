using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using CDO;

namespace Thru
{
    public class HUD
    {
        public Button mainMenuButton, mapButton, snackButton;
        private ContentManager Content;
        ButtonGroup buttonGroup;
        private PlayerStats _playerStats;
        Player Player;

        public HUD(IServiceProvider services, GraphicsDeviceManager graphics, Player player)
        {
            Content = new ContentManager(services, "Content");
            Player = player;
            Texture2D buttonImage = Content.Load<Texture2D>("square_button");
            SpriteFont font = Content.Load<SpriteFont>("Score");
            ArrayList buttonList = new ArrayList();
            Content.RootDirectory = "Content";
            mainMenuButton = new Button(buttonImage, "Main Menu", font);
            mapButton = new Button(buttonImage, "Map", font);
            snackButton = new Button(buttonImage, "Eat 1 Snack for 5 Energy", font);
            buttonList.Add(mainMenuButton);
            buttonList.Add(mapButton);
            buttonList.Add(snackButton);
            buttonGroup = new ButtonGroup(buttonList, new Vector2(1700, 10));
            _playerStats = new PlayerStats(services, player);
        }

        public void Update(GameTime gameTime)
        {
            buttonGroup.Update(gameTime);
            _playerStats.Update();
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            buttonGroup.Draw(spriteBatch);
            _playerStats.Draw(spriteBatch);
        }

    }


}