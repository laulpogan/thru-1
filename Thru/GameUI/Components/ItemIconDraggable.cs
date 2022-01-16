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
        public Texture2D Icon;
        public IDraggableContainer receiver;
        public ItemIconDraggableGroup Group;
        public 
            Color Color;
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

                if (isOnBoard)
                    return receiver.ScreenHome;
                return InventoryGameBoard.getInventoryScreenXY(ShapeHome.X, ShapeHome.Y, Group.ScreenHome, Group.gridMargin);
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
                if (isOnBoard)
                    return receiver.ScreenHome;
                return InventoryGameBoard.getInventoryScreenXY(ShapeHome.X, ShapeHome.Y, Group.CurrentPoint, Group.gridMargin);
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
                             
        public ItemIconDraggable(Texture2D Icon,  Point shapeHome, Item item, ItemIconDraggableGroup group, SpriteFont font = null, Color? color = null)
        {
            this.Icon = Icon;
            Group = group;
            ShapeHome = shapeHome;
            receiver = null;
            if (color is not null)
                Color = (Color)color;
            else
                Color = Color.White;
            Button = new Button(group.MouseHandler, this.Icon, shapeHome.ToString(), font, color, Color.Black, null, .44f );
            
        }
        

        public GameState Update(GameTime gameTime)
        {
            Button.Bounds.Location = CurrentPoint;
            Button.Text = ShapeHome.ToString();
            if (isBeingDragged)
                Color = Color.Red;
            else if (isOnBoard)
                Color = Color.Purple;
            else
                Color = Color.White;

            Button.Color = Color;

            Button.Update(gameTime);
            return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Button.Draw(spriteBatch);
        }
    }


}