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
        private List<int> currentAnimationFrames;
        private Dictionary<string, List<int>> animations;
        private int currentAnimationFramesIndex = 0;


        /// <summary>
        /// Change a colour using a vector4 R G B A
        /// </summary>
        public Vector4 Colour { get => colour.ToVector4(); set { colour =  new Color(value); } }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="spriteWidth">the width of the sprite on the sprite sheet</param>
        /// <param name="spriteHeight">the height of the sprite on the sprite sheet</param>
        public Sprite(int spriteWidth = 64, int spriteHeight = 64)
        {
            
            //setup the default animation 
            animations = new Dictionary<string, List<int>>();
            animations.Add("default", new List<int> { 1 });
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

        /// <summary>
        /// 
        /// </summary>
        public void SetToCharacterAnimation()
        {

            animations.Add("cast_up",      new List<int> { 0, 1, 2, 3, 4, 5, 6 });
            animations.Add("cast_left",    new List<int> { 13, 14, 15, 16, 17, 18, 19 });
            animations.Add("cast_down",    new List<int> { 26, 27, 28, 29, 30, 31, 32 });
            animations.Add("cast_right",   new List<int> { 39, 40, 41, 42, 43, 44, 45 });
            animations.Add("thrust_up",    new List<int> { 52, 53, 54, 55, 56, 57, 58, 59 });
            animations.Add("thrust_left",  new List<int> { 65, 66, 67, 68, 69, 70, 71, 72 });
            animations.Add("thrust_down",  new List<int> { 78, 79, 80, 81, 82, 83, 84, 85 });
            animations.Add("thrust_right", new List<int> { 91, 92, 93, 94, 95, 96, 97, 98 });
            animations.Add(Character.state.walk + "" + Character.direction.up, new List<int> { 105, 106, 107, 108, 109, 110, 111, 112 }); 
            animations.Add(Character.state.stop + "" + Character.direction.up, new List<int> { 104 });
            animations.Add(Character.state.walk + "" + Character.direction.left, new List<int> { 118, 119, 120, 121, 122, 123, 124, 125 }); 
            animations.Add(Character.state.stop + "" + Character.direction.left, new List<int> { 117 });
            animations.Add(Character.state.walk + "" + Character.direction.down, new List<int> { 131, 132, 133, 134, 135, 136, 137, 138 }); 
            animations.Add(Character.state.stop + "" + Character.direction.down, new List<int> { 130 });
            animations.Add(Character.state.walk + "" + Character.direction.right, new List<int> { 144, 145, 146, 147, 148, 149, 150, 151 });
            animations.Add(Character.state.stop + "" + Character.direction.right, new List<int> { 143 });

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
