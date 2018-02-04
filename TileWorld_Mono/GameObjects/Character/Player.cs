using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileWorld_Mono
{
    class Player
    {
        private Character character; //GameObject
        public Character Character { get { return character; } }
        private World world;

        
        public Player(World world)
        {
            character = new Character( new Vector2() ); // Random Character TODO: Change this to load the players created/saved char
        }
        public void LoadContent(ContentManager content)
        {
            character.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            
            if (Inputs.IsActionPressed(Inputs.Action.MoveUp))
            {
                character.ChangeState(Character.state.walk, Character.direction.up);
            }
            else if (Inputs.IsActionPressed(Inputs.Action.MoveDown))
            {
                character.ChangeState(Character.state.walk, Character.direction.down);
            }
            else if(Inputs.IsActionPressed(Inputs.Action.MoveLeft))
            {
                character.ChangeState(Character.state.walk, Character.direction.left);
            }
            else if (Inputs.IsActionPressed(Inputs.Action.MoveRight))
            {
                character.ChangeState(Character.state.walk, Character.direction.right);
            }
            else //No action pressed
            {
                character.ChangeState(Character.state.stop, null);
            }

            character.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            character.Draw(spriteBatch);
        }
    }
}
