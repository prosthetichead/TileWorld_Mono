﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;


namespace TileWorld_Mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
     class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameStateManager gameStateManager;

        int screenResWidth = 1280; //3840; //1920;  //1280 
        int screenResHeight = 720; //2160; //1080; //720
        RenderTarget2D mainRenderTarget;

        public static Camera camera;
        public static Game Instance; //Static Refrance to This Game Instance (fuck you im lazy and I needed access to shit)

        //DEBUG MODE 
        public static bool debugMode = true;
        public static DebugConsole debugConsole;


        public Game()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.PreferredBackBufferWidth = screenResWidth;
            graphics.PreferredBackBufferHeight = screenResHeight;
            graphics.IsFullScreen = false; 
            graphics.ApplyChanges();

            mainRenderTarget = new RenderTarget2D(GraphicsDevice, screenResWidth, screenResHeight);

            camera = new Camera(GraphicsDevice.Viewport);
            debugConsole = new DebugConsole(200);
            gameStateManager = new GameStateManager();

            IsFixedTimeStep = false;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load static recorces used thought the game
            Fonts.LoadContent(Content);
            CharacterAppearance.LoadContent(Content);



            debugConsole.LoadContent(Content);
            // TODO: use this.Content to load your game content here


            // 
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

            Inputs.Initialize();

            gameStateManager.SetContent(Content);
            gameStateManager.AddState("MainMenu", new MainMenuState(GraphicsDevice));
            gameStateManager.AddState("World", new WorldState(GraphicsDevice));
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
            camera.Update(gameTime);

            //Update the input manager
            Inputs.Update(gameTime);

            //Update the current state
            gameStateManager.Update(gameTime);

            //update debugConsole
            debugConsole.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.SetRenderTarget(mainRenderTarget);
            GraphicsDevice.Clear(Color.Aqua);

            gameStateManager.Draw(spriteBatch); //draw current state
            spriteBatch.Begin();
                debugConsole.Draw(spriteBatch);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
                var ratio = Math.Min((float)GraphicsDevice.Viewport.Width / screenResWidth, (float)GraphicsDevice.Viewport.Height / screenResHeight);


            //float height = (screenResHeight / (float)screenResWidth) * GraphicsDevice.Viewport.Width;   
            int renderTargetWidth = (int)(screenResWidth * ratio);
            int renderTargetHeight = (int)(screenResHeight * ratio);
            int renderTargetPosX = (GraphicsDevice.Viewport.Width-renderTargetWidth) /2;
            int renderTargetPosY = (GraphicsDevice.Viewport.Height-renderTargetHeight) /2;
            spriteBatch.Draw(mainRenderTarget, new Rectangle(renderTargetPosX, renderTargetPosY, renderTargetWidth, renderTargetHeight), Color.White);      
            spriteBatch.End();

        }


    }
}
