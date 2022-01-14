using Newtonsoft.Json;
using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Thru
{
    public class InventoryController
    {

        ArrayList Draggables, Items, Receivers;
        public InventoryGameView InventoryGameView;
        public ButtonGroup ButtonGroup;
        public MouseState mouseState;
        bool mpressed, prev_mpressed = false;

        public InventoryController()
        {
            mpressed = mouseState.LeftButton == ButtonState.Pressed;
        }

        public void checkMenu()
        {
            mouseState = Mouse.GetState();
            prev_mpressed = mpressed;
            mpressed = mouseState.LeftButton == ButtonState.Pressed;
            if (mpressed)
            {
                DragAndDrop();
            }
           
        }
        public bool isOverReceiver(BoardReceiver receiver, ItemIconDraggable draggable)
        {
            return receiver.Bounds.Contains(draggable.CurrentPoint);
        }

        public void DragAndDrop()
        {
            foreach (BoardReceiver receiver in Receivers)
            {
                foreach(ItemIconDraggable draggable in Draggables)
                {
                    if(isOverReceiver(receiver,draggable)){
                       draggable.ScreenHome = receiver.Bounds.Location;
                    }
                }
            }
        }
    }


}