using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public interface IDraggableContainer
    {
        public ItemIconDraggable iconHeld { get; set; }
        public Point BoardHome { get; set; }
        public Point ScreenHome { get; set; }
        public bool isOnBoard
        {
            get
            {
                return BoardHome.X > -1 && BoardHome.Y > -1;
            }
        }




    }


}