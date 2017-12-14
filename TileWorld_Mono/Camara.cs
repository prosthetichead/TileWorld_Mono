using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileWorld_Mono
{
    class Camara
    {

        public Vector2 position; // location of the camara (center focus point)
        public float zoom = 1f; // Camera Zoom
        private  Matrix transform; // Matrix Transform
        

        public Camara(Vector2 position)
        {
            this.position = position;
        }

        public  Matrix Get_transformation(GraphicsDevice graphicsDevice)
        {
            transform =       // Thanks to o KB o for this solution
            Matrix.CreateTranslation(new Vector3(-graphicsDevice.Viewport.Width / 2, -graphicsDevice.Viewport.Height/2, 0)) *
                                         Matrix.CreateRotationZ(0) *
                                         Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return transform;
        }


    }
}
