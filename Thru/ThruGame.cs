using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using GeoJSON.Net.Converters;
using GeoJSON.Net.CoordinateReferenceSystem;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using MonoGame;
using System.Threading;

namespace Thru
{
    public class ThruGame : Game
    {
        enum StateMode
        {
            Update,
            Draw
        }
      
        private SpriteFont font;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private State state;
        private Menu menu;
        private MainSettings mainSettings;
        public MainGameView mainGameView;
        public Texture2D background;
        public DisplayWindow displayBox;
        public IOController IOController;
        Location location1, location2, location3;
        Dictionary<string,Location> Locations;
        GameStateController GameStateController;
        StandardBasicEffect basicEffect;
        List<VertexPositionColorTexture> vert;
        short[] ind;
        List<Feature> features;
        double radius = 6371;
        Dictionary<string, double> p0; 
        Dictionary<string, double> p1;
        Dictionary<string, double> pos0;
        Dictionary<string, double> pos1;
        PolygonShape tempShape;
        List<PolygonShape> shapeList;
        public ThruGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            p0 = new Dictionary<string, double>() {
                                { "scrX", 0},        // Minimum X position on screen
                                { "scrY",0},         // Minimum Y position on screen
                                { "lat",32.69145},    // Latitude
                                { "lng",-116.53645}     // Longitude
                            };
            p1 = new Dictionary<string, double>() {
                                { "scrX", Window.ClientBounds.Width},        // Maximum X position on screen
                                { "scrY", Window.ClientBounds.Height},         // Maximum Y position on screen
                                { "lat",32.59114},    // Latitude
                                { "lng",-116.46230}     // Longitude
                            };
            pos0 = latlngToGlobalXY(p0["lat"], p0["lng"]);
            pos1 = latlngToGlobalXY(p1["lat"], p1["lng"]);

        }
        public void setupLocations()
        {
            Locations = new Dictionary<string, Location>();

            location1 = new Location(Locations, background, "southern terminus");
            Locations["southern terminus"] = location1;

            location2 = new Location(Locations, Content.Load<Texture2D>("Moms_Diner"), "mom's diner");
            Locations["mom's diner"] = location2;
            location1.AdjacentLocations["mom's diner"] = location2;

            location3 = new Location(Locations, Content.Load<Texture2D>("triangle"), "7th dimension hyperroom");
            Locations["7th dimension hyperroom"] = location3;
            location2.AdjacentLocations["7th dimension hyperroom"] = location3;
            location1.AdjacentLocations["7th dimension hyperroom"] = location3;

           
            IOController.serializeToFile<Location>(Locations);
            var jsonString = IOController.deserializeFromFile<Location>();
            foreach (string key in jsonString.Keys)
            {
                Console.WriteLine("LOADED: " + key + ": " + jsonString[key]);
            }
        }
        public Dictionary<string, double> latlngToGlobalXY(double lat, double lng)
        {
            double x = radius * lng * Math.Cos((p0["lat"] + p1["lat"]) / 2);
            double y = radius * lat;
            Dictionary<string, double> returnPos = new Dictionary<string, double>() { { "x", x }, { "y", y } };
            return returnPos;
        }

        public Dictionary<string, double> latlngToScreenXY(double lat, double lng)
        {
            Dictionary<string, double> pos = latlngToGlobalXY(lat, lng);
            double perX = ((pos["x"] - pos0["x"]) / (pos1["x"] -  pos0["x"]));
            double perY = ((pos["y"] - pos0["y"]) / (pos1["y"] - pos0["y"]));

            Dictionary<string, double> returnPos = new Dictionary<string, double>() {
                                { "x", p0["scrX"] + (p1["scrX"] - p0["scrX"]) *perX},
                                { "y", p0["scrY"] + (p1["scrY"] - p0["scrY"]) *perY},
                                };
            return returnPos;
        }

        public Dictionary<string, double>  trueAdjuster(double lat, double lng)
        {
            //todo: switch back to client bounds

            double x = Window.ClientBounds.Width / 360 * (180 + lng) ;
            double y = Window.ClientBounds.Height / 180 * (90 - lat);
            Dictionary<string, double> returnPos = new Dictionary<string, double>() { { "x", x }, { "y", y } };
            return returnPos;
        }

