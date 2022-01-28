using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Text;
using MonoGame;

namespace Thru
{
    class PlayerStatsDisplay
    {
        public SpriteFont font;
        private readonly Character Player;
        private Texture2D bgTexture;
        private Vector2 drawCoords;
        private string _stats;
        Vector2 textOffset;
        public StatusBar MoraleBar, EnergyBar;
        public PlayerStatsDisplay(IServiceProvider services, GraphicsDeviceManager graphics, Character player)
        {
            Player = player;
            var Content = new ContentManager(services, "Content");
            bgTexture = Content.Load<Texture2D>("stats_bar");
            font = Content.Load<SpriteFont>("Score");
            textOffset = new Vector2(bgTexture.Width * .05f, bgTexture.Height * 0.1f);
            drawCoords = new Vector2(10, 10);
            MoraleBar = new StatusBar(graphics, drawCoords.ToPoint() + new Point(0,80) +textOffset.ToPoint(), 250, 15, "Morale", player, false, font); ;
            EnergyBar = new StatusBar(graphics, drawCoords.ToPoint() + new Point(0,110) + textOffset.ToPoint(), 250, 15, "Energy", player,false , font);


        }

        public void Update(GameTime gameTime)
        {
            // todo add location    \nLocation: {_player.location.Name}
            MoraleBar.Update(gameTime);
            EnergyBar.Update(gameTime);
            _stats = $"Name: {Player.TrailName}({Player.Name})\n\n\n\n\n\nMoney: ${Player.Stats.Money}\n\nSnacks: {Player.Stats.Snacks}";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bgTexture, drawCoords, Color.White);
            spriteBatch.DrawString(font, "Stats", drawCoords + textOffset, Color.White);
            var lineY = drawCoords + textOffset + new Vector2(0, 30);
            spriteBatch.DrawLine(lineY, lineY + new Vector2(bgTexture.Width * 0.8f, 0), Color.White);
            try
            {
                spriteBatch.DrawString(font, _stats, drawCoords + textOffset + new Vector2(0, 50), Color.White);
                MoraleBar.Draw(spriteBatch);
                EnergyBar.Draw(spriteBatch);
            }

            catch { } ;
            
        }
    }
}
