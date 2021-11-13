using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using MSDASC;
using Newtonsoft.Json;

namespace Thru
{
    public class MapDataHandler
    {
        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        public class Properties
        {
            [JsonProperty(PropertyName = "ele")]
            public float ele;
            [JsonProperty(PropertyName = "name")]
            public string name;
            [JsonProperty(PropertyName = "desc")]
            public string description;
            [JsonProperty(PropertyName = "sym")]
            public string  sym;
        }
        List<VertexPositionColorTexture> vert;
        List<List<VertexPositionColorTexture>> allShapes;
        List<Feature> features;
        TrailMap gameMap;
        public ContentManager Content;
        int ClientWidth, ClientHeight;
        Dictionary<string, double> p0;
        Dictionary<string, double> p1;
        int baseLat = 33, baseLng = -116;
        double radius = 6371;
        Texture2D standardBackground;
        public MapDataHandler(int clientWidth, int clientHeight, IServiceProvider services)
        {

            Content = new ContentManager(services, "Content");

            ClientWidth = clientWidth;
            ClientHeight = clientHeight;
            standardBackground = Content.Load<Texture2D>("Backgrounds/southern_terminus");
            List<FeatureCollection> mapDataTotal = new List<FeatureCollection>();

// TODO config for map source data
            const string mapDataPath = "Content\\DataLists\\pct_map";
            Console.WriteLine("Loading maps data from: " + mapDataPath);
            foreach (var file in Directory.GetFiles(mapDataPath, "*.geojson"))
            {
                var data = loadMapDataFile(file);
                if (data.Features.Count > 0)
                {
                    mapDataTotal.Add(data);
                }
            }
            Console.WriteLine("Maps loaded");
            vert = new List<VertexPositionColorTexture>();
            allShapes = new List<List<VertexPositionColorTexture>>();
            gameMap = new TrailMap(new ArrayList(), new ArrayList(), "Game Map");

            Location newLoc = new Location(null, null, null, null, new Vector3(0, 0,0));
            Location oldLoc;
            Trail tempEdge = new Trail(null, null,0,  "", null);
            float distance;
            foreach (FeatureCollection mapDataIndividual in mapDataTotal)
                {
                    Console.WriteLine("vertices list size: " + vert.Count);
                    features = mapDataIndividual.Features;
                    //features.RemoveAll(item => item == null);
                    var shapeList = new List<PolygonShape>();
                    List<VertexPositionColorTexture> tempVerts = new List<VertexPositionColorTexture>();
                    foreach (Feature feature in features ?? Enumerable.Empty<Feature>())
                    {
                        //only waypoints have names in geoJSON
                        if (feature.Properties.ContainsKey("name") && feature.Properties.ContainsKey("sym"))
                        {

                        string symbol = feature.Properties["sym"].ToString();
                            if (String.Equals(symbol, "Campground") || String.Equals(symbol, "Flag, Blue") || String.Equals(symbol, "Post Office"))
                            {
                                oldLoc = newLoc;
                                newLoc = geojsonToLocation(feature);
                                if (oldLoc.Trails != null)
                                {
                                distance = Vector3.Distance(oldLoc.Coords, newLoc.Coords);

                                tempEdge = new Trail(oldLoc, newLoc, distance, "test", standardBackground);
                                    oldLoc.Trails.Add(tempEdge);
                                    newLoc.Trails.Add(tempEdge);
                                    gameMap.Trails.Add(tempEdge);
                                }
                                gameMap.Locations.Add(newLoc);
                            }

                        }
                        geoTypeParser(feature.Geometry);
                    }
                }
        }

        public TrailMap getGameMap()
        {
            return gameMap;
        }

        public Vector3 geoTypeParser(IGeometryObject geometry)
        {
            
            switch (geometry.Type)
            {
                case GeoJSONObjectType.LineString:
                    LineString lineString = (LineString)geometry;
                    var q = logCoords(lineString.Coordinates);
                    vert = (List<VertexPositionColorTexture>)vert.Concat<VertexPositionColorTexture>(q).ToList();
                    allShapes.Add(q);
                    break;
                case GeoJSONObjectType.Polygon:
                    Polygon polygon = (Polygon)geometry;

                    foreach (LineString lines in polygon.Coordinates)
                    {
                        var tempLine = logCoords(lines.Coordinates);
                        vert = (List<VertexPositionColorTexture>)vert.Concat<VertexPositionColorTexture>(tempLine).ToList();
                        allShapes.Add(tempLine);
                    }
                    break;
                case GeoJSONObjectType.Point:
                    GeoJSON.Net.Geometry.Point point = (GeoJSON.Net.Geometry.Point)geometry;
                    vert.Add(logCoord(point.Coordinates));
                    break;
                case GeoJSONObjectType.MultiLineString:
                    MultiLineString multiLineString = (MultiLineString)geometry;
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
                    MultiPoint multiPoint = (MultiPoint)geometry;
                    foreach (GeoJSON.Net.Geometry.Point tempPoint in multiPoint.Coordinates)
                        vert.Add(logCoord(tempPoint.Coordinates));
                    break;
                case GeoJSONObjectType.MultiPolygon:
                    Console.WriteLine("FOUND A " + GeoJSONObjectType.MultiPolygon);
                    break;
                default:
                    break;
            }

            Vector3 returnVec = new Vector3(0,0,0);
            returnVec.X = vert[vert.Count-1].Position.X;
            returnVec.Y = vert[vert.Count-1].Position.Y * -1;
            
            return returnVec;
        }
        public Location geojsonToLocation(Feature feature)
        {
            string symbol = feature.Properties["sym"].ToString();

           
            var geometry = geoTypeParser(feature.Geometry);
            Console.WriteLine("Creating Location: " + feature.Properties["name"].ToString() + " at " + geometry);
            Location location = new Location(feature.Properties["name"].ToString(), feature.Properties["desc"].ToString(), new ArrayList(), Content.Load<Texture2D>("MapAssets/buttonSheet"), geometry);
            if (String.Equals(symbol, "Flag, Blue") || String.Equals(symbol, "Post Office"))
            {
                location.Tags[0] = Tags.Town;
            }   else
            {
                location.Tags[0] = Tags.Desert;
            }


            return location;
        }

