using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using static Thru.DraggableReceiver;
using FontStashSharp;

namespace Thru
{
    public class EquipmentReceiver : IDraggableContainer
    {

        public Vector2 ScreenXY;
        public Rectangle Bounds;
        public Texture2D Icon;
        public MouseHandler MouseHandler;
        public SpriteFontBase Font;
        public string Name;
        public ItemSlot itemSlot { get; set; }
        public Color Color
        {
            get
            {
                if (isOccupied)
                    return Color.Red;
                else
                    return Color.Black;
            }
            set { }
        }
        public Point ScreenHome
        {
            get
            {
                return InventoryGameBoard.getInventoryScreenXY(ModelHome.X, ModelHome.Y, PlayerModel.ModelOrigin, PlayerModel.gridMargin);
            }
            set { }
        }
        public Point BoardHome { get; set; }
        public Point ModelHome;
        public InventoryState receiverType { get; set; }

        public bool isOccupied
        {
            get
            {
                return Item is not null;
            }
        }
        public Item Item
        {
            get
            {
                return iconHeld is not null ? iconHeld.Group.Item : null;
            }
            set { }

        }
        public PlayerEquipmentModel PlayerModel;
        public ItemIconDraggable iconHeld { get; set; }


        public EquipmentReceiver(MouseHandler mouseHandler, GraphicsDeviceManager graphics, Point modelHome, PlayerEquipmentModel playerModel, ItemSlot itemslot, SpriteFontBase font = null, string name = "", Color? color = null)
        {
            receiverType = InventoryState.Equipment;
            Font = font;
            Name = name;
            itemSlot = itemslot;
            Color = (Color)(color is not null ? color : Color.Black);
            Icon = ThruLib.makeBlankRect(graphics, 32, 32);
            MouseHandler = mouseHandler;
            Bounds = Icon.Bounds;
            BoardHome = new Point(-1, -1);
            ModelHome = modelHome;
            iconHeld = null;
            PlayerModel = playerModel;
            Bounds.Location = this.ScreenHome;

        }

        public GameState Update(GameTime gameTime)
        {

            return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Icon, Bounds, Color);
            spriteBatch.DrawString(Font, Name, Bounds.Location.ToVector2(), Color.White);
        }



    }


}