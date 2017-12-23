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


    public class Character : GameObject
    {
        private class AppearanceSprite
        {
            public AppearanceKey appearanceKey;
            public Appearance appearance;
            public Sprite sprite;

            public AppearanceSprite(AppearanceKey appearanceKey)
            {
                this.appearanceKey = appearanceKey;
                appearance = Appearances.GetAppearance(appearanceKey);
                if (appearance.HasTexture)
                {
                    sprite = new Sprite(appearance.Width, appearance.Height);
                    sprite.LoadContent(appearance.Texture);
                    sprite.SetToCharacterAnimation();
                }
            }
        }

        public enum state { stop, walk_left, walk_right, walk_up }
        public enum sex { male, female };

        private sex gender;
        private float hitPoints;

        private List<AppearanceSprite> appearanceSprites;
        private List<AppearanceKey> appearanceKeys;

        //protected List<Sprite> appearanceSprites = new List<Sprite>(); 


        public Character(Vector2 position, int width, int height) :base(position, width, height)
        {
            
        }

        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager content)
        {
            if(appearanceKeys == null)
                appearanceKeys = Appearances.RandomApperance(sex.male);

            appearanceSprites = new List<AppearanceSprite>();
            foreach(var appearanceKey in appearanceKeys)
            {

                appearanceSprites.Add(new AppearanceSprite(appearanceKey) );
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var appearanceSprite in appearanceSprites.Where(i=>i.sprite != null))
            {
                appearanceSprite.sprite.Update(gameTime, position);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(var appearanceSprite in appearanceSprites.Where(i => i.sprite != null))
            {
                appearanceSprite.sprite.Draw(spriteBatch);
            }
        }
        

 

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }


    }
}
