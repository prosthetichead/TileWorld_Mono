using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace TileWorld_Mono
{
    class Player
    {
        Character character;

        public Player()
        {
            character = new Character();
        }
        public void LoadContent()
        {
        }
        public void LoadContent(ContentManager content)
        {
            
        }
    }
}
