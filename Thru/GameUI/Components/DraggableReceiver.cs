using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class DraggableReceiver
    {

        public Vector2 ScreenXY;
        public Rectangle Bounds;
        public Texture2D icon;
        public MouseHandler MouseHandler;
        public Point screenHome, boardHome;
        public bool isOccupied;
        public Item item;

        public DraggableReceiver(MouseHandler mouseHandler, GraphicsDeviceManager graphics, Point home, Point BoardHome)
        {

            icon = ThruLib.makeBlankRect(graphics, 32, 32);
            MouseHandler = mouseHandler;
            Bounds = icon.Bounds;
            screenHome = home;
            Bounds.Location = screenHome;
            item = null;
        }

        public GameState Update(GameTime gameTime)
        {

            if (ThruLib.hit_image_alpha(
                    Bounds, icon, MouseHandler.mx, MouseHandler.my))
            {
                if(MouseHandler.dragged != null && item == null)
                {
                   if(MouseHandler.State == BState.JUST_RELEASED)
                    {
                        MouseHandler.dragged.Draggable.receiver = this;
                    }
                }
            }
                return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(icon,Bounds , Color.Black);
        }
    }


}