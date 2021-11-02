using Newtonsoft.Json;
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
        public Button mainMenuButton, mapButton;
        private ContentManager Content;
        ButtonGroup buttonGroup;
        public TextBox DisplayWindow;
        Player Player;

        public HUD(IServiceProvider services, GraphicsDeviceManager graphics, Player player)
        {
            Content = new ContentManager(services, "Content");
            Player = player;
            Texture2D buttonImage = Content.Load<Texture2D>("longButton");
            SpriteFont font = Content.Load<SpriteFont>("Score");
            ArrayList buttonList = new ArrayList();
            Content.RootDirectory = "Content";
            mainMenuButton = new Button(buttonImage, "Main Menu", font);
            mapButton = new Button(buttonImage, "Map", font);
            buttonList.Add(mainMenuButton);
            buttonList.Add(mapButton);
            buttonGroup = new ButtonGroup(buttonList, new Vector2(1000, 10));
            string Message = $"Morale: {Player.stats.Morale} Money: ${Player.stats.Money} Snacks: {Player.stats.Snacks}"; 
            DisplayWindow = new TextBox(Message, "Vitals", services, graphics);

        }

        public void Update(GameTime gameTime)
        {
            buttonGroup.Update(gameTime);
            DisplayWindow.Message = $"Morale: {Player.stats.Morale} Money: ${Player.stats.Money} Snacks: {Player.stats.Snacks}";
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            buttonGroup.Draw(spriteBatch);
            DisplayWindow.Draw(spriteBatch);
        }

    }


}