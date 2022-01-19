using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class StatusBar 
    {

        public Vector2 ScreenXY, TitleXY;
        public Rectangle BaseBounds, ActualBounds;
        public Texture2D BaseBar, ActualBar;
        public SpriteFont Font;
        public string Name, RatioString;
        public string Stat;
        public Player Player;
        int StatMax;
        int oldVal, newVal;
        public Color BaseColor, ActualColor;
        public int Width, Height;
        public GraphicsDeviceManager Graphics;



        public StatusBar( GraphicsDeviceManager graphics, Point screenXY, int width, int height, string stat, Player player, SpriteFont font = null, string name = "", Color? baseColor = null, Color? actualColor = null)
        {
            Font = font;
            Name = name;
            BaseColor = (Color)(baseColor is not null ? baseColor : Color.Red);
            ActualColor = (Color)(actualColor is not null ? actualColor : Color.Green); ;
            TitleXY = new Vector2(screenXY.X, screenXY.Y - 10);
            
            ScreenXY = screenXY.ToVector2();
            Graphics = graphics;
            Stat = stat;
            Player = player;
            StatMax = Player.Stats.maxValue(stat);
            Width = width;
            Height = height;
            oldVal = 0;
            BaseBar = ThruLib.makeBlankRect(graphics, width, height);
            ActualBar = ThruLib.makeBlankRect(graphics, getWidth(Player.Stats.get(Stat), StatMax), height);
            BaseBounds = BaseBar.Bounds;
            BaseBounds.Location = screenXY;
            ActualBounds = ActualBar.Bounds;
            ActualBounds.Location = screenXY;
        }

        public GameState Update(GameTime gameTime)
        {
            newVal = Player.Stats.get(Stat);
            if (newVal != oldVal)
            {
                ActualBar = ThruLib.makeBlankRect(Graphics, getWidth(newVal,StatMax), Height);
                ActualBounds = ActualBar.Bounds;
                ActualBounds.Location = ScreenXY.ToPoint();
                oldVal = newVal;
                RatioString = newVal + "/" + StatMax;
            }

            return GameState.Inventory;
        }

        public int getWidth(int value, int max)
        {

            float temp = ((float)value / (float)max) * (float)Width;
            return (int)temp;
        }

        public Vector2 findBarCenter(Vector2 point)
        {
            return new Vector2(point.X + Width/3 , point.Y );      
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BaseBar, BaseBounds, BaseColor);
            spriteBatch.Draw(ActualBar, ActualBounds, ActualColor);
            spriteBatch.DrawString(Font, Stat, TitleXY , Color.Black, 0f, Vector2.Zero, .75f, new SpriteEffects(), 0f);
            spriteBatch.DrawString(Font, RatioString, findBarCenter(ScreenXY), Color.White, 0f, Vector2.Zero, .75f, new SpriteEffects(), 0f);
        }



    }


}