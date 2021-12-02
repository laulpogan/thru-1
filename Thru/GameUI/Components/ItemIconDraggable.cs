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
        public Texture2D icon;
                                                                    
        public ItemIconDraggable( MouseHandler mouseHandler, Texture2D Icon, Point home)
        {
            icon = Icon;
            MouseHandler = mouseHandler;
            ButtonHome = home;
            Button = new Button(mouseHandler, icon, "Drag Me");
            Button.Bounds.Location = ButtonHome;
            ScreenXY = Button.Bounds.Location;
        }

        public GameState Update(GameTime gameTime)
        {
            ScreenXY = Button.Bounds.Location;

            switch (Button.State)
            {

                case BState.DOWN:
                    MouseHandler.dragged = this;
                    Button.Bounds.Location = new Point(MouseHandler.mx, MouseHandler.my);
                    break;
                case BState.UP:
                    break;
                case BState.JUST_RELEASED:
                    MouseHandler.dragged = null;
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
              Bounds, icon, MouseHandler.mx, MouseHandler.my))
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