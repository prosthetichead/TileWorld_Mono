using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TileWorld_Mono
{
    static class Fonts
    {
        public static SpriteFont ArmaFive;

        static public void LoadContent(ContentManager content)
        {
            ArmaFive = content.Load<SpriteFont>(@"fonts\Font-PF Arma Five");
        }
    }
}
