using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace TileWorld_Mono

{
    public abstract class GameObject
    {
        protected Vector2 position = new Vector2(0, 0);
        protected int height;
        protected int width;

        public Vector2 Position { get { return position; } set { position = value; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public Rectangle BoundingBox { get { return new Rectangle((int)position.X, (int)position.Y, width, height); } }


        public GameObject(Vector2 position, int width, int height)
        {
            this.position = position;
            this.width = width;
            this.height = height;

        }

        public Rectangle CollisionWith(GameObject gameObject)
        {
            Rectangle intersects = Rectangle.Intersect(BoundingBox, gameObject.BoundingBox);
            return intersects;
        }

        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
