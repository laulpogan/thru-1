using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Thru
{
    public class InventoryGameBoard
    {
        public List<Item> draggables;
        public Item BearCan, ColdSoakJar, CookPot, IceAxe, Knife, MountainHouse, RawologyCorkball,
            SawyerBugRepellent, SawyerFilter, SleepingBag, Spoon, Spork, Stove, Tent, ToiletPaper, TrekkingPoles,
            WaterbottleClean, WaterbottleDirty;
        private ContentManager Content;
        public Texture2D BearCanImage, ColdSoakJarImage, CookPotImage, IceAxeImage, KnifeImage, MountainHouseImage,
            RawologyCorkballImage, SawyerBugRepellentImage, SawyerFilterImage, SleepingBagImage, SpoonImage, SporkImage, StoveImage,
            TentImage, ToiletPaperImage, TrekkingPolesImage, WaterbottleCleanImage, WaterbottleDirtyImage;
        public Player Player;
        public Backpack Backpack;

        public Rectangle Bounds;
        public SpriteBatch spriteBatch;
        public BoardReceiver[,] receivers;
        public int[,] trueBoard;
        public int[,] board
        {
            get {
                for (int i = 0; i < trueBoard.GetLength(0); i++)
                    for (int j = 0; j < trueBoard.GetLength(1); j++)
                        if (receivers[i, j].isOccupied) 
                            trueBoard[receivers[i, j].BoardHome.X, receivers[i, j].BoardHome.Y] = 1;
                        else
                            trueBoard[receivers[i, j].BoardHome.X, receivers[i, j].BoardHome.Y] = 0;


                return trueBoard;}
            set {}
        }
        public int[,] EmptyBoard;
        public int rows, columns, bloc;
        public Point BoardOrigin;
        public int gridMargin;
        public MouseHandler MouseHandler;
        public FreeSpace FreeSpace;
       
     
        public InventoryGameBoard(IServiceProvider services ,MouseHandler mouseHandler, GraphicsDeviceManager graphics, Player player, int rows, int columns, int margin, int iconSize, Point boardOrigin)
        {
            Player = player;
            Content = new ContentManager(services, "Content");
            trueBoard = ThruLib.emptyBoard(rows, columns);
            receivers = new BoardReceiver[rows,columns];
            bloc = margin + iconSize;
            BoardOrigin = boardOrigin;
            gridMargin = margin + iconSize;
            MouseHandler = mouseHandler;
            FreeSpace = new FreeSpace();
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            Backpack = new Backpack(services, graphics, Player, MouseHandler);
            loadImages();

            for (int row = 0; row < rows; row ++)
                for (int col = 0; col < columns; col ++)
                    receivers[row,col] = new BoardReceiver(mouseHandler, graphics, new Point(row,col), this);

            foreach (Item draggable in draggables)
                foreach(ItemIconDraggable icon in draggable.DraggableGroup.Draggables)
                    if(icon is not null)
                        icon.receiver = FreeSpace;
        }


        public GameState Update(GameTime gameTime)
        {

            if (MouseHandler.RState == BState.JUST_RELEASED && MouseHandler.isDragging)
                MouseHandler.ItemDragged.DraggableGroup.Rotate();
            BoardReceiver firstReceiver = null;
            switch (MouseHandler.State)
                {
                    case BState.DOWN:
                    if (!MouseHandler.isDragging)
                    {
                        foreach (Item item in draggables)
                            foreach (ItemIconDraggable draggable in item.DraggableGroup.Draggables)
                                if (draggable is not null)
                                    if (ThruLib.hit_image_alpha(draggable.Button.Bounds, draggable.icon, MouseHandler.mx, MouseHandler.my))
                                        pickUpIconGroup(draggable);
                    }
                    else
                        MouseHandler.iconHeld.Group.CurrentPoint = MouseHandler.mXY -  ThruLib.multiplyPointByInt(MouseHandler.iconHeld.ShapeHome, gridMargin);

                        break;
                    case BState.UP:
                        break;
                    case BState.JUST_RELEASED:
                        if (MouseHandler.isDragging)
                    {
                        bool success = true;
                        for (int i = 0; i < trueBoard.GetLength(0); i++)
                            for (int j = 0; j < trueBoard.GetLength(1); j++)
                                if (ThruLib.hit_image_alpha(receivers[i, j].Bounds, receivers[i, j].Icon, MouseHandler.mx, MouseHandler.my))
                                    if (!ThruLib.isValidMove(MouseHandler.iconHeld.Group.ItemShape, board, getBoardShapeOrigin(receivers[i, j].BoardHome, MouseHandler.iconHeld.ShapeHome), rows, columns))
                                        success = false;
                                    else
                                        if (firstReceiver is null)
                                    {
                                        firstReceiver = receivers[i, j];
                                    }
                        if (success && firstReceiver is not null)
                        {
                           Point temp = getBoardShapeOrigin(firstReceiver.BoardHome, MouseHandler.iconHeld.ShapeHome);

                            handOffIconGroup(MouseHandler.ItemDragged.DraggableGroup, firstReceiver);

                        }
                        else
                            returnIconGroupHome(MouseHandler.ItemDragged.DraggableGroup);
                    }
                            
                    break;
                    case BState.HOVER:
                        break;
                }


            for (int i = 0; i < trueBoard.GetLength(0); i++)
                for (int j = 0; j < trueBoard.GetLength(1); j++)
                    receivers[i,j].Update(gameTime);
            foreach (Item item in draggables)
                item.Update(gameTime);

            


            return GameState.Inventory;
        }

        public void pickUpIconGroup(ItemIconDraggable draggable)
        {
            draggable.receiver.iconHeld = null;
            MouseHandler.iconHeld = draggable;
            draggable.receiver = MouseHandler;
        }
        public void handOffIcon(ItemIconDraggable draggable, IDraggableContainer destination)
        {

            if (draggable.receiver is not null)
                draggable.receiver.iconHeld = null;
            destination.iconHeld = draggable;
            draggable.receiver = destination;
            draggable.Group.iconsWithReceivers.Add(draggable, destination);
        }
        
        public void handOffIconGroup(ItemIconDraggableGroup group, IDraggableContainer receiver)
        {
            ThruLib.printLn(board);
            group.ScreenHome = receiver.BoardHome;
            group.iconsWithReceivers = new Dictionary<ItemIconDraggable, IDraggableContainer>();
             for (int i = 0; i < group.GetLength(0); i++)
                for (int j = 0; j < trueBoard.GetLength(1); j++)
                    handOffIcon(draggable, receivers[receiver.BoardHome.X + draggable.ShapeHome.X, receiver.BoardHome.Y + draggable.ShapeHome.Y]);
        }


        public void returnIconGroupHome(ItemIconDraggableGroup group)
        {
            
            group.CurrentPoint = group.ScreenHome;
            foreach (ItemIconDraggable draggable in group.Draggables)
                if (draggable is not null)
                {
                    if (draggable.receiver is not null)
                        draggable.receiver.iconHeld = null;
                    draggable.receiver = FreeSpace;
                }
        }

    public Point getBoardShapeOrigin(Point boardHome, Point shapeHome)
        {

            Point point = boardHome - shapeHome;
            Console.WriteLine(point);


            return point ;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Backpack.Draw(spriteBatch);
            foreach (BoardReceiver receiver in receivers)
            {
                receiver.Draw(spriteBatch);
            }
            foreach (Item draggable in draggables)
            {
                draggable.Draw(spriteBatch);
            }
        }
public void loadImages()
        {

            
            BearCanImage = Content.Load<Texture2D>("ItemIcons/Bearcan32x32");
            ColdSoakJarImage = Content.Load<Texture2D>("ItemIcons/ColdSoakJar32x32");
            CookPotImage = Content.Load<Texture2D>("ItemIcons/CookPot32x32");
            IceAxeImage = Content.Load<Texture2D>("ItemIcons/IceAx32x32");
            KnifeImage = Content.Load<Texture2D>("ItemIcons/Knife32x32");
            MountainHouseImage = Content.Load<Texture2D>("ItemIcons/MountainHouse32x32");
            RawologyCorkballImage = Content.Load<Texture2D>("ItemIcons/RawologyCorkballLarge32x32");
            SawyerBugRepellentImage = Content.Load<Texture2D>("ItemIcons/Sawyer-BugRepellent32x32");
            SawyerFilterImage = Content.Load<Texture2D>("ItemIcons/Sawyer-Filter32x32");
            SleepingBagImage = Content.Load<Texture2D>("ItemIcons/SleepingBag132x32");
            SpoonImage = Content.Load<Texture2D>("ItemIcons/Spoon32x32");
            SporkImage = Content.Load<Texture2D>("ItemIcons/Spork32x32");
            StoveImage = Content.Load<Texture2D>("ItemIcons/Stove32x32");
            TentImage = Content.Load<Texture2D>("ItemIcons/Tent132x32");
            ToiletPaperImage = Content.Load<Texture2D>("ItemIcons/ToiletPaper32x32");
            TrekkingPolesImage = Content.Load<Texture2D>("ItemIcons/TrekkingPoles32x32");
            WaterbottleCleanImage = Content.Load<Texture2D>("ItemIcons/Waterbottle-CLEAN32x32");
            WaterbottleDirtyImage = Content.Load<Texture2D>("ItemIcons/Waterbottle-DIRTY32x32");
            int[,] itemShape = new int[,]{
               { 1, 1, 1},
                { 0, 1, 0},
{ 0, 0, 0}
};
            SpriteFont font = Content.Load<SpriteFont>("Score");
            
            BearCan = new Item(MouseHandler, BearCanImage, new Point(500, 250), false, 4, 1.7f, 0, itemShape);
            ColdSoakJar = new Item(MouseHandler, ColdSoakJarImage, new Point(550, 250), false, 4, 1.7f, 0, itemShape);
            CookPot = new Item(MouseHandler, CookPotImage, new Point(600, 250), false, 4, 1.7f, 0, itemShape);
            IceAxe = new Item(MouseHandler, IceAxeImage, new Point(650, 250), false, 4, 1.7f, 0, itemShape);
            Knife = new Item(MouseHandler, KnifeImage, new Point(700, 250), false, 4, 1.7f, 0, itemShape);
            MountainHouse = new Item(MouseHandler, MountainHouseImage, new Point(500, 300), false, 4, 1.7f, 0, itemShape);
            RawologyCorkball = new Item(MouseHandler, RawologyCorkballImage, new Point(500, 350), false, 4, 1.7f, 0, itemShape);
            SawyerBugRepellent = new Item(MouseHandler, SawyerBugRepellentImage, new Point(500, 400), false, 4, 1.7f, 0, itemShape);
            SawyerFilter = new Item(MouseHandler, SawyerFilterImage, new Point(550, 300), false, 4, 1.7f, 0, itemShape);
            SleepingBag = new Item(MouseHandler, SleepingBagImage, new Point(600, 300), false, 4, 1.7f, 0, itemShape);
            Spoon = new Item(MouseHandler, SpoonImage, new Point(650, 300), false, 4, 1.7f, 0, itemShape);
            Spork = new Item(MouseHandler, SporkImage, new Point(700, 300), false, 4, 1.7f, 0, itemShape);
            Stove = new Item(MouseHandler, StoveImage, new Point(550, 350), false, 4, 1.7f, 0, itemShape);
            Tent = new Item(MouseHandler, TentImage, new Point(600, 350), false, 4, 1.7f, 0, itemShape);
            ToiletPaper = new Item(MouseHandler, ToiletPaperImage, new Point(650, 350), false, 4, 1.7f, 0, itemShape);
            TrekkingPoles = new Item(MouseHandler, TrekkingPolesImage, new Point(700, 350), false, 4, 1.7f, 0, itemShape);
            WaterbottleClean = new Item(MouseHandler, WaterbottleCleanImage, new Point(550, 400), false, 4, 1.7f, 0, itemShape);
            WaterbottleDirty = new Item(MouseHandler, WaterbottleDirtyImage, new Point(600, 400), false, 4, 1.7f, 0, itemShape);
            draggables = new List<Item>(){
                BearCan, ColdSoakJar, CookPot, IceAxe, Knife, MountainHouse, RawologyCorkball,
                SawyerBugRepellent, SawyerFilter, SleepingBag, Spoon, Spork, Stove, Tent, ToiletPaper, TrekkingPoles,
                WaterbottleClean, WaterbottleDirty
        };
        }


            }
        }
       