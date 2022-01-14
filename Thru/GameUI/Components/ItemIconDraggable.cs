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
                return receiver.BoardHome;
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
            ShapeHome = shapeHome;
            receiver = null;
            Button = new Button(group.MouseHandler, icon, shapeHome.ToString(), font, Color.Black, null, .44f );
            
        }
        

        public GameState Update(GameTime gameTime)
        {
            Button.Bounds.Location = CurrentPoint;
            Button.Text = ShapeHome.ToString();
            Button.Update(gameTime);
            return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Button.Draw(spriteBatch);
        }
    }


}