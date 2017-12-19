using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TileWorld_Mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //Camara camara;

        //int ChunkSizeWidth = 64; //size of chunks in tiles
        //int ChunkSizeHeight = 64; //size of chunks in tiles
        //int TileSizeWidth = 32; //size of each tile in pixels
        //int TileSizeHeight = 32; //size of each tile in pixels

        int screenResWidth = 1920;
        int screenResHeight = 1080;
        
        //DEBUG MODE 
        public static bool debugMode = false;



        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = screenResWidth;
            graphics.PreferredBackBufferHeight = screenResHeight;
            graphics.IsFullScreen = false; 
            graphics.ApplyChanges();


            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() 
        {
            base.Initialize();
            
            // TODO:: change later to save last position
            //Vector2 playerStartPos = new Vector2(1, 1); //replace later with a player objects


            //

            //camara = new Camara( new Vector2(1,1)  );
            //world = new World(Content, "TheWorld", ChunkSizeWidth, ChunkSizeHeight, TileSizeWidth, TileSizeHeight, playerStartPos, camara.Position);
            Inputs.InInitialize();
            GameStateManager.Instance.SetContent(Content);
            GameStateManager.Instance.AddState("World", new WorldState(GraphicsDevice));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Update the input manager
            Inputs.Update(); 
            //Update the current state
            GameStateManager.Instance.Update(gameTime);


            //Camara.Location.X = (int)(100) - ;
            //Camara.Location.Y = (int)(44) - GraphicsDevice.Viewport.Height / 2;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GameStateManager.Instance.Draw(spriteBatch);
        }
    }
}
