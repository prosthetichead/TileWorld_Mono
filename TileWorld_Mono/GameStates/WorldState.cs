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
        Camara camara;
        Character player;

        public WorldState(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            world = new World("worldname", 32, 32, 32, 32);
            camara = new Camara( new Vector2(0, 0) );
            player = new Character(new Vector2(100,100), 64, 64);
        }

        public override void Initialize()
        {
            camara.zoom = 1; 

        }

        public override void LoadContent(ContentManager content)
        {
            //load all appearance textures
            Appearances.LoadContent(content);

           
            world.LoadContent(content);
            player.LoadContent(content);
            
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Inputs.IsActionPressed(Inputs.Action.MoveDown))
                camara.position.Y += 2.5f;
            if (Inputs.IsActionPressed(Inputs.Action.MoveUp))
                camara.position.Y -= 2.5f;
            if (Inputs.IsActionPressed(Inputs.Action.MoveLeft))
                camara.position.X -= 2.5f;
            if (Inputs.IsActionPressed(Inputs.Action.MoveRight))
                camara.position.X += 2.5f;


            player.Update(gameTime);
            world.Update(gameTime, camara.position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, camara.Get_transformation(graphicsDevice));
                world.Draw(spriteBatch);
                player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
