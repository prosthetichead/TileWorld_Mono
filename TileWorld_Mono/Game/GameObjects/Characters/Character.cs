﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TileWorld_Mono
{
    //public class CharacterSheet
    //{
    //    public string name;
    //    public int maxMana;
    //    public int maxHitPoints;
    //    public int mana;
    //    public int hitPoints;
    //    public int level;
    //    public int xp;

    //    public int strength;
    //    public int dexterity;
    //    public int constitution;
    //    public int intelligence;
    //    public int wisdom;
    //    public int charisma;

    //    public Character.sex gender;

    //    public List<AppearanceKey> appearanceKeys;
    //    public Vector4 hairColour;
    //    public Vector4 facialColour;

    //    public CharacterSheet()
    //    {
    //        //New Empty Char
    //        Random random = new Random();
    //        //random gender
    //        gender = (Character.sex)random.Next(0, Enum.GetNames(typeof(Character.sex)).Length);
    //        //random apperance
    //        if (appearanceKeys == null)
    //            appearanceKeys = Appearances.RandomApperance(gender);
    //        //random hair colour
    //        if (hairColour == Vector4.Zero)
    //            hairColour = new Vector4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1);
    //        //random facial Colour
    //        if (facialColour == Vector4.Zero)
    //            facialColour = new Vector4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1);
    //    }
    //}

    public class Character : GameObject
    {
        public enum state { stop, walk, thrust, cast }
        public enum direction { up, down, left, right }
        public enum sex { female, male, skeleton }
        public enum slot { body, hair, facial }

        private String[] apperanceKeys; //key for each slot if null then slot has no aperance.
        private Sprite[] appearanceSprites;

        private sex characterSex;
        private Vector4 facialHairColour;
        private Vector4 hairColour;

        private Vector2 directionVector2;
        private direction currentDirection;
        private state currentState;

        
        public Character(Vector2 position) : base(position, 64, 64)
        {
            apperanceKeys = new String[Enum.GetValues(typeof(slot)).Length];
            appearanceSprites = new Sprite[apperanceKeys.Length];
            ChangeState(state.stop, direction.down);

            characterSex = sex.skeleton;

        }

        /// <summary>
        /// Randomize just the Body, Hair, and Facial Slots
        /// </summary>
        public void RandomApperance()
        {
            apperanceKeys[(int)slot.body] = CharacterAppearance.RandomApperanceSlotKey(characterSex, slot.body);
            apperanceKeys[(int)slot.facial] = CharacterAppearance.RandomApperanceSlotKey(characterSex, slot.facial);
            apperanceKeys[(int)slot.hair] = CharacterAppearance.RandomApperanceSlotKey(characterSex, slot.hair);

            RefreshApperance();
        }


        public override void Initialize()
        {

        }

        /// <summary>
        /// reload the apperance using the character apperanceKeys
        /// </summary>
        public void RefreshApperance()
        {
            appearanceSprites = new Sprite[apperanceKeys.Length]; //clear the whole sprite list.
            for (int i = 0; i < apperanceKeys.Length; i++) {
                appearanceSprites[i] = CharacterAppearance.GetSprite(characterSex, (slot)i, apperanceKeys[i]);
            }

            //set hair colour
            //foreach (var appearanceSprite in appearanceSprites.Where(i => i.appearanceKey.slot == (int)Appearances.slot.hair && i.sprite != null))
              //  appearanceSprite.sprite.Colour = cs.hairColour;
            // //set facial colour
            //foreach (var appearanceSprite in appearanceSprites.Where(i => i.appearanceKey.slot == (int)Appearances.slot.facial && i.sprite != null))
              //  appearanceSprite.sprite.Colour = cs.facialColour;
        }

        public override void LoadContent(ContentManager content)
        {
            //We dont need to load anything its already loaded by the static Appearances Class
            //we just need to refresh the apperance sprites using the keys
            RandomApperance();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in appearanceSprites)
            {
                if (sprite != null)
                {
                    sprite.SetAnimation(currentState + "" + currentDirection); //set animation to the animation name
                    sprite.Update(gameTime, position); // update the sprite
                }
            }
            switch (currentState) // state machine
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
        
        public void ChangeState(state? newState, direction? newDirection)
        {
            if(newState != null)
                currentState = (state)newState;
            if(newDirection != null)
                currentDirection = (direction)newDirection;

            //set the direction vector.
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
            foreach(var appearanceSprite in appearanceSprites)
            {
                if (appearanceSprite != null)
                {
                    appearanceSprite.Draw(spriteBatch);
                }
            }
        }

        
 

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }


    }
}