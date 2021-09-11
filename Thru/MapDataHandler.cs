using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    public class MapDataHandler
    {

        StandardBasicEffect basicEffect;
        List<VertexPositionColorTexture> vert;
        List<List<VertexPositionColorTexture>> allShapes;
        short[] ind;
        List<Feature> features;
        double radius = 6371;
        Dictionary<string, double> p0;
        Dictionary<string, double> p1;
        Dictionary<string, double> pos0;
        Dictionary<string, double> pos1;
        PolygonShape tempShape;
        List<PolygonShape> shapeList;
        int ClientWidth, ClientHeight;
        int baseLat= 32, baseLng =-116;
        public MapDataHandler(int clientWidth, int clientHeight)
        {
            ClientWidth = clientWidth;
            ClientHeight = clientHeight;
            p0 = new Dictionary<string, double>() {
                                { "scrX", 0},        // Minimum X position on screen
                                { "scrY",0},         // Minimum Y position on screen
                                { "lat",32.69145},    // Latitude
                                { "lng",-116.53645}     // Longitude
                            };
            p1 = new Dictionary<string, double>() {
                                { "scrX",1000},        // Maximum X position on screen
                                { "scrY", 700},         // Maximum Y position on screen
                                { "lat",32.59114},    // Latitude
                                { "lng",-116.46230}     // Longitude
                            };
            pos0 = latlngToGlobalXY(p0["lat"], p0["lng"]);
            pos1 = latlngToGlobalXY(p1["lat"], p1["lng"]);
            List<FeatureCollection> mapDataTotal =   new List<FeatureCollection>();
            //    loadMapData("G:\\Users\\thein\\Downloads\\output (1)") ?? new List<FeatureCollection>();//
            mapDataTotal.Add(loadMapDataFile("G:\\Users\\thein\\Downloads\\output (1)\\Elev_Contour2.json"));
                                                                                                                   // 
            vert = new List<VertexPositionColorTexture>();
            allShapes = new List<List<VertexPositionColorTexture>>();
            foreach (FeatureCollection mapDataIndividual in mapDataTotal)
            {
                Console.WriteLine("vertices list size: " + vert.Count);
                features = mapDataIndividual.Features;
                //features.RemoveAll(item => item == null);
                shapeList = new List<PolygonShape>();
                List<VertexPositionColorTexture> tempVerts = new List<VertexPositionColorTexture>();
                foreach (Feature feature in features ?? Enumerable.Empty<Feature>())
                {
                    switch (feature.Geometry.Type)
                    {
                        case GeoJSONObjectType.LineString:
                            LineString lineString = (LineString)feature.Geometry;
                            var q = logCoords(lineString.Coordinates);
                            vert = (List<VertexPositionColorTexture>)vert.Concat<VertexPositionColorTexture>(q).ToList();
                            allShapes.Add(q);
                            break;
                        case GeoJSONObjectType.Polygon:
                            Polygon polygon = (Polygon)feature.Geometry;

                            foreach (LineString lines in polygon.Coordinates)
                            {
                                var tempLine = logCoords(lines.Coordinates);
                                vert = (List<VertexPositionColorTexture>)vert.Concat<VertexPositionColorTexture>(tempLine).ToList();
                                allShapes.Add(tempLine);
                            }
                            break;
                        case GeoJSONObjectType.Point:
                            GeoJSON.Net.Geometry.Point point = (GeoJSON.Net.Geometry.Point)feature.Geometry;
                            vert.Add(logCoord(point.Coordinates));
                            break;
                        case GeoJSONObjectType.MultiLineString:
                            MultiLineString multiLineString = (MultiLineString)feature.Geometry;
                            foreach (LineString subLine in multiLineString.Coordinates)
                            {
                                var tempSub = logCoords(subLine.Coordinates);
                                vert = (List<VertexPositionColorTexture>)vert.Concat<VertexPositionColorTexture>(tempSub).ToList();
                                allShapes.Add(tempSub);
                            }
                            break;
                        case GeoJSONObjectType.GeometryCollection:
                            Console.WriteLine("FOUND A " + GeoJSONObjectType.GeometryCollection);
                            break;
                        case GeoJSONObjectType.MultiPoint:
                            MultiPoint multiPoint = (MultiPoint)feature.Geometry;
                            foreach (GeoJSON.Net.Geometry.Point tempPoint in multiPoint.Coordinates)
                                vert.Add(logCoord(tempPoint.Coordinates));
                            break;
                        case GeoJSONObjectType.MultiPolygon:
                            Console.WriteLine("FOUND A " + GeoJSONObjectType.MultiPolygon);
                            break;
                        default:
                            break;
                    }
                }
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
            double perX = ((pos["x"] - pos0["x"]) / (pos1["x"] - pos0["x"]));
            double perY = ((pos["y"] - pos0["y"]) / (pos1["y"] - pos0["y"]));

            Dictionary<string, double> returnPos = new Dictionary<string, double>() {
                                { "x", p0["scrX"] + (p1["scrX"] - p0["scrX"]) *perX},
                                { "y", p0["scrY"] + (p1["scrY"] - p0["scrY"]) *perY},
                                };
            return returnPos;
        }

        public Dictionary<string, double> trueAdjuster(double lat, double lng)
        {
            //todo: switch back to client bounds

            double x = ClientWidth / 360 * (180 + lng);
            double y = ClientHeight / 180 * (90 - lat);
            Dictionary<string, double> returnPos = new Dictionary<string, double>() { { "x", x }, { "y", y } };
            return returnPos;
        }

        public Dictionary<string, double> thirdTry(double lat, double lng)
        {

            double x = ClientWidth * floorCeil(lat, baseLat);
            double y = ClientHeight * floorCeil(lng, baseLng);
            Dictionary<string, double> returnPos = new Dictionary<string, double>() { { "x", x }, { "y", y } };
            return returnPos;
        }

        public double floorCeil(double measure, int baseMeasure){
            double retVal = 0 ;
            if (measure > baseMeasure)
                retVal+=  measure-baseMeasure;
            if (baseMeasure > measure)
                retVal -= baseMeasure - measure;
            return retVal;
     }
        public List<VertexPositionColorTexture> logCoords(IReadOnlyCollection<IPosition> coords)
        {
            List<VertexPositionColorTexture> vertices = new List<VertexPositionColorTexture>();
            foreach (IPosition coord in coords)
            {
                vertices.Add(logCoord(coord));
            }
            return vertices;
        }

        public VertexPositionColorTexture logCoord(IPosition coord)
        {

            VertexPositionColorTexture tempVPT = new VertexPositionColorTexture();
            Dictionary<string, double> pos = new Dictionary<string, double>();
            double altitude = coord.Altitude ?? 0;
            pos = thirdTry(coord.Latitude, coord.Longitude);
            tempVPT.Position = new Vector3((float)pos["x"], (float)pos["y"], (float)altitude);
            tempVPT.TextureCoordinate = new Vector2(0, 0);
            tempVPT.Color = Color.FloralWhite;
            return tempVPT;
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
            int count = 0;
            int countFailed = 0;
            foreach (string fileName in Directory.GetFiles(directoryName)) 
            {
                
                try
                {
                    mapList.Add(loadMapDataFile(fileName) );
                    Console.WriteLine("Successfully Loaded " + fileName);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to load "+ fileName);
                    countFailed++;
                }
                count++;
                Console.WriteLine($"Completed file {count} of {Directory.GetFiles(directoryName).Length}. {countFailed} of {count} files failed to load.");
            }

            return mapList;
        }
        protected  void LoadContent()
        {
        }

        public  void Update(GameTime gameTime)
        {

           
        }

        public  void Draw(SpriteBatch _spriteBatch, GameTime gameTime)
        {

            //GameStateController.Draw(_spriteBatch, gameTime);
            /*foreach (EffectPass effectPass in basicEffect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                short n = (short)vert.Count;

                ind = new short[n + 1]; // the +1 is to close the shape
                for (short q = 0; q < n; q++)
                    ind[q] = q;
                ind[n] = 0;

                _graphics.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                   PrimitiveType.LineStrip, vert.ToArray(), 0, vert.Count, ind, 0, ind.Length);

            }*/
            //shape.Draw(basicEffect);

            foreach(List<VertexPositionColorTexture> vertList in allShapes)
            {
                for (int i = vertList.Count - 1; i > 0; i--)
                {

                    float x1 = vertList[i].Position.X;
                    float x2 = vertList[i - 1].Position.X;
                    float y1 = vertList[i].Position.Y;
                    float y2 = vertList[i - 1].Position.Y;

                    _spriteBatch.DrawLine(new Vector2(scaleToX(x1), scaleToY(y1)), new Vector2(scaleToX(x2), scaleToY(y2)), Color.SeaGreen);
                    /*ThreadWork.x1 = x1;
                    ThreadWork.x2 = x2;
                    ThreadWork.y1 = y1;
                    ThreadWork.y2 = y2;

                    Thread thread1 = new Thread(ThreadWork.Log);
                    thread1.Start();*/
                }
            }
            

        }
        public float scaleToY(float y)
        {
           // y =   y/1074;
            return y* -1;

        }
        public float scaleToX(float x)
        {
          //  x = x/ 1920;
            return x;
        }
        public class ThreadWork
        {
            public static float x1, x2, y1, y2;
            public static void Log()
            {
                Console.WriteLine("Drawing line from " + x1 + "," + y1 + " to " + x2 + "," + y2);
            }
        }

       

    }
}
