using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class StatusBar 
    {

        public Vector2 ScreenXY;
        public Rectangle BaseBounds, ActualBounds;
        public Texture2D BaseBar, ActualBar;
        public MouseHandler MouseHandler;
        public SpriteFont Font;
        public string Name;
        public Stats Stat;
        public Player Player;
        int StatMax;
        int oldVal, newVal;
        public Color BaseColor, ActualColor;
        public int Width, Height;
        public GraphicsDeviceManager Graphics;



        public StatusBar(MouseHandler mouseHandler, GraphicsDeviceManager graphics, Point screenXY, int width, int height,Stats stat, SpriteFont font = null, string name = "", Color? baseColor = null, Color? actualColor = null)
        {
            Font = font;
            Name = name;
            BaseColor = (Color)(baseColor is not null ? baseColor : Color.Red);
            ActualColor = (Color)(actualColor is not null ? actualColor : Color.White); ;
            BaseBar = ThruLib.makeBlankRect(graphics, width, height);
            ActualBar = ThruLib.makeBlankRect(graphics, width, height);
            MouseHandler = mouseHandler;
            BaseBounds = BaseBar.Bounds;
            BaseBounds.Location = screenXY;
            ActualBounds = ActualBar.Bounds;
            ActualBounds.Location = screenXY;
            ScreenXY = screenXY.ToVector2();
            Graphics = graphics;
            Stat = stat;
            StatMax = Player.Stats.maxValue(Stat.ToString());
            Width = width;
            Height = height;
        }

        public GameState Update(GameTime gameTime)
        {
            newVal = Player.Stats.get(Stat.ToString());
            if (newVal != oldVal)
            {
                int difference = StatMax - newVal;
                ActualBar = ThruLib.makeBlankRect(Graphics, Width, difference);
            }

            return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BaseBar, BaseBounds, BaseColor);
            spriteBatch.Draw(ActualBar, ActualBounds, ActualColor);
            spriteBatch.DrawString(Font, Name, BaseBounds.Location.ToVector2(), Color.White, 0f, Vector2.Zero, .25f, new SpriteEffects(), 0f);
        }



    }


}