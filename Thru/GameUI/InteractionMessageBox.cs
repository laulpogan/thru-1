using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Text;
using FontStashSharp;

namespace Thru
{
    public class InteractionMessageBox
    {
        public SpriteFontBase Font;
        Texture2D Rect;
        Rectangle Bounds;
        Texture2D Picture;
        Vector2 Coor;
        public string Message;
        public string Title;
        private ContentManager Content;
        Vector2 textOffset;

        public InteractionMessageBox( string message, string title, IServiceProvider services, GraphicsDeviceManager graphics, int x, int y, SpriteFontBase font)
        {
            Content = new ContentManager(services, "Content");
            Picture = Content.Load<Texture2D>("Blurp_Scooter");
            textOffset = new Vector2(Picture.Width, 25) + new Vector2(50,25);
            Font = font;
            Rect = ThruLib.makeBlankRect(graphics, x, y);
            Title = title;  
            Message = ThruLib.WrapText(font, message, Rect.Bounds.Width-textOffset.X);
            Coor = new Vector2(500, 600);
        
        }

        public void Update()
        {
            Message = ThruLib.WrapText(Font, Message, Rect.Bounds.Width - textOffset.X);
        }

       public void  unloadContent()
            {
               // whiteRectangle.Dispose();
            }

       public void Draw(SpriteBatch spriteBatch)
            {
                 spriteBatch.Draw(Rect, Coor, Color.Black);
            spriteBatch.Draw(Picture, Coor + new Vector2(25, 25), Color.White);
            spriteBatch.DrawString(Font, Title, Coor + textOffset, Color.White);
            spriteBatch.DrawString(Font,Message, Coor +textOffset + new Vector2(0,70), Color.White);
            // Option One (if you have integer size and coordinates)
            // spriteBatch.Draw(whiteRectangle, new Rectangle(10, 20, 80, 30),
            //          Color.Chocolate);
        }

    }
}