        public Dictionary<string, double> thirdTry(double lat, double lng)
        {
            double x = Window.ClientBounds.Width * (180 + lat)/360;
            double y = Window.ClientBounds.Height * (90 - lng)/180;
            Dictionary<string, double> returnPos = new Dictionary<string, double>() { { "x", x }, { "y", y } };
            return returnPos;
        }
        protected override void Initialize()
        {
            state = State.Menu;
            //Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 250);
            IOController = new IOController(Services, "TestPlaces3.json");
            //displayBox = new DisplayWindow(rect, Services);
            menu = new Menu(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
            mainSettings = new MainSettings(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
            background = Content.Load<Texture2D>("southern_terminus");
            setupLocations();
            mainGameView = new MainGameView(location1, Services);
            Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 200);
            displayBox = new DisplayWindow(rect, "", "", Services);
            GameStateController = new GameStateController(Services, displayBox);

            _graphics.PreferredBackBufferWidth = background.Width;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = background.Height;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            /*List<string> geoType = new List<string>(){"Point", "MultiPoint", "LineString",
       "MultiLineString", "Polygon", "MultiPolygon",
       "GeometryCollection"};*/
            List<FeatureCollection> mapDataTotal = loadMapData("C:\\Users\\thein\\Downloads\\output (1)") ?? new List<FeatureCollection>();
            foreach (FeatureCollection mapDataIndividual in mapDataTotal)
            {
                features = mapDataIndividual.Features;
                features.RemoveAll(item => item == null);
                shapeList = new List<PolygonShape>();
                vert = new List<VertexPositionColorTexture>();
                Dictionary<string, double> pos = new Dictionary<string, double>();
                VertexPositionColorTexture tempVPT = new VertexPositionColorTexture();
            foreach (Feature feature in features ?? Enumerable.Empty<Feature>())
                {
                    
                    Console.WriteLine(feature.Geometry.Type);
                    double altitude;

                    switch (feature.Geometry.Type)
                    {
                        case GeoJSONObjectType.LineString:
                            LineString lineString = (LineString)feature.Geometry;
                            foreach (IPosition coord in lineString.Coordinates)
                            {
                                /*Console.WriteLine(coord.Altitude);
                                Console.WriteLine(coord.Latitude);
                                Console.WriteLine(coord.Longitude);*/
                                altitude = coord.Altitude ?? 0;

                                pos = thirdTry(coord.Latitude, coord.Longitude) ?? new Dictionary<string, double>();
                                   /* Console.WriteLine("X: " + coord.Latitude + " Y: " + coord.Longitude);
                                    Console.WriteLine("X: " + pos["x"] + " Y: " + pos["y"]);*/
                                    tempVPT = new VertexPositionColorTexture();
                                tempVPT.Position = new Vector3((float)pos["x"], (float)pos["y"], (float)altitude);
                                tempVPT.TextureCoordinate = new Vector2(0, 0);
                                vert.Add(tempVPT);
                            }
                            break;
                        case GeoJSONObjectType.Polygon:
                            Polygon polygon = (Polygon)feature.Geometry;
                            foreach (LineString lines in polygon.Coordinates)
                            {

                                foreach (IPosition coord in lines.Coordinates)
                                {
                                    altitude = coord.Altitude ?? 0;
                                    //Console.WriteLine(altitude);
                                    //Console.WriteLine(coord.Latitude);
                                    //Console.WriteLine(coord.Longitude);
                                    pos = thirdTry(coord.Latitude, coord.Longitude);
                                    //Console.WriteLine("X: " + coord.Latitude + " Y: " + coord.Longitude);
                                    //Console.WriteLine("X: " + pos["x"] + " Y: " + pos["y"]);
                                    tempVPT = new VertexPositionColorTexture();
                                    tempVPT.Position = new Vector3((float)pos["x"], (float)pos["y"], (float)altitude);
                                    tempVPT.TextureCoordinate = new Vector2((float)pos["x"], (float)pos["y"]);
                                    tempVPT.Color = Color.FloralWhite;
                                    vert.Add(tempVPT);
                                }
                                tempShape = new PolygonShape(_graphics.GraphicsDevice, vert.ToArray());
                                shapeList.Add(tempShape);
                            }

                            break;
                        case GeoJSONObjectType.Point:

                            GeoJSON.Net.Geometry.Point point = (GeoJSON.Net.Geometry.Point)feature.Geometry;
                            altitude = point.Coordinates.Altitude ?? 0;
                            pos = thirdTry(point.Coordinates.Latitude, point.Coordinates.Longitude);
                            tempVPT = new VertexPositionColorTexture();
                            tempVPT.Position = new Vector3((float)pos["x"], (float)pos["y"], (float)altitude);
                            tempVPT.TextureCoordinate = new Vector2((float)pos["x"], (float)pos["y"]);
                            tempVPT.Color = Color.FloralWhite;
                            vert.Add(tempVPT);
                            break;
                        case GeoJSONObjectType.MultiLineString:
                            Console.WriteLine("FOUND A " + GeoJSONObjectType.MultiLineString);
                            break;
                        case GeoJSONObjectType.GeometryCollection:
                            Console.WriteLine("FOUND A " + GeoJSONObjectType.GeometryCollection);
                            break;
                        case GeoJSONObjectType.MultiPoint:
                            Console.WriteLine("FOUND A " + GeoJSONObjectType.MultiPoint);
                            break;
                        case GeoJSONObjectType.MultiPolygon:
                            Console.WriteLine("FOUND A " + GeoJSONObjectType.MultiPolygon);
                            break;
                        default:
                            break;

                    }





                }
            }
            
            basicEffect = new StandardBasicEffect(_graphics.GraphicsDevice);
            basicEffect.TextureEnabled = true;

            

            base.Initialize();
        }

        public FeatureCollection loadMapDataFile(string fileName)
        {
            StringBuilder jsonString = new StringBuilder("");
            Console.WriteLine("Reading from file " + fileName);

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {

                    jsonString.Append(s);
                }
                sr.Close();
                Console.WriteLine(fileName + " closed");
            }
            return JsonConvert.DeserializeObject<FeatureCollection>(jsonString.ToString());
        }
        
