﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static Thru.ItemIconDraggableGroup;
using FontStashSharp;
using System.Text;
using System.IO;

namespace Thru
{
    public class InventoryGameBoard
    {
        public List<Item> draggables;
        public Item BearCan, ColdSoakJar, CookPot, IceAxe, Knife, MountainHouse, RawologyCorkball,
            SawyerBugRepellent, SawyerFilter, SleepingBag, Spoon, Spork, Stove, Tent, ToiletPaper, TrekkingPoles,
            WaterbottleClean, WaterbottleDirty, Shorts, HawaiianShirt2, ClimbingShoes, Backpack, ZpackShirt;
        private ContentManager Content;
        public Texture2D BackpackImage, BearCanImage, ColdSoakJarImage, CookPotImage, IceAxeImage, KnifeImage, MountainHouseImage,
            RawologyCorkballImage, SawyerBugRepellentImage, SawyerFilterImage, SleepingBagImage, SpoonImage, SporkImage, StoveImage,
            TentImage, ToiletPaperImage, TrekkingPolesImage, WaterbottleCleanImage, WaterbottleDirtyImage, ShortsImage, HawaiianShirt2Image, ClimbingShoesImage, ZpacksShirtImage;
        public Character Player;
        //public Backpack Backpack;

        public SpriteBatch spriteBatch;
        public DraggableReceiver[,] receivers;
        public int[,] trueBoard;
        public int[,] board
        {
            get
            {
                for (int i = 0; i < receivers.GetLength(0); i++)
                    for (int j = 0; j < receivers.GetLength(1); j++)
                        if (receivers[i, j].isOccupied)
                            trueBoard[i, j] = 1;
                        else
                            trueBoard[i, j] = 0;


                return trueBoard;
            }
            set { trueBoard = value; }
        }
        public int[,] EmptyBoard;
        public int rows, columns, bloc;
        public Point BoardOrigin;

        public int gridMargin;
        public MouseHandler MouseHandler;
        public FreeSpace FreeSpace;
        public SpriteFontBase Font;
        public PlayerEquipmentModel PlayerModel;
        public GraphicsDeviceManager Graphics;
        IOController dataController;


        public InventoryGameBoard(IServiceProvider services, MouseHandler mouseHandler, GraphicsDeviceManager graphics, Character player, int rows, int columns, int margin, int iconSize, Point boardOrigin, GlobalState globalState)
        {
            dataController = new IOController(services, "GameData/Items.json");

            Content = new ContentManager(services, "Content");
            Font = globalState.FontSystem.GetFont(12);
            PlayerModel = new PlayerEquipmentModel(graphics, mouseHandler, player, margin, iconSize, new Point(200, 650), 4f, Font);
            trueBoard = ThruLib.emptyBoard(rows, columns);
            receivers = new DraggableReceiver[rows, columns];
            bloc = margin + iconSize;
            BoardOrigin = boardOrigin;
            gridMargin = margin + iconSize;
            MouseHandler = mouseHandler;
            FreeSpace = new FreeSpace();
            Graphics = graphics;
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            // Backpack = new Backpack(services, graphics, Player, MouseHandler);
            loadImages();
            Point temp = Point.Zero;
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < columns; col++)
                {
                    temp = new Point(row, col);
                    receivers[temp.X, temp.Y] = new DraggableReceiver(mouseHandler, graphics, temp, this, Font, temp.ToString());

                }


            foreach (Item draggable in draggables)
            {
                foreach (ItemIconDraggable icon in draggable.DraggableGroup.Draggables)
                    if (icon is not null)
                        icon.receiver = FreeSpace;

            }



        }

