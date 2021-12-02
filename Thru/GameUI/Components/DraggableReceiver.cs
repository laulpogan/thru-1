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
        public Point receiverHome;

        public DraggableReceiver(MouseHandler mouseHandler, Texture2D Icon, Point home)
        {
            MouseHandler = mouseHandler;
            icon = Icon;
            Bounds = icon.Bounds;
            receiverHome = home;

        }

        public GameState Update(GameTime gameTime)
        {

            if (ThruLib.hit_image_alpha(
                    Bounds, icon, MouseHandler.mx, MouseHandler.my))
            {
                if(MouseHandler.dragged != null)
                {
                   if(MouseHandler.State == BState.JUST_RELEASED)
                    {
                        MouseHandler.dragged.ButtonHome = receiverHome; 
                    }
                }
            }

                //if(mx)
                return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }


        void update_button()
        {



          
        }
    }


}