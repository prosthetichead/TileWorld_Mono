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
    class WorldState : GameState
    {
        World world;
        RenderTarget2D mainRenderTarget;
        Camara camara;

        public WorldState(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            mainRenderTarget = new RenderTarget2D(graphicsDevice, 1920, 1080);
            world = new World("worldname", 32, 32, 32, 32);
            camara = new Camara( new Vector2(0, 0) );
        }

        public override void Initialize()
        {
            camara.zoom = 1; 
        }

        public override void LoadContent(ContentManager content)
        {
            world.LoadContent(content);
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Inputs.IsGamePadDPadDownPressed())
                camara.position.Y++;
            if (Inputs.IsGamePadDPadUpPressed())
                camara.position.Y--;
            if (Inputs.IsGamePadDPadLeftPressed())
                camara.position.X--;
            if (Inputs.IsGamePadDPadRightPressed())
                camara.position.X++;



            world.Update(gameTime, camara.position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.SetRenderTarget(mainRenderTarget);
            graphicsDevice.Clear(Color.BurlyWood);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, camara.Get_transformation(graphicsDevice));
                world.Draw(spriteBatch);
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(samplerState: SamplerState.PointWrap);
                spriteBatch.Draw(mainRenderTarget, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White );
            spriteBatch.End();
        }
    }
}
