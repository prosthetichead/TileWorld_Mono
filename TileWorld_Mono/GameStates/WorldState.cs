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
        Player player;
        Character character;
        
        public WorldState(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            
            world = new World("woopwoopwoopwoop");
            player = new Player(world);  //TODO: load char sheet 

            //character = new Character(new Vector2(50,50));
            
        }

        public override void Initialize()
        {
            Game.camera.FollowGameObject(player.Character); //follow the player around
        }

        public override void LoadContent(ContentManager content)
        {
            //load all appearance textures
            CharacterAppearance.LoadContent(content);

           
            world.LoadContent(content);
            player.LoadContent(content);
            
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            
            world.Update(gameTime);

            //character.ChangeState(Character.state.walk, Character.direction.left);
            //character.Update(gameTime);

            player.Update(gameTime);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Game.camera.TransformMatrix);
                world.Draw(spriteBatch);
                player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
