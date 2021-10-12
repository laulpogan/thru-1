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
    public class Camera
    {
        public float Zoom { get; set; }
        public Vector2 Pos { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }

        private float currentMouseWheelValue, previousMouseWheelValue, zoom, previousZoom;

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = 1f;
            Pos = Vector2.Zero;
        }


        private void UpdateVisibleArea()
        {
            var inverseViewMatrix = Matrix.Invert(Transform);

            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Pos.X, -Pos.Y, 0)) * 
                    Matrix.CreateTranslation(new Vector3(-Mouse.GetState().X - Bounds.Width / 2, -Mouse.GetState().Y - Bounds.Height / 2, 0)) * //Mouse Translation Matrix
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {

            if (Pos + movePosition != Pos)
            {
                Console.WriteLine("Old Camera Position: " +  Pos);
                Pos += movePosition;
                Console.WriteLine("New Camera Position: " + Pos);

            }
        }

        public void AdjustZoom(float zoomAmount)
        {
            Zoom += zoomAmount;
            if (Zoom < .1f)
            {
                Zoom = .1f;
            }
            if (Zoom > 10f)
            {
                Zoom = 10f;
            }
        }

        public void UpdateCamera(Viewport bounds)
        {
            Bounds = bounds.Bounds;
            UpdateMatrix();

            Vector2 cameraMovement = Vector2.Zero;
            int moveSpeed;

            if (Zoom > .8f)
            {
                moveSpeed = 45;
            }
            else if (Zoom < .8f && Zoom >= .6f)
            {
                moveSpeed = 60;
            }
            else if (Zoom < .6f && Zoom > .35f)
            {
                moveSpeed = 75;
            }
            else if (Zoom <= .35f)
            {
                moveSpeed = 100;
            }
            else
            {
                moveSpeed = 25;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                cameraMovement.Y = -moveSpeed;
                Console.WriteLine("Up Arrow Pressed");

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                cameraMovement.Y = moveSpeed;
                Console.WriteLine("Down Arrow Pressed");

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                cameraMovement.X = -moveSpeed;
                Console.WriteLine("Left Arrow Pressed");

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                cameraMovement.X = moveSpeed;
                Console.WriteLine("Right Arrow Pressed");

            }

            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue)
            {
                AdjustZoom(1f);
                Console.WriteLine("Camera Movespeed adjusted: " + moveSpeed);
            }

            if (currentMouseWheelValue < previousMouseWheelValue)
            {
                AdjustZoom(-.5f);
                Console.WriteLine("Camera Movespeed adjusted: " + moveSpeed);
            }

            previousZoom = zoom;
            zoom = Zoom;
            if (previousZoom != zoom)
            {
                Console.WriteLine("Camera Zoom Adjusted from "+ previousZoom + " to "+ zoom );

            }

            MoveCamera(cameraMovement);
        }
    }
}

