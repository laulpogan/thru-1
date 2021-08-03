using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace Thru
{
    public class DisplayWindow
    {
        public SpriteFont font;
        Texture2D Rect;
        string text;
        Rectangle Bounds;
        Texture2D Picture;
        GraphicsDevice GraphicsDevice;
        Vector2 Coor;
        string Message;
        string Speaker;
        private ContentManager Content;
        Vector2 textOffset;

        public DisplayWindow(Texture2D rect, IServiceProvider services)
        {
            Content = new ContentManager(services, "Content");
            Picture = Content.Load<Texture2D>("Blurp_Scooter");
            textOffset = new Vector2(Picture.Width, 25) + new Vector2(50,25);
            font = Content.Load<SpriteFont>("Score"); // Use the name of your sprite font file here instead of 'Score'.

            Color[] data = new Color[1000 * 250];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);
            Rect = rect;
            Speaker = "Blurp Scooter:";
              
           Message = WrapText(font,
                "Are you starting the PCT today? Make sure you sign the logbook. You wouldn't believe how many people forget. ;)", Rect.Width-textOffset.X);
            Coor = new Vector2(600, 800);
        
        }

        public void Update()
        {




        }




       public void  unloadContent()
            {

               // whiteRectangle.Dispose();
            }

       public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
            {
                 spriteBatch.Draw(Rect, Coor, Color.Black);
            spriteBatch.Draw(Picture, Coor + new Vector2(25, 25), Color.White);
            spriteBatch.DrawString(font, Speaker, Coor + textOffset, Color.White);
            spriteBatch.DrawString(font,Message, Coor +textOffset + new Vector2(0,70), Color.White);


            // Option One (if you have integer size and coordinates)
            // spriteBatch.Draw(whiteRectangle, new Rectangle(10, 20, 80, 30),
            //          Color.Chocolate);



        }
        public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }

    }
}
