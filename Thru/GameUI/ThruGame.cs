using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using FontStashSharp;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using Newtonsoft.Json;


namespace Thru
{
    public class ThruGame : Game
    {
       
        public SpriteFontBase font;
        public GraphicsDeviceManager graphics;
        public GlobalState state;
        private FontSystem _fontSystem;
        public ThruGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }
       
        protected override void Initialize()
        {
                        LoadContent();
            base.Initialize();
        }

        protected override void LoadContent()
        {

        _fontSystem = new FontSystem();
        _fontSystem.AddFont(File.ReadAllBytes(@"Content/Fonts/PressStart2P-Regular.ttf"));
        state = new(Window.ClientBounds.Width, Window.ClientBounds.Height, Services, graphics, _fontSystem);
        }




        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var kstate = Keyboard.GetState();
            state.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            state.Draw(gameTime);
            base.Draw(gameTime);
        }

    }
}
