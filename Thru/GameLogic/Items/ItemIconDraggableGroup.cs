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
        public int gridMarginX, gridMarginY;
        float Bulk;
        Item Item;


        public ItemIconDraggableGroup(MouseHandler mouseHandler, Texture2D icon, Point home, Item item, SpriteFont font = null)
        {
            MouseHandler = mouseHandler;
            Item = item;
            Bulk = Item.ItemShape.GetLength(0);
            BoardHome = home;
            BoardOrigin = home;
            ScreenHome = home;
            currentPoint = home;
            gridMarginX = 18 + icon.Bounds.Width;
            gridMarginY = gridMarginX;
            Draggables = new ItemIconDraggable[Item.ItemShape.GetLength(0), Item.ItemShape.GetLength(1)];
            Point tempPoint = Point.Zero;

            for (int i = 0; i < Bulk; i++)
                for (int j = 0; j < Bulk; j++)
                    if (item.ItemShape[i,j] == 1)
                    {
                        tempPoint = ThruLib.getInventoryScreenXY(i, j, BoardOrigin, gridMarginX);
                        Draggables[i, j] = new ItemIconDraggable(mouseHandler, icon, tempPoint, new Point(i,j), item, this);
                    }

        }

        public GameState Update(GameTime gameTime)
        {
           

            for (int i = 0; i < Bulk; i++)
                for (int j = 0; j < Bulk; j++)
                    if (Draggables[i, j] is not null && Draggables[i,j].isBeingDragged)
                    {
                        currentPoint = Draggables[i,j].Button.Bounds.Location;
                        currentPoint.X -= i * gridMarginX;
                        currentPoint.Y -= j * gridMarginY;
                        isBeingDragged = true;
                    }

            if (isBeingDragged)
                adjustGroupPosition(currentPoint, currentBoardPoint);
            else
            {
                currentPoint = ScreenHome;
                currentBoardPoint = BoardHome;
            }
                
            isBeingDragged = false;

           
            if (receiver is not null && receiver != oldReceiver)
            {
                BoardHome = receiver.BoardHome - receiver.IconDraggable.ShapeHome;
                ScreenHome = receiver.ScreenHome - ThruLib.pointTransform(receiver.IconDraggable.ShapeHome, gridMarginX);
                receiver.Item = Item;
                if (oldReceiver is not null)
                    oldReceiver.Item = null;
                oldReceiver = receiver;
                receiver = null;
                for (int i = 0; i < Item.ItemShape.GetLength(0); i++)
                    for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                        if (Draggables[i, j] is not null)
                        {
                            Draggables[i, j].ScreenHome = ScreenHome;
                            Draggables[i, j].ScreenHome.X += i * gridMarginX;
                            Draggables[i, j].ScreenHome.Y += j * gridMarginY;
                            Draggables[i, j].BoardHome = BoardHome + new Point(i, j);
                        }
            }

            if (MouseHandler.RState == BState.JUST_RELEASED && MouseHandler.dragged.Draggable.BoardHome == BoardHome)
            {
                Console.WriteLine("Before");
                printLn(Item.ItemShape);
                ThruLib.rotate90DegClockwise<int>(Item.ItemShape);
                ThruLib.rotate90DegClockwise<ItemIconDraggable>(Draggables);

                Console.WriteLine("After");
                printLn(Item.ItemShape);
            

            }

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

        public void printLn(int[,] input) {

            Console.WriteLine("------------------");
            for (int i = 0; i < input.GetLength(0); i++)
            {
                string duh = "";
                for (int j = 0; j < input.GetLength(1); j++)
                    duh += " "+input[i, j].ToString();
                Console.WriteLine(duh);
            }

            Console.WriteLine("------------------");

        }

        public void adjustGroupPosition(Point point, Point boardPoint)
        {

            for(int i = 0; i < Item.ItemShape.GetLength(0); i++)
                for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                    if (Draggables[i, j] is not null)
                    {
                        Point tempPoint = point;
                        Point tempBoardPoint = boardPoint;
                        tempPoint.X += i * gridMarginX;
                        tempPoint.Y += j * gridMarginY;
                        tempBoardPoint -= (Draggables[i, j].ShapeHome - tempBoardPoint);
                        Draggables[i, j].Button.Bounds.Location = tempPoint;
                        Draggables[i,j].BoardHome = tempBoardPoint;
                    }

        }

      /*  public void updateIcons(Point destinationBoardPoint, Point shapeSectionSelected)
        {
            for (int i = 0; i < Item.ItemShape.GetLength(0); i++)
                for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                    if (Draggables[i, j] is not null)
                        if(Draggables[i,j].BoardHome.X < destinationBoardPoint.X)
                            if(Draggables[i,j].ShapeHome.X < shapeSectionSelected.X)
        }


        public void getShapeCoords()
        {
            
            if(MouseHandler.isDragging)

        }*/

    }


}