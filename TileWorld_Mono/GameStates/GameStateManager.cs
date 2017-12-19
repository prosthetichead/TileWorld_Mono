﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TileWorld_Mono
{
    public class GameStateManager
    {
        private ContentManager content;
        // List for the screens     
        //private List<GameState> stateStack = new List<GameState>();
        private Dictionary<string, GameState> gameStates = new Dictionary<string, GameState>();

        private string activeStateKey;
        public string ActiveStateKey { get { return ActiveStateKey; } set { activeStateKey = value; } }

        // Instance of the game state manager     
        private static GameStateManager _instance;

        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }
        public void SetContent(ContentManager content) => this.content = content;

        /// <summary>
        /// Add a new state to the manager
        /// screens Initialize and LoadContent functions are also called
        /// </summary>
        /// <param name="state">The Screen State</param>
        /// <param name="stateKey">The Key to uniquely itentify the game state</param>
        public void AddState(string stateKey, GameState state)
        {
            // Initialize the state
            state.Initialize();
            // Call the LoadContent on the state
            if (content != null)
                state.LoadContent(content);

            // Add the state to the stack
            gameStates.Add(stateKey, state);

            //if no other state is active make this new state active
            if(activeStateKey == null) {
                activeStateKey = stateKey;
            }
        }

        /// <summary>
        /// Remove a state.
        /// </summary>
        /// <param name="stateKey">The Key to uniquely itentify the game state</param>
        public void RemoveState(string stateKey)
        {
            try
            {
                if (gameStates.TryGetValue(activeStateKey, out var gameState))
                {
                    gameState.UnloadContent();
                    gameStates.Remove(stateKey);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        
        

        /// <summary>
        /// Clears all the states from the Manager
        /// </summary>
        public void RemoveAllStates()
        {
            gameStates = new Dictionary<string, GameState>();
        }

        /// <summary>
        /// Update the current top state
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            try
            {
                if (gameStates.TryGetValue(activeStateKey, out var gameState ))
                {
                    gameState.Update(gameTime);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Render the current top State
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {

                if (gameStates.TryGetValue(activeStateKey, out var gameState))
                {

                    gameState.Draw(spriteBatch);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call all states unload content methods
        /// </summary>
        public void UnloadContent()
        {
            foreach (GameState gameState in gameStates.Values)
            {
                gameState.UnloadContent();
            }
        }   
    }
}