        public List<FeatureCollection> loadMapData(string directoryName)
        {
            List<FeatureCollection> mapList = new List<FeatureCollection>();
            foreach(string fileName in Directory.GetFiles(directoryName))
            {
                try
                {
                    mapList.Add(loadMapDataFile(fileName)?? new FeatureCollection());
                }   catch(Exception e)
                {
                    Console.WriteLine(fileName + " " + e.ToString());
                }
            }

            return mapList;
        }
        protected override void LoadContent()
        {


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Score"); // Use the name of your sprite font file here instead of 'Score'.

        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            stateMachine(gameTime, StateMode.Update);
              
           // GameStateController.Update(gameTime);
            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteBatch.Begin();
            stateMachine(gameTime, StateMode.Draw);
            //GameStateController.Draw(_spriteBatch, gameTime);

            foreach (PolygonShape shape in shapeList)
            {
               shape.Draw(basicEffect);
                
                for (int i = shape.vertices.Length-1; i>0; i--)
                {
                    var x1 = shape.vertices[i].Position.X;
                    var x2 = shape.vertices[i-1].Position.X;
                    var y1 = shape.vertices[i].Position.Y ;
                    var y2 = shape.vertices[i-1].Position.Y;

                    _spriteBatch.DrawLine(new Vector2(x1,y1), new Vector2(x2,y2), Color.SeaGreen);
                    ThreadWork.x1 = x1;
                    ThreadWork.x2 = x2;
                    ThreadWork.y1 = y1;
                    ThreadWork.y2 = y2;

                    Thread thread1 = new Thread(ThreadWork.Log);
                    thread1.Start();

                }

            }



            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public class ThreadWork
        {
            public static float x1, x2, y1, y2;
            public static void Log()
            {
                Console.WriteLine("Drawing line from " + x1 + "," + y1 + " to " + x2 + "," + y2);
            }
        }

        private void stateMachine( GameTime gameTime, StateMode stateMode)
        {
            switch (state)
            {
                case State.Menu:
                    state = runState(menu, stateMode, gameTime) ?? state ;
                   // _graphics.GraphicsDevice.Clear(Color.Green);

                    break;
                case State.MainSettings:
                    //_graphics.GraphicsDevice.Clear(Color.Blue);
                    

                    state = runState(mainSettings, stateMode, gameTime) ?? state;
					break;
				case State.Game:
                    state = runState(mainGameView, stateMode, gameTime) ?? state;

                    break;
				/*case State.Final:
					break;
				case State.Road:
					break;
				case State.Town:
					break;
				case State.Trailhead:
					break;
				case State.Start:
					break; */
                default:
                    Console.WriteLine("game is broken bucko: "+state);
                    Exit();
                    break;

            }
        }

        private State? runState(IGameView gameView, StateMode stateMode, GameTime gameTime) 
        {
           
            switch (stateMode)
            {
                case StateMode.Update:
                    return gameView.Update(gameTime);
                     
                case StateMode.Draw:
                    //gameView.Draw(_spriteBatch, _graphics);
                    return null;
                default:
                    Exit();
                    break;
            }
            return null;
        }

    }
}
