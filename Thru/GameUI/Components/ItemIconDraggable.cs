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
        public Item Item;
        public MouseHandler MouseHandler;
        public Texture2D icon;
        public bool isBeingDragged;
        public DraggableReceiver receiver, oldReceiver;
                                                                    
        public ItemIconDraggable( MouseHandler mouseHandler, Texture2D Icon, Point home, Item item, SpriteFont font = null)
        {
            icon = Icon;
            MouseHandler = mouseHandler;
            ButtonHome = home;
            Button = new Button(mouseHandler, icon);
            Button.Bounds.Location = ButtonHome;
            Item = item;
        }

        public GameState Update(GameTime gameTime)
        {
            switch (MouseHandler.State)
            {
                case BState.DOWN:
                    if (!isBeingDragged && !MouseHandler.isDragging && ThruLib.hit_image_alpha(
                    Button.Bounds, icon, MouseHandler.mx, MouseHandler.my))
                    {
                        MouseHandler.dragged =Item;
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
                    if(receiver is not null && receiver != oldReceiver)
                    {
                        ButtonHome = receiver.receiverHome;
                        receiver.item = Item;
                        if (oldReceiver is not null)
                            oldReceiver.item = null;
                        oldReceiver = receiver;
                        receiver = null;
                    }
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
    }


}