using Microsoft.AspNetCore.Routing;
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
        public MouseHandler MouseHandler;
        public Texture2D icon;
        public bool isBeingDragged;
                                                                    
        public ItemIconDraggable( MouseHandler mouseHandler, Texture2D Icon, Point home, SpriteFont font)
        {
            icon = Icon;
            MouseHandler = mouseHandler;
            ButtonHome = home;
            Button = new Button(mouseHandler, icon);
            Button.Bounds.Location = ButtonHome;
        }

        public GameState Update(GameTime gameTime)
        {
            switch (MouseHandler.State)
            {
                case BState.DOWN:
                    if (!isBeingDragged && !MouseHandler.isDragging && ThruLib.hit_image_alpha(
                    Button.Bounds, icon, MouseHandler.mx, MouseHandler.my))
                    {
                        MouseHandler.dragged = this;
                        MouseHandler.isDragging = true;
                        isBeingDragged = true;
                        Button.Bounds.Location = new Point(MouseHandler.mx, MouseHandler.my);
                    } else if (isBeingDragged && MouseHandler.isDragging)
                    {
                        Button.Bounds.Location = new Point(MouseHandler.mx, MouseHandler.my);
                    }
                    break;
                case BState.UP:
                    break;
                case BState.JUST_RELEASED:
                    MouseHandler.dragged = null;
                    MouseHandler.isDragging = false;
                    isBeingDragged = false;
                    Button.Bounds.Location = ButtonHome;
                    break;
                case BState.HOVER:
                    break;
            }
             return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Button.Draw(spriteBatch);
        }

        public void checkCollision(object sender, EventArgs e)
        {
            if (ThruLib.hit_image_alpha(
              Button.Bounds, icon, MouseHandler.mx, MouseHandler.my))
            {
                if (MouseHandler.dragged != null)
                {
                    if (MouseHandler.State == BState.JUST_RELEASED)
                    {

                    }
                }
            }
        }

    }


}