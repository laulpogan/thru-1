using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class ItemIconDraggable : IDraggable
    {

        public Point ScreenXY;
        public bool IsClicked;
        public Button Button;
        public Point ButtonHome;
        public Item item;
        public Rectangle Bounds;
        public MouseHandler MouseHandler;
                                                                    
        public ItemIconDraggable(Texture2D icon, MouseHandler mouseHandler)
        {
            MouseHandler = mouseHandler;
            Button = new Button(mouseHandler, icon);
            ScreenXY = Button.Bounds.Location;
        }

        public GameState Update(GameTime gameTime)
        {
            ScreenXY = Button.Bounds.Location;

            switch (Button.State)
            {

                case BState.DOWN:
                    Button.Bounds.Location = new Point(MouseHandler.mx, MouseHandler.my);
                    break;
                case BState.UP:
                    break;
                case BState.JUST_RELEASED:
                    Button.Bounds.Location = ButtonHome;
                    break;
                case BState.HOVER:
                    break;

            }

            return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }


}