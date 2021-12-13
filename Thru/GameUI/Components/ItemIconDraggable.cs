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
        public Point ScreenHome, PreviousHome, ShapeHome, BoardHome;
        public Item Item;
        public MouseHandler MouseHandler;
        public Texture2D icon;
        public bool isBeingDragged;
        public DraggableReceiver receiver, oldReceiver;
        public ItemIconDraggableGroup Group;
                                                                    
        public ItemIconDraggable( MouseHandler mouseHandler, Texture2D Icon, Point home,  Point shapeHome, Item item, ItemIconDraggableGroup group, SpriteFont font = null)
        {
            icon = Icon;
            Group = group;
            MouseHandler = mouseHandler;
            ScreenHome = home;
            PreviousHome = home;
            Button = new Button(mouseHandler, icon);
            Button.Bounds.Location = ScreenHome;
            ShapeHome = shapeHome;
            isBeingDragged = false;
            Item = item;
        }
        
        //todo make this mouse checking occur a level up in the group? Or even just on the board

        public GameState Update(GameTime gameTime)
        {

           
            switch (MouseHandler.State)
            {
                case BState.DOWN:
                    if (!isBeingDragged && !MouseHandler.isDragging && ThruLib.hit_image_alpha(
                    Button.Bounds, icon, MouseHandler.mx, MouseHandler.my))
                    {
                        MouseHandler.dragged =Item;
                        MouseHandler.iconDragged = this;
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
                        PreviousHome = ScreenHome;
                        ScreenHome = receiver.ScreenHome;
                        receiver.Item = Item;
                        if (oldReceiver is not null)
                            oldReceiver.Item = null;
                        oldReceiver = receiver;
                        receiver = null;
                    }
                    Button.Bounds.Location = ScreenHome;
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