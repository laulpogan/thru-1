using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GeoJSON;
using Newtonsoft.Json;
using GeoJSON.Net.Geometry;
using GeoJSON.Net.Feature;

namespace Thru
{
	public class PolygonShape
    {
        // A Polygon object that you will be able to draw.
        // Animations are being implemented as we speak.
        // <param name="graphicsDevice">The graphicsdevice from a Game object</param>
        // <param name="vertices">The vertices in a clockwise order</param>
        public GraphicsDevice graphicsDevice;
        public VertexPositionColorTexture[] vertices, triangulatedVertices;
        public bool triangulated;
        public int[] indexes;
        Vector3 centerPoint;
        public PolygonShape(GraphicsDevice graphicsDevice, VertexPositionColorTexture[] vertices)
        {
            this.graphicsDevice = graphicsDevice;
            this.vertices = vertices;
            this.triangulated = false;

            triangulatedVertices = new VertexPositionColorTexture[vertices.Length * 3];
            indexes = new int[vertices.Length];
        }

        // Triangulate the set of VertexPositionTextures so it will be drawn correcrly        
        // <returns>The triangulated vertices array</returns>}
        public VertexPositionColorTexture[] Triangulate()
        {
            calculateCenterPoint();
            {
                setupIndexes();
                for (int i = 0; i < indexes.Length; i++)
                {
                    setupDrawableTriangle(indexes[i]);
                }

                triangulated = true;
                return triangulatedVertices;
            }
        }

            // Calculate the center point needed for triangulation.
            // The polygon will be irregular, so this isn't the actual center of the polygon
            // but it will do for now, as we only need an extra point to make the triangles with</summary>
            public void calculateCenterPoint()
            {
                float xCount = 0, yCount = 0;

                foreach (VertexPositionColorTexture vertice in vertices)
                {
                    xCount += vertice.Position.X;
                    yCount += vertice.Position.Y;
                }

                centerPoint = new Vector3(xCount / vertices.Length, yCount / vertices.Length, 0);
            }

            public void setupIndexes()
            {
                for (int i = 1; i < triangulatedVertices.Length; i = i + 3)
                {
                    indexes[i / 3] = i - 1;
                }
            }

           public void setupDrawableTriangle(int index)
            {
                triangulatedVertices[index] = vertices[index / 3]; //No DividedByZeroException?...
                if (index / 3 != vertices.Length - 1)
                    triangulatedVertices[index + 1] = vertices[(index / 3) + 1];
                else
                    triangulatedVertices[index + 1] = vertices[0];
                triangulatedVertices[index + 2].Position = centerPoint;
            }

            // Draw the polygon. If you haven't called Triangulate yet, I wil do it for you.
            // <param name="effect">The BasicEffect needed for drawing</param>
            public void Draw(BasicEffect effect)
            {
                try
                {
                    if (!triangulated)
                        Triangulate();

                    draw(effect);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            private void draw(BasicEffect effect)
            {
                effect.CurrentTechnique.Passes[0].Apply();
                graphicsDevice.DrawUserPrimitives<VertexPositionColorTexture>(
                    PrimitiveType.TriangleList, triangulatedVertices, 0, vertices.Length);
            }
    }
}
