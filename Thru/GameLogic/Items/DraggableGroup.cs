using Microsoft.AspNetCore.Routing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Thru
{
    public class DraggableGroup : IDraggable
    {

        public ItemIconDraggable[,] Draggables;
        public Point ButtonHome, currentPoint;
        public MouseHandler MouseHandler;
        public bool isBeingDragged;
        public DraggableReceiver receiver, oldReceiver;
        public int gridMarginX, gridMarginY;
        float Bulk;
        Item Item;


        public DraggableGroup(MouseHandler mouseHandler, Texture2D icon, Point home, Item item, SpriteFont font = null)
        {
            MouseHandler = mouseHandler;
            Item = item;
            ButtonHome = home;
            currentPoint = home;
            gridMarginX = 18 + icon.Bounds.Width;
            gridMarginY = gridMarginX;
            Bulk = item.Bulk;
            Draggables = new ItemIconDraggable[(int)Bulk, (int)Bulk];
            Point tempPoint = Point.Zero;

            for (int i = 0; i < Item.ItemShape.GetLength(0); i++)
                for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                    if (item.ItemShape[i,j] == 1)
                    {
                        tempPoint = ThruLib.getInventoryScreenXY(i, j, ButtonHome, gridMarginX);
                        Draggables[i, j] = new ItemIconDraggable(mouseHandler, icon, tempPoint, item);
                    }

        }

        public GameState Update(GameTime gameTime)
        {
           

            for (int i = 0; i < Item.ItemShape.GetLength(0); i++)
                for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                    if (Draggables[i, j] is not null && Draggables[i,j].isBeingDragged)
                    {
                        currentPoint = Draggables[i,j].Button.Bounds.Location;
                        currentPoint.X -= i * gridMarginX;
                        currentPoint.Y -= j * gridMarginY;
                        isBeingDragged = true;
                    }

            if (isBeingDragged)
                adjustGroupPosition(currentPoint);
            else
                currentPoint = ButtonHome;
            isBeingDragged = false;

           
            if (receiver is not null && receiver != oldReceiver)
            {
                ButtonHome = receiver.screenHome;
                receiver.item = Item;
                if (oldReceiver is not null)
                    oldReceiver.item = null;
                oldReceiver = receiver;
                receiver = null;
                for (int i = 0; i < Bulk; i++)
                    for (int j = 0; j < Bulk; j++)
                        if (Draggables[i, j] is not null)
                        {
                            Draggables[i, j].ButtonHome = ButtonHome;
                            Draggables[i, j].ButtonHome.X += i * gridMarginX;
                            Draggables[i, j].ButtonHome.Y += j * gridMarginY;
                        }
            }

            if (MouseHandler.RState == BState.JUST_RELEASED)
            {
                Console.WriteLine("Before");
                printLn(Item.ItemShape);
                ThruLib.rotate90DegClockwise<int>(Item.ItemShape);
                ThruLib.rotate90DegClockwise<ItemIconDraggable>(Draggables);
                Console.WriteLine("After");
                printLn(Item.ItemShape);
                for (int i = 0; i < Item.ItemShape.GetLength(0); i++)
                    for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                        if (Item.ItemShape[i,j] ==1 &&Draggables[i,j] is not null)
                        {
                            Draggables[i, j].ButtonHome = ThruLib.getInventoryScreenXY(i, j, ButtonHome, gridMarginX);
                        }

            }

            for (int i = 0; i < Bulk; i++)
                for (int j = 0; j < Bulk; j++)
                    if (Draggables[i, j] is not null)
                        Draggables[i, j].Update(gameTime);

            return GameState.Inventory;
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < Bulk; i++)
                for (int j = 0; j < Bulk; j++)
                    if (Draggables[i, j] is not null)
                        Draggables[i, j].Draw(spriteBatch);
            
        }

        public void printLn(int[,] input) {

            Console.WriteLine("------------------");
            for (int i = 1; i < input.GetLength(0); i++)
            {
                string duh = "";
                for (int j = 0; j < input.GetLength(1); j++)
                    duh += " "+input[i, j].ToString();
                Console.WriteLine(duh);
            }

            Console.WriteLine("------------------");

        }

        public void adjustGroupPosition(Point point)
        {
            //ThruLib.getInventoryScreenXY(0,0, ButtonHome,gridMarginX) Draggables[0].Button.Bounds.Location = currentPoint;

            for(int i = 1; i < Bulk; i++)
                for (int j = 0; j < Bulk; j++)
                    if (Draggables[i, j] is not null)
                    {
                        Point tempPoint = currentPoint;
                        tempPoint.X += i * gridMarginX;
                        tempPoint.Y += j * gridMarginY;
                        Draggables[i,j].Button.Bounds.Location = tempPoint;
                    }

        }

    }


}