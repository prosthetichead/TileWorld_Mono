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
    public class CharacterSheet
    {
        public string name;
        public int maxMana;
        public int maxHitPoints;
        public int mana;
        public int hitPoints;
        public int level;
        public int xp;

        public int strength;
        public int dexterity;
        public int constitution;
        public int intelligence;
        public int wisdom;
        public int charisma;

        public Character.sex gender;

        public List<AppearanceKey> appearanceKeys;
        public Vector4 hairColour;
        public Vector4 facialColour;

        public CharacterSheet()
        {
            //New Empty Char
            Random random = new Random();
            //random gender
            gender = (Character.sex)random.Next(0, Enum.GetNames(typeof(Character.sex)).Length);
            //random apperance
            if (appearanceKeys == null)
                appearanceKeys = Appearances.RandomApperance(gender);
            //random hair colour
            if (hairColour == Vector4.Zero)
                hairColour = new Vector4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1);
            //random facial Colour
            if (facialColour == Vector4.Zero)
                facialColour = new Vector4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1);
        }
    }

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
                    sprite.SetLayer(2 + ((float)(appearanceKey.slot*1) / 1000)); //fix layering as they all share the same x,y cords so depth fucks up (hair behind body)
                }
            }
        }

        public enum state { stop, walk }
        public enum sex { female, male, skeleton };
        public enum direction { up, down, left, right }
        private List<AppearanceSprite> appearanceSprites;
        private CharacterSheet cs;
        private Vector2 directionVector2;
        private direction currentDirection = direction.up;
        private state currentState;
        
        public Character(Vector2 position ) : base(position, 64, 64)
        {
            cs = new CharacterSheet();
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            appearanceSprites = new List<AppearanceSprite>();
            foreach(var appearanceKey in cs.appearanceKeys)
            {
                appearanceSprites.Add(new AppearanceSprite(appearanceKey) );
            }

            //set hair colour
           foreach( var appearanceSprite in appearanceSprites.Where(i=>i.appearanceKey.slot == (int)Appearances.slot.hair && i.sprite !=null))
                appearanceSprite.sprite.Colour = cs.hairColour;
           // //set facial colour
            foreach (var appearanceSprite in appearanceSprites.Where(i => i.appearanceKey.slot == (int)Appearances.slot.facial && i.sprite != null))
               appearanceSprite.sprite.Colour = cs.facialColour;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var appearanceSprite in appearanceSprites.Where(i=>i.sprite != null))
            {
                appearanceSprite.sprite.SetAnimation(currentState + "" + currentDirection); //se animation 
                appearanceSprite.sprite.Update(gameTime, position);
            }
            switch (currentState)
            {
                case state.stop:
                    break;
                case state.walk:
                    position += directionVector2 * 2;
                    break;
                default:
                    break;
            }
        }

        public void ChangeState(state state)
        {
            ChangeState(state, currentDirection);
        }

        public void ChangeState(state state, direction direction)
        {
            currentState = state;
            currentDirection = direction;
            switch (currentDirection)
            {
                case direction.up:
                    directionVector2 = Vector2.UnitY * -1;
                    break;
                case direction.down:
                    directionVector2 = Vector2.UnitY;
                    break;
                case direction.left:
                    directionVector2 = Vector2.UnitX * -1;
                    break;
                case direction.right:
                    directionVector2 = Vector2.UnitX;
                    break;
                default:
                    break;
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
