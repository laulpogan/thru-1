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
                return MouseHandler.ItemDragged == Item;
            }
        }

        public Dictionary<ItemIconDraggable, IDraggableContainer> iconsWithReceivers;
        
        public int gridMargin;
        public float Bulk;
        public Item Item;
        public ItemIconDraggable[,] Draggables;
        public Point ScreenHome;
        public Point findMe;
        public bool isOnBoard
        {
            get
            {
                foreach (ItemIconDraggable draggable in Draggables)
                    if (draggable is not null && !draggable.isOnBoard)
                        return false;

               return true;
            }
        }
        public Point CurrentPoint
        {
            get
            {
                if (isOnBoard)
                    return shapeOrigin.ScreenHome;
                else if(isBeingDragged)
                    return MouseHandler.mXY - ThruLib.multiplyPointByInt(MouseHandler.iconHeld.ShapeHome,gridMargin);
                else
                    return ScreenHome;
            }
            set { }
        }
        public int[,] trueShape;
        public int[,] ItemShape {
            get
            {
                for (int i = 0; i < Draggables.GetLength(0); i++)
                    for (int j = 0; j < Draggables.GetLength(1); j++)
                        if (Draggables[i, j] is not null)
                            trueShape[i, j] = 1;
                        else
                            trueShape[i, j] = 0;
                return trueShape;
            }
            set { trueShape = value; }
        
        }

        public ItemIconDraggable shapeOrigin;
      
      

        public ItemIconDraggableGroup(MouseHandler mouseHandler, Texture2D icon, int[,] itemShape, Point home, Item item, SpriteFont font = null)
        {
            MouseHandler = mouseHandler;
            CurrentPoint = home;
            ScreenHome = home;
            Item = item;
            ItemShape = itemShape;
            gridMargin = 50;
            Point findMe = new Point(1000, 1000);
            Draggables = new ItemIconDraggable[itemShape.GetLength(0), itemShape.GetLength(1)];
            Bulk = itemShape.GetLength(0);
            ItemIconDraggable draggable;
            for (int i = 0; i < itemShape.GetLength(0); i++)
                for (int j = 0; j < itemShape.GetLength(1); j++)
                    if (itemShape[i, j] == 1)
                    {
                        draggable = new ItemIconDraggable(icon, new Point(j, i), item, this, font);
                        if (draggable.ShapeHome.X <= findMe.X && draggable.ShapeHome.Y <= findMe.Y)
                        {
                            shapeOrigin = draggable;
                            findMe = draggable.ShapeHome;
                        }
                        Draggables[i, j] = draggable;
                          
                    }

            printShape();
        }

        public GameState Update(GameTime gameTime)
        {
            foreach (ItemIconDraggable draggable in Draggables)
                if(draggable is not null)
                {
                    draggable.Update(gameTime);
                    if (draggable.ShapeHome.X <= findMe.X && draggable.ShapeHome.Y <= findMe.Y)
                    {
                        shapeOrigin = draggable;
                        findMe = draggable.ShapeHome;
                    }
                }
            return GameState.Inventory;
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (ItemIconDraggable draggable in Draggables)
                if (draggable is not null)
                    draggable.Draw(spriteBatch);
        }

        public void Rotate()
        {
            ThruLib.rotate90DegClockwise<ItemIconDraggable>(Draggables);
            ThruLib.rotate90DegClockwise<int>(ItemShape);

            for (int i = 0; i < ItemShape.GetLength(0); i++)
                for (int j = 0; j < ItemShape.GetLength(1); j++)
                    if (ItemShape[i, j] == 1)
                        if(Draggables[i,j] is not null)
                            Draggables[i,j].ShapeHome = new Point(i,j);

        }
        public void printShape()
        {

            Console.WriteLine("------------------");
            for (int i = 0; i < Draggables.GetLength(0); i++)
            {
                string duh = "";
                for (int j = 0; j < Draggables.GetLength(1); j++)
                    if (ItemShape[i, j] == 1)
                        duh += " " + new Point(i,j).ToString() + "O";
                    else if(Draggables[i,j] is not null)
                        duh += " " + Draggables[i, j].ShapeHome + "A";
                    else
                        duh += " " + new Point(i, j).ToString() + "Q";

                Console.WriteLine(duh);
            }

            Console.WriteLine("------------------");
        }

    }


}