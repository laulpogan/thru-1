using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using FontStashSharp;

namespace Thru
{
    public class StatusBar 
    {

        public Point ScreenXY, TitleXY;
        public Rectangle BaseBounds, ActualBounds;
        public Texture2D BaseBar, ActualBar;
        public SpriteFontBase Font;
        public string Name, RatioString;
        public string Stat;
        public Character Player;
        int StatMax;
        int oldVal;
        public Color BaseColor;

        public Color TrueActualColor, TrueBaseColor;
        
        public Color ActualColor
        {
            get
            {
                if (AdjustedLength >0)
                {
                    return TrueActualColor;
                }
                else
                    return Color.Transparent;
            }
            set { TrueActualColor = value; }
        }
        public int Width, Height;
        public GraphicsDeviceManager Graphics;
        public bool IsVertical;
        Color TempColor;
        public  int AdjustedLength
        {
            get {
                if (IsVertical)
                    return getProportion(StatMax - StatValue, StatMax, Height);
                else
                    return getProportion(StatValue, StatMax, Width);
            }
        }


        public int StatValue
        {
            get
            {
                return Player.Stats.get(Stat);
            }
        }




        public StatusBar( GraphicsDeviceManager graphics, Point screenXY, int width, int height, string stat, Character player, bool isVertical = false,SpriteFontBase font = null, string name = "", Color? baseColor = null, Color? actualColor = null)
        {
            Player = player;
            Stat = stat;
            StatMax = Player.Stats.maxValue(stat);
            Font = font;
            Name = name;
            BaseColor = (Color)(baseColor is not null ? baseColor : Color.Red);
            ActualColor = (Color)(actualColor is not null ? actualColor : Color.Green);
           
            TitleXY = new Point(screenXY.X, screenXY.Y - 10);
            
            ScreenXY = screenXY;
            Graphics = graphics;
            Width = width;
            Height = height;
            oldVal = 0;
            if (isVertical)
            {
                TempColor = BaseColor;
                BaseColor = ActualColor;
                ActualColor = TempColor;
            }
            BaseBar = ThruLib.makeBlankRect(graphics, width, height);
            BaseBounds = BaseBar.Bounds;
            BaseBounds.Location = screenXY;
            IsVertical = isVertical;
            makeStatusBar();


            
        }

        public GameState Update(GameTime gameTime)
        {
            if (StatValue != oldVal)
            {
                makeStatusBar();
                oldVal = StatValue;
                RatioString = StatValue + "/" + StatMax;
            }

            return GameState.Inventory;
        }



        
        public void makeStatusBar()
        {
            try
            {
                if (IsVertical)
                    ActualBar = ThruLib.makeBlankRect(Graphics, Width, AdjustedLength);
                else
                    ActualBar = ThruLib.makeBlankRect(Graphics, AdjustedLength, Height);
                ActualBounds = ActualBar.Bounds;
            ActualBounds.Location = ScreenXY;
            }   catch(Exception e)
            {

            }


            

        }

        public static int getProportion(int value, int max, int dimension)
        {
            if (value > max)
                value = max;
            float temp = ((float)value / (float)max) * (float)dimension;
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
             spriteBatch.DrawString(Font, Stat, TitleXY.ToVector2() , Color.Black);
            spriteBatch.DrawString(Font, RatioString, findBarCenter(ScreenXY.ToVector2()), Color.White);
            //spriteBatch.DrawString(Font, Stat, TitleXY.ToVector2() , Color.Black, 0f, Vector2.Zero, .75f, new SpriteEffects(), 0f);
           // spriteBatch.DrawString(Font, RatioString, findBarCenter(ScreenXY.ToVector2()), Color.White, 0f, Vector2.Zero, .75f, new SpriteEffects(), 0f);
        }



    }


}