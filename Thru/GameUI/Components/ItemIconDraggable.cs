using Microsoft.AspNetCore.Routing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class ItemIconDraggable : IDraggable
    {

        public bool IsClicked;
        public Button Button;
        public Point  ShapeHome, BoardOrigin;
        public Item Item;
        public MouseHandler MouseHandler;
        public Texture2D icon;
        public int gridMargin;
        public IDraggableContainer receiver, oldReceiver;
        public ItemIconDraggableGroup Group;
        public Point BoardHome
        {
            get
            {
                return receiver is not null ? receiver.BoardHome : Point.Zero;
            }
            set
            {

            }
        }
        public Point ScreenHome
        {
            get
            {
                return Group.ScreenHome + ThruLib.multiplyPointByInt(ShapeHome, gridMargin);
            }
            set { }
        }
        public bool isOnBoard
        {
            get
            {
                return receiver is not null;
            }
        }
        public Point CurrentPoint
        {
            get
            {
              return Group.CurrentPoint + ThruLib.multiplyPointByInt(ShapeHome, gridMargin);
            }
            set { }
        }
        public bool isBeingDragged
        {
            get
            {
                return MouseHandler.iconHeld == this;
            }
        }
                             
        public ItemIconDraggable( MouseHandler mouseHandler, Texture2D Icon, int margin, Point boardOrigin, Point home,  Point shapeHome, Item item, ItemIconDraggableGroup group, SpriteFont font = null)
        {
            icon = Icon;
            Group = group;
            MouseHandler = mouseHandler;
            ScreenHome = home;
            BoardOrigin = boardOrigin;
            gridMargin = margin;
            Button = new Button(mouseHandler, icon);
            ShapeHome = shapeHome;
            Item = item;
            receiver = null;
        }
        

        public GameState Update(GameTime gameTime)
        {
            Button.Bounds.Location = CurrentPoint;
            switch (MouseHandler.State)
            {
                case BState.DOWN:
                    if (!isBeingDragged && !MouseHandler.isDragging && ThruLib.hit_image_alpha(
                    Button.Bounds, icon, MouseHandler.mx, MouseHandler.my))
                    {
                        MouseHandler.iconHeld = this;
                        Button.Bounds.Location = new Point(MouseHandler.mx, MouseHandler.my);
                    } else if (isBeingDragged && MouseHandler.isDragging)
                    {
                        Button.Bounds.Location = new Point(MouseHandler.mx, MouseHandler.my);
                    }
                    break;
                case BState.UP:
                    break;
                case BState.JUST_RELEASED:
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