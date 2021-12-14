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

        public ItemIconDraggable[,] Draggables;
        public Point BoardHome,ScreenHome, currentPoint, currentBoardPoint, BoardOrigin;
        public MouseHandler MouseHandler;
        public bool isBeingDragged;
        public DraggableReceiver receiver, oldReceiver;
        public int gridMargin;
        public float Bulk;
       public  Item Item;


        public ItemIconDraggableGroup(MouseHandler mouseHandler, Texture2D icon, Point home, Item item, SpriteFont font = null)
        {
            MouseHandler = mouseHandler;
            Item = item;
            Bulk = Item.ItemShape.GetLength(0);
            BoardHome = home;
            BoardOrigin = home;
            ScreenHome = home;
            currentPoint = home;
            gridMargin = 50;
            ThruLib.printLn(Item.ItemShape);
            Draggables = new ItemIconDraggable[Item.ItemShape.GetLength(0), Item.ItemShape.GetLength(1)];
            Point tempPoint = Point.Zero;
            for (int i = 0; i < Item.ItemShape.GetLength(0); i++)
                for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                    if (item.ItemShape[i, j] == 1)
                    {
                        tempPoint = ThruLib.getInventoryScreenXY(i, j, BoardOrigin, gridMargin);
                        Draggables[i, j] = new ItemIconDraggable(mouseHandler, icon, tempPoint, new Point(i,j), item, this);
                    }

        }

        public GameState Update(GameTime gameTime)
        {
            for (int i = 0; i < Item.ItemShape.GetLength(0); i++)
                for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                    if (Draggables[i, j] is not null)
                        Draggables[i, j].Update(gameTime);
            return GameState.Inventory;
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Item.ItemShape.GetLength(0); i++)
                for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                    if (Draggables[i, j] is not null)
                        Draggables[i, j].Draw(spriteBatch);
        }

        public void adjustGroupPosition(Point point)
        {
            for (int i = 0; i < Item.ItemShape.GetLength(0); i++)
                for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                    if (Draggables[i, j] is not null)
                    {
                       
                        Draggables[i, j].Button.Bounds.Location = getInventoryScreenXY(i,j, point, gridMargin);
                        //Draggables[i, j].BoardHome = tempBoardPoint;
                    }

        }
        public static Point getInventoryScreenXY(int row, int col, Point Home, int marginStep)
        {
            return new Point(Home.X + (row * marginStep), Home.Y + (col * marginStep));
        }
    }


}