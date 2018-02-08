using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;

namespace TileWorld_Mono
{
    public class Sprite
    {
        private int spriteWidth;
        private int spriteHeight;
        private Vector2 position = new Vector2(100, 100);
        private Vector2 origin = Vector2.Zero;
        private float rotation = 0.0f;
        private float depth = 0.5f;
        private Vector2 scale = Vector2.One;
        private Color colour = Color.White;

        private bool visible = true;

        private TileSet tileSet; //use the tileset class to load a sprite sheet

        private bool paused = false;
        private float timeElapsed;
        private float timePerFrame = 100;

        private string currentAnimationName = "default";
        private int[] currentAnimationFrames;
        private Dictionary<string, int[]> animations;
        private int currentAnimationFramesIndex = 0;


        /// <summary>
        /// Change a colour using a vector4 R G B A
        /// 0 white - 1 black
        /// </summary>
        public Vector4 Colour { get => colour.ToVector4(); set { colour = new Color(value); } }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="spriteWidth">the width of the sprite on the sprite sheet</param>
        /// <param name="spriteHeight">the height of the sprite on the sprite sheet</param>
        public Sprite(int spriteWidth = 64, int spriteHeight = 64)
        {

            //setup the default animation 
            animations = new Dictionary<string, int[]>();
            animations.Add("default", new int[] { 1 });
            animations.TryGetValue("default", out currentAnimationFrames);

            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
        }

        /// <summary>
        /// Parameter constructor
        /// </summary>
        /// <param name="position">Position of the sprite</param>
        /// <param name="origin">Origin of the sprite</param>
        /// <param name="rotation">Rotation of the sprite</param>
        /// <param name="scale">Scale of the sprite</param>
        /// <param name="depth">Depth of the sprite</param>
        public Sprite(Vector2 position, Color colour, Vector2 origin, float rotation, Vector2 scale, float depth, int spriteWidth = 32, int spriteHeight = 32)
        {
            this.position = position;
            this.colour = colour;
            this.origin = origin;
            this.rotation = rotation;
            this.scale = scale;
            this.depth = depth;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
        }

        public void SetLayer(float layer)
        {
            tileSet.Layer = layer;
        }

        public void LoadContent(ContentManager content, String spriteSheetName)
        {
            tileSet = new TileSet(content, spriteWidth, spriteHeight, spriteSheetName);
            tileSet.Layer = 2;
        }
        public void LoadContent(Texture2D texture)
        {
            tileSet = new TileSet(texture, spriteWidth, spriteHeight);
            tileSet.Layer = 2;
        }
        public void LoadContent(TileSet tileSet)
        {
            this.tileSet = tileSet;
            spriteWidth = tileSet.TileWidth;
            spriteHeight = tileSet.TileHeight;
            tileSet.Layer = 2;
        }



        public void AddAnimation(string key, int[] frames)
        {
            animations.Add(key, frames);
        }

        public void SetAnimation(string actionName)
        {
            if (currentAnimationName != actionName)
            {
                currentAnimationName = actionName;
                bool success = animations.TryGetValue(actionName, out currentAnimationFrames);
                currentAnimationFramesIndex = 0;
            }
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="position">X and Y</param>
        public void Update(GameTime gameTime, Vector2 position )
        {
            this.position = position;
            if (!paused)
            {
                timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                if(timeElapsed >= timePerFrame)
                {
                    currentAnimationFramesIndex++;
                    if (currentAnimationFramesIndex >= currentAnimationFrames.Count())
                        currentAnimationFramesIndex = 0;
                    timeElapsed = 0;
                }
            }
        }

        /// <summary>
        /// Draws the sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                tileSet.Draw(spriteBatch, position, origin, currentAnimationFrames[currentAnimationFramesIndex], colour);
                //spriteBatch.Draw(texture, position, rectangle, colour, rotation, origin, scale, spriteEffects, depth);
            }
        }
        


    }
}
