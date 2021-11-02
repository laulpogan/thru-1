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
        private Player _player;
        private Texture2D bg_texture;
        private Vector2 draw_coords;
        private string _stats;
        Vector2 textOffset;
        public PlayerStats(IServiceProvider services, Player player)
        {
            _player = player;
            var Content = new ContentManager(services, "Content");
            bg_texture = Content.Load<Texture2D>("stats_bar");
            font = Content.Load<SpriteFont>("Score");

            textOffset = new Vector2(bg_texture.Width * .05f, bg_texture.Height * 0.1f);
            draw_coords = new Vector2(10, 10);
            Update();
        }

    public void Update()
    {
        _stats = $"Morale: {_player.stats.Morale}\nMoney: ${_player.stats.Money}\nSnacks: {_player.stats.Snacks}";
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(bg_texture, draw_coords, Color.White);
        spriteBatch.DrawString(font, "Stats", draw_coords + textOffset, Color.White);
        var lineY = draw_coords + textOffset + new Vector2(0, 30);
        spriteBatch.DrawLine(lineY, lineY+new Vector2(bg_texture.Width*0.8f,0), Color.White);
        spriteBatch.DrawString(font, _stats, draw_coords + textOffset + new Vector2(0, 50), Color.White);
    }

}
}
