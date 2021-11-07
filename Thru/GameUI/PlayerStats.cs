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
    class PlayerStats
    {
        public SpriteFont font;
        private readonly Player _player;
        private Texture2D bgTexture;
        private Vector2 drawCoords;
        private string _stats;
        Vector2 textOffset;
        public PlayerStats(IServiceProvider services, Player player)
        {
            _player = player;
            var Content = new ContentManager(services, "Content");
            bgTexture = Content.Load<Texture2D>("stats_bar");
            font = Content.Load<SpriteFont>("Score");

            textOffset = new Vector2(bgTexture.Width * .05f, bgTexture.Height * 0.1f);
            drawCoords = new Vector2(10, 10);
            Update();
        }

        public void Update()
        {
            // todo add location    \nLocation: {_player.location.Name}
            _stats = $"Name: {_player.TrailName}({_player.Name})\n\nMorale: {_player.stats.Morale}\nMoney: ${_player.stats.Money}\nEnergy: {_player.stats.Energy}\nSnacks: {_player.stats.Snacks}";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bgTexture, drawCoords, Color.White);
            spriteBatch.DrawString(font, "Stats", drawCoords + textOffset, Color.White);
            var lineY = drawCoords + textOffset + new Vector2(0, 30);
            spriteBatch.DrawLine(lineY, lineY + new Vector2(bgTexture.Width * 0.8f, 0), Color.White);
            spriteBatch.DrawString(font, _stats, drawCoords + textOffset + new Vector2(0, 50), Color.White);
        }
    }
}
