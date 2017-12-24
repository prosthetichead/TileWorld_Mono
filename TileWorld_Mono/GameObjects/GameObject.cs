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
        protected Vector2 position;
        protected int height;
        protected int width;
        public Vector2 Position { get { return position; } set { position = value; } }


        public GameObject()
        {

        }
        public GameObject(Vector2 position, int width, int height)
        {
            this.position = position;
            this.width = width;
            this.height = height;

        }
        public abstract void Initialize();
        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
