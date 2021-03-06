﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileWorld_Mono
{
    class Camera
    {
        private float zoom = 1f;
        private Vector2 position;
        private float rotationZ = 0;
        //private Viewport view;
        private Rectangle bounds;
        private GameObject follow;

        public Vector2 Position { get { return position; } }

        public Matrix TransformMatrix
        {
            get
            {
                return
                    Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                    Matrix.CreateRotationZ(rotationZ) *
                    Matrix.CreateScale(zoom) *
                    Matrix.CreateTranslation(new Vector3(bounds.Width * 0.5f, bounds.Height * 0.5f, 0));
            }
        }

        public Rectangle Bounds { get { return bounds; } }

        public Rectangle VisibleArea
        {
            get
            {
                var inverseViewMatrix = Matrix.Invert(TransformMatrix);

                var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
                var tr = Vector2.Transform(new Vector2(bounds.X, 0), inverseViewMatrix);
                var bl = Vector2.Transform(new Vector2(0, bounds.Y), inverseViewMatrix);
                var br = Vector2.Transform(new Vector2(bounds.Width, bounds.Height), inverseViewMatrix);

                var min = new Vector2(
                    MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                    MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
                var max = new Vector2(
                    MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                    MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
                return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));

            }
        }

        public Camera(Viewport viewport)
        {

            bounds = viewport.Bounds;
        }

        public void FollowGameObject(GameObject follow)
        {
            this.follow = follow;
        }
        
        public Vector2 GetScreenPosition( Vector2 worldPosition)
        {
            var screenPos = Vector2.Transform(worldPosition, TransformMatrix);

            return screenPos;
        }

        public void Update(GameTime gameTime)
        {
            if (follow != null) //Do we have something to follow?
            {
                position = new Vector2(follow.Position.X + (follow.Width / 2), follow.Position.Y + (follow.Height / 2));
                
            }
        }

    }
}