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
    class MainMenuState : GameState
    {

        SimpleMenu simpleMenu;

        public MainMenuState(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            simpleMenu = new SimpleMenu(new Rectangle(100, 100, 60, 70), Color.WhiteSmoke, Color.DarkSlateBlue);

        }

        public override void LoadContent(ContentManager content)
        {
            simpleMenu.LoadContent();

        }

        public override void Initialize(GameStateManager gameStateManager)
        {
            base.Initialize(gameStateManager);
            
            SimpleMenu.SimpleMenuItem newGame = new SimpleMenu.SimpleMenuItem("New Game", Fonts.ArmaFive, Color.LightSalmon);
            SimpleMenu.SimpleMenuItem loadGame = new SimpleMenu.SimpleMenuItem("Load Game", Fonts.ArmaFive, Color.LightSalmon);
            SimpleMenu.SimpleMenuItem options = new SimpleMenu.SimpleMenuItem("Options", Fonts.ArmaFive, Color.LightSalmon);

            simpleMenu.Initialize( new List<SimpleMenu.SimpleMenuItem>() { newGame, loadGame, options } );
        }

        public override void Update(GameTime gameTime)
        {
            simpleMenu.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            simpleMenu.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void UnloadContent()
        {
           
        }






    }
}
