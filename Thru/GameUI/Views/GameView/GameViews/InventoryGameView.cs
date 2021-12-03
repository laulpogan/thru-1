using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Reflection.Metadata;

namespace Thru
{
    public class InventoryGameView : IGameView
    {

        public DraggableReceiver receiver;
        public ItemIconDraggable draggable;
        private ContentManager Content;
        public ButtonGroup buttonGroup;
        public Texture2D buttonImage;
        public SpriteBatch spriteBatch;

        public InventoryGameView(IServiceProvider services, GraphicsDeviceManager graphics, Player player, GlobalState globalState)
{

            Content = new ContentManager(services, "Content");
            buttonImage = Content.Load<Texture2D>("InterfaceTextures/short_button");
            SpriteFont font = Content.Load<SpriteFont>("Score");
            receiver = new DraggableReceiver(globalState.MouseHandler, buttonImage, new Point(250,500));
            draggable = new ItemIconDraggable(globalState.MouseHandler, buttonImage, new Point(500, 250), font);
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

        }

        public  GameState Update(GameTime gameTime)
        {

            receiver.Update(gameTime);
            draggable.Update(gameTime);
            return GameState.Inventory;
        }

        public  void Draw(GraphicsDeviceManager _graphics)
        {
            spriteBatch.Begin();
            receiver.Draw(spriteBatch);
            draggable.Draw(spriteBatch);
            spriteBatch.End();

        }
    }

     
}