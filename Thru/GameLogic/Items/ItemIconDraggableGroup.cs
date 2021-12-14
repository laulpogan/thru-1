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

        public void adjustGroupPosition(Point point, Point boardPoint)
        {

            for (int i = 0; i < Item.ItemShape.GetLength(0); i++)
                for (int j = 0; j < Item.ItemShape.GetLength(1); j++)
                    if (Draggables[i, j] is not null)
                    {
                        Point tempPoint = point;
                        Point tempBoardPoint = boardPoint;
                        tempPoint.X += i * gridMarginX;
                        tempPoint.Y += j * gridMarginY;
                        tempBoardPoint -= (Draggables[i, j].ShapeHome - tempBoardPoint);
                        Draggables[i, j].Button.Bounds.Location = tempPoint;
                        Draggables[i, j].BoardHome = tempBoardPoint;
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