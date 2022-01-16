using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using static Thru.DraggableReceiver;

namespace Thru
{
    public interface IDraggableContainer
    {
        public ItemIconDraggable iconHeld { get; set; }
        public Point BoardHome { get; set; }
        public Point ScreenHome { get; set; }
        public InventoryState receiverType { get; set; }
        public ItemSlot itemSlot { get; set; }
        public bool isOnBoard
        {
            get
            {
                return BoardHome.X > -1 && BoardHome.Y > -1;
            }
        }




    }


}