        public GameState Update(GameTime gameTime)
        {

            bool success = false;
            if (MouseHandler.RState == BState.JUST_RELEASED && MouseHandler.isDragging)
                MouseHandler.ItemDragged.DraggableGroup.Rotate();
            switch (MouseHandler.State)
            {
                case BState.DOWN:
                    if (!MouseHandler.isDragging)
                        foreach (Item item in draggables)
                        {
                            foreach (ItemIconDraggable draggable in item.DraggableGroup.Draggables)
                                if (draggable is not null)
                                    if (ThruLib.hit_image_alpha(draggable.Button.Bounds, draggable.Icon, MouseHandler.mx, MouseHandler.my))
                                    {
                                        handOffIconGroup(draggable, MouseHandler);
                                        success = true;
                                        break;
                                    }
                            if (success)
                                break;
                        }


                    break;
                case BState.UP:
                    break;
                case BState.JUST_RELEASED:
                    if (MouseHandler.isDragging)
                    {
                        for (int i = 0; i < trueBoard.GetLength(0); i++)
                            for (int j = 0; j < trueBoard.GetLength(1); j++)
                                if (ThruLib.hit_image_alpha(receivers[i, j].Bounds, receivers[i, j].Icon, MouseHandler.mx, MouseHandler.my))
                                {
                                    handOffIconGroup(MouseHandler.iconHeld, receivers[i, j]);
                                    success = true;
                                    break;
                                }

                        foreach (EquipmentReceiver receiver in PlayerModel.Receivers)
                            if (ThruLib.hit_image_alpha(receiver.Bounds, receiver.Icon, MouseHandler.mx, MouseHandler.my))
                                if (MouseHandler.iconHeld.Group.ItemSlot == receiver.itemSlot)
                                {
                                    handOffIconGroup(MouseHandler.iconHeld, receiver);
                                    success = true;
                                    break;
                                }

                        if (!success)
                            handOffIconGroup(MouseHandler.iconHeld, FreeSpace);
                    }


                    break;
                case BState.HOVER:
                    for (int i = 0; i < trueBoard.GetLength(0); i++)
                        for (int j = 0; j < trueBoard.GetLength(1); j++)
                            if (ThruLib.hit_image_alpha(receivers[i, j].Bounds, receivers[i, j].Icon, MouseHandler.mx, MouseHandler.my))
                                receivers[i, j].Color = Color.Red;
                            else
                                receivers[i, j].Color = Color.Black;
                    foreach (EquipmentReceiver receiver in PlayerModel.Receivers)
                        if (ThruLib.hit_image_alpha(receiver.Bounds, receiver.Icon, MouseHandler.mx, MouseHandler.my))
                            receiver.Color = Color.Red;
                        else
                            receiver.Color = Color.Black;
                    break;
            }

            foreach (Item item in draggables)
                item.Update(gameTime);
            for (int i = 0; i < receivers.GetLength(0); i++)
                for (int j = 0; j < receivers.GetLength(1); j++)
                    receivers[i, j].Update(gameTime);



            PlayerModel.Update(gameTime);

            return GameState.Inventory;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerModel.Draw(spriteBatch);

            foreach (DraggableReceiver receiver in receivers)
                receiver.Draw(spriteBatch);
            foreach (Item draggable in draggables)
                draggable.Draw(spriteBatch);


        }


        public void handOffIcon(ItemIconDraggable draggable, IDraggableContainer destination)
        {
            draggable.receiver.iconHeld = null;
            destination.iconHeld = draggable;
            draggable.receiver = destination;
        }

        public void handOffIconGroup(ItemIconDraggable icon, IDraggableContainer receiver)
        {
            ItemIconDraggableGroup group = icon.Group;
            switch (receiver.receiverType)
            {
                case InventoryState.InventoryBoard:
                    Point boardShapeOrigin = getBoardShapeOrigin(receiver.BoardHome, icon.ShapeHome);
                    if (isValidMove(icon.Group.ItemShape, board, boardShapeOrigin, rows, columns))
                    {

                        for (int i = 0; i < group.ItemShape.GetLength(0); i++)
                            for (int j = 0; j < group.ItemShape.GetLength(1); j++)
                                if (group.ItemShape[i, j] == 1)
                                    if (group.Draggables[i, j] is not null)
                                        if (receiver.isOnBoard)
                                            handOffIcon(group.Draggables[i, j], fetchReceiver(i, j, boardShapeOrigin));


                    }
                    else
                        foreach (ItemIconDraggable drag in group.Draggables)
                            if (drag is not null)
                                handOffIcon(drag, FreeSpace);

                    break;
                case InventoryState.Equipment:

                    foreach (ItemIconDraggable drag in group.Draggables)
                        if (drag is not null)
                        {
                            handOffIcon(drag, receiver);
                        }


                    break;
                default:
                    foreach (ItemIconDraggable drag in group.Draggables)
                        if (drag is not null)
                            handOffIcon(drag, receiver);
                    break;
            }

            printBoard();
        }



