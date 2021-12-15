using Microsoft.AspNetCore.Routing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Thru
{
    public class ItemIconDraggableGroup : IDraggable
    {
        public MouseHandler MouseHandler;
        public bool isBeingDragged
        {
            get
            {
                return MouseHandler.dragged is not null? MouseHandler.dragged.Draggable == this : false;
            }
        }

        public DraggableReceiver receiver, oldReceiver;
        public int gridMargin;
        public float Bulk;
        public Item Item;
        public ItemIconDraggable[,] Draggables;
        public Point  BoardOrigin;
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
                if (isOnBoard)
                    return ThruLib.getInventoryScreenXY(BoardHome.X, BoardHome.Y, BoardOrigin, gridMargin);
                else
                    return BoardOrigin;
            }
            set { }
        }

        public Point truePoint;
        public Point CurrentPoint
        {
            get
            {
                if (isBeingDragged)
                    return MouseHandler.mXY;
                else
                    return truePoint;
            }
            set { truePoint = value; }
        }
        public bool isOnBoard
        {
            get
            {
                return receiver is not null;    
            }
        }
        
        
        

        public ItemIconDraggableGroup(MouseHandler mouseHandler, Texture2D icon, int[,] itemShape, Point home, Item item, SpriteFont font = null)
        {
            MouseHandler = mouseHandler;
            receiver = null;
            Item = item;
            Bulk = itemShape.GetLength(0);
            BoardHome = home;
            BoardOrigin = home;
            ScreenHome = home;
            gridMargin = 50;
            ThruLib.printLn(itemShape);
            Draggables = new ItemIconDraggable[itemShape.GetLength(0), itemShape.GetLength(1)];
            Point tempPoint = Point.Zero;

          
            for (int i = 0; i < itemShape.GetLength(0); i++)
                for (int j = 0; j < itemShape.GetLength(1); j++)
                    if (itemShape[i, j] == 1)
                    {
                        tempPoint = ThruLib.getInventoryScreenXY(i, j, BoardOrigin, gridMargin);
                        Draggables[i, j] = new ItemIconDraggable(mouseHandler, icon, gridMargin,BoardOrigin, tempPoint, new Point(i,j), item, this);
                    }

        }

        public GameState Update(GameTime gameTime)
        {
            foreach (ItemIconDraggable draggable in Draggables)
                if(draggable is not null) {
                    draggable.Update(gameTime);
                }
            return GameState.Inventory;
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ItemIconDraggable draggable in Draggables)
                if (draggable is not null)
                {
                    draggable.Draw(spriteBatch);
                }
        }

        public void adjustGroupPosition(Point point)
        {
            foreach (ItemIconDraggable draggable in Draggables)
                if (draggable is not null)
                {
                    draggable.Button.Bounds.Location = ThruLib.getInventoryScreenXY(draggable.ShapeHome.X, draggable.ShapeHome.Y, BoardOrigin, gridMargin);
                }
        }

        
    }


}