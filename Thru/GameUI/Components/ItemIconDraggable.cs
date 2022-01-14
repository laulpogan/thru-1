using Microsoft.AspNetCore.Routing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class ItemIconDraggable : IDraggable
    {

        public Button Button;
        public Point  ShapeHome;
        public Texture2D icon;
        public IDraggableContainer receiver;
        public ItemIconDraggableGroup Group;
        public Point BoardHome
        {
            get
            {
                return receiver.BoardHome + ShapeHome;
            }
            set
            {

            }
        }
        public Point ScreenHome
        {
            get
            {
                return Group.ScreenHome + ThruLib.multiplyPointByInt(ShapeHome, Group.gridMargin);
            }
            set { }
        }
        public bool isOnBoard
        {
            get
            {
                return BoardHome.X > -1 && BoardHome.Y >-1;
            }
        }
        public Point CurrentPoint
        {
            get
            {
              return Group.CurrentPoint + ThruLib.multiplyPointByInt(ShapeHome, Group.gridMargin);
            }
            set { }
        }
        public bool isBeingDragged
        {
            get
            {
                return receiver == Group.MouseHandler;
            }
        }
                             
        public ItemIconDraggable(Texture2D Icon,  Point shapeHome, Item item, ItemIconDraggableGroup group, SpriteFont font = null)
        {
            icon = Icon;
            Group = group;
            Button = new Button(group.MouseHandler, icon);
            ShapeHome = shapeHome;
            receiver = null;
        }
        

        public GameState Update(GameTime gameTime)
        {
            Button.Bounds.Location = CurrentPoint;
            Button.Update(gameTime);
            return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Button.Draw(spriteBatch);
        }
    }


}