        public Dictionary<string, double> latlngToGlobalXY(double lat, double lng)
        {
            double x = radius * lng * Math.Cos((p0["lat"] + p1["lat"]) / 2);
            double y = radius * lat;
            Dictionary<string, double> returnPos = new Dictionary<string, double>() { { "x", x }, { "y", y } };
            return returnPos;
        }

        public Vector3 coordConvert(Vector3 coords)
        {
           float lat = coords.X;
            float lng = coords.Y;
            float x = ClientWidth * floorCeil(lat, baseLat);
            float y = ClientHeight * floorCeil(lng, baseLng);
            return new Vector3(x,y,coords.Z);
        }

        public float floorCeil(float measure, int baseMeasure){
            float retVal = 0 ;
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
            Vector3 pos = new Vector3(0,0,0);
            double altitude = coord.Altitude ?? 0;
            pos = coordConvert(new Vector3((float)coord.Latitude,(float) coord.Longitude, (float)(coord.Altitude ?? 0)));
            tempVPT.Position = new Vector3((float)pos.X, (float)pos.Y, (float)altitude);
            tempVPT.TextureCoordinate = new Vector2(0, 0);
            tempVPT.Color = Color.FloralWhite;
            return tempVPT;
        }

        public FeatureCollection loadMapDataFile(string fileName)
        {
            StringBuilder jsonString = new StringBuilder("");
            //Console.WriteLine("Reading from file " + fileName);

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {

                    jsonString.Append(s);
                }
                sr.Close();
              //  Console.WriteLine(fileName + " closed");
            }
            return JsonConvert.DeserializeObject<FeatureCollection>(jsonString.ToString());
        }

        protected void LoadContent()
        {
        }

        public List<Vector3> getTrailPoints()
        {
            List<Vector3> vectors = new List<Vector3>();
            foreach (List<VertexPositionColorTexture> vertList in allShapes)
            {
                for (int i = vertList.Count - 1; i > 0; i--)
                {
                    var newVec = vertList[i].Position;
                    newVec.Y *= -1;
                    vectors.Add(newVec);
                }
            }
            return vectors;
        }

        public TrailMap getTrailMap()
        {

            TrailMap trailMap = new TrailMap(new ArrayList(), new ArrayList(), "Trail Map"); ;
            


            Location newLoc = new Location(null, null, null, null, new Vector3(0,0, 0));
            Location oldLoc = new Location(null, null, null, null, new Vector3(0, 0, 0)) ;
            Trail tempEdge = new Trail(null, null, 0, "", null);
            foreach (Vector3 vec in getTrailPoints())
            {

                oldLoc = newLoc;
                newLoc = new Location(vec.GetHashCode().ToString(), vec.GetHashCode().ToString(), new ArrayList(), standardBackground,vec);
                if (oldLoc.Trails != null)
                {
                    float distance = Vector3.Distance(oldLoc.Coords, newLoc.Coords);
                    tempEdge = new Trail(oldLoc, newLoc, distance, "test", Content.Load<Texture2D>("Backgrounds/southern_terminus"));
                    oldLoc.Trails.Add(tempEdge);
                    newLoc.Trails.Add(tempEdge);
                    trailMap.Trails.Add(tempEdge);
                }
                trailMap.Locations.Add(newLoc);
            }
            return trailMap;
        }



public void Update(GameTime gameTime)
        {

           
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach(List<VertexPositionColorTexture> vertList in allShapes)
            {
                for (int i = vertList.Count - 1; i > 0; i--)
                {

                    float x1 = vertList[i].Position.X;
                    float x2 = vertList[i - 1].Position.X;
                    float y1 = vertList[i].Position.Y;
                    float y2 = vertList[i - 1].Position.Y;

                    _spriteBatch.DrawLine(new Vector2(scaleToX(x1), scaleToY(y1)), new Vector2(scaleToX(x2), scaleToY(y2)), Color.SeaGreen);
      
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

       

    }
}