        public DraggableReceiver fetchReceiver(int X, int Y, Point boardShapeOrigin)
        {
            Console.WriteLine("Transforming " + X + ", " + Y + ", and " + boardShapeOrigin + "to get:(" + (boardShapeOrigin.X + X) + "," + (boardShapeOrigin.Y + Y) + ")");
            return receivers[boardShapeOrigin.X + X, boardShapeOrigin.Y + Y];
        }



        public Point getBoardShapeOrigin(Point boardHome, Point shapeHome)
        {


            return boardHome - shapeHome;
        }





        public static bool isValidMove(int[,] itemShape, int[,] board, Point point, int rows, int columns)
        {
            int shapeWidth = itemShape.GetLength(0);
            int shapeHeight = itemShape.GetLength(1);


            /*  if (!isInBounds(itemShape, point, rows, columns))
                  return false;*/
            try
            {
                for (int x = 0; x < shapeWidth; x++)
                    for (int y = 0; y < shapeHeight; y++)
                        if (itemShape[x, y] == 1 && board[x + point.X, y + point.Y] == 1)
                        {
                            Console.WriteLine("Failed to place piece at (" + point + "): collision at (" + x + point.X + "," + y + point.Y + ")");
                            ThruLib.printLn(board);
                            return false;

                        }
            }
            catch (Exception e)
            {
                return false;
            }



            return true;
        }
        public static Point getInventoryScreenXY(int row, int col, Point Origin, int marginStep)
        {
            return new Point(Origin.X + (row * marginStep), Origin.Y + (col * marginStep)); ;
        }

        public void printBoard()
        {

            Console.WriteLine("------------------");
            for (int i = 0; i < receivers.GetLength(0); i++)
            {
                string duh = "";
                for (int j = 0; j < receivers.GetLength(1); j++)
                    if (receivers[i, j].isOccupied)
                        duh += " " + receivers[i, j].Name + "O";
                    else
                        duh += " " + receivers[i, j].Name;
                Console.WriteLine(duh);
            }

            Console.WriteLine("------------------");
            ThruLib.printLn(board);
        }

        public Texture2D PremultiplyTexture(String FilePath, GraphicsDevice device)
        {
            if (String.IsNullOrEmpty(FilePath))
                return null;
            Texture2D texture;
            FilePath = Path.GetFullPath("../../../Content/") + FilePath;
            FileStream titleStream = File.OpenRead(FilePath);
            texture = Texture2D.FromStream(device, titleStream);
            titleStream.Close();
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            texture.SetData(buffer);

            return texture;
        }
        public void loadImages()
        {


            Dictionary<string, ItemData> recordsLoaded = dataController.deserializeFromFile<ItemData>();
            draggables = new List<Item>();
            foreach (string itemName in recordsLoaded.Keys)
            {
                ItemData record = recordsLoaded[itemName];
                Item newItem = new Item(MouseHandler, itemName, PremultiplyTexture(record.iconPath, Graphics.GraphicsDevice), record.screenXY,
                    BoardOrigin, record.isFlexible, record.bulk, record.weight, 1.7f, record.itemShape, record.itemSlot,
                    PremultiplyTexture(record.spriteSheetPath, Graphics.GraphicsDevice), PremultiplyTexture(record.secondarySpriteSheetPath, Graphics.GraphicsDevice));
                newItem.iconPath = record.iconPath;
                newItem.spritePath = record.spriteSheetPath;
                newItem.secondarySpritePath = record.secondarySpriteSheetPath;
                draggables.Add(newItem);
            }



            Dictionary<string, ItemData> records = new Dictionary<string, ItemData>();

            foreach (Item item in draggables)
            {
                records[item.Name] = item.convertToRecord();


            }
            dataController.serializeToFile<ItemData>(records);
        }


    }
}
