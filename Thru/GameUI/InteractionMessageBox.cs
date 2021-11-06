﻿using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace Thru
{
    public class InteractionMessageBox
    {
        public SpriteFont font;
        Texture2D Rect;
        Rectangle Bounds;
        Texture2D Picture;
        Vector2 Coor;
        public string Message;
        public string Title;
        private ContentManager Content;
        Vector2 textOffset;

        public InteractionMessageBox( string message, string title, IServiceProvider services, GraphicsDeviceManager graphics)
        {


            Content = new ContentManager(services, "Content");

            Texture2D rect = new Texture2D(graphics.GraphicsDevice, 1000, 250);
            Picture = Content.Load<Texture2D>("Blurp_Scooter");
            textOffset = new Vector2(Picture.Width, 25) + new Vector2(50,25);
            font = Content.Load<SpriteFont>("Score"); 
            Bounds = rect.Bounds;
            Color[] data = new Color[Bounds.Width * Bounds.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);
            Rect = rect;
            Title = title;
              
           Message = WrapText(font,
               message, Rect.Bounds.Width-textOffset.X);
            Coor = new Vector2(500, 600);
        
        }

        public void Update()
        {

            Message = WrapText(font,
            Message, Rect.Bounds.Width - textOffset.X);


        }




       public void  unloadContent()
            {

               // whiteRectangle.Dispose();
            }

       public void Draw(SpriteBatch spriteBatch)
            {
                 spriteBatch.Draw(Rect, Coor, Color.Black);
            spriteBatch.Draw(Picture, Coor + new Vector2(25, 25), Color.White);
            spriteBatch.DrawString(font, Title, Coor + textOffset, Color.White);
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