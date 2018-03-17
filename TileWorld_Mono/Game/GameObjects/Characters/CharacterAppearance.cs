using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;


namespace TileWorld_Mono
{
    public static class CharacterAppearance
    {
        private static Dictionary<string, TileSet>[,] appearances;
        private static int numberSexes;
        private static int numberSlots;

        static CharacterAppearance()
        {
            numberSlots = Enum.GetNames(typeof(Character.slot)).Length;
            numberSexes = Enum.GetNames(typeof(Character.sex)).Length;

            appearances = new Dictionary<string, TileSet>[numberSexes, numberSlots];
            for (int h = 0; h < numberSexes; h++)
            {
                for (int i = 0; i < numberSlots; i++)
                {
                    appearances[h, i] = new Dictionary<string, TileSet>();
                }
            }
        }
        
        public static void LoadContent(ContentManager content)
        {
            int male = (int)Character.sex.male;
            int female = (int)Character.sex.female;
            int skeleton = (int)Character.sex.skeleton;
            int body = (int)Character.slot.body;
            int facial = (int)Character.slot.facial;
            int hair = (int)Character.slot.hair;
            float bodyLayer = 2.1f;
            float facialLayer = 2.2f;
            float hairLayer = 2.3f;


            //Bodys
            appearances[skeleton, body].Add("White Skeleton", new TileSet(content, 64, 64, bodyLayer, "characters/body/male/skeleton"));
            appearances[male, body].Add("Human 1", new TileSet(content, 64, 64, bodyLayer, "characters/body/male/light"));
            appearances[female, body].Add("Human 1", new TileSet(content, 64, 64, bodyLayer, "characters/body/female/light"));

            //Facial Hair
            appearances[skeleton, facial].Add("Beard 1", new TileSet(content, 64, 64, facialLayer, "characters/facial/male/beard"));
            appearances[male, facial].Add("Beard 1", new TileSet(content, 64, 64, facialLayer, "characters/facial/male/beard"));
            appearances[female, facial].Add("Beard 1", new TileSet(content, 64, 64, facialLayer, "characters/facial/male/beard"));
            
            //Hair
            appearances[skeleton, hair].Add("Messy 1", new TileSet(content, 64, 64, hairLayer,"characters/hair/male/messy1"));
            appearances[male, hair].Add("Messy 1", new TileSet(content, 64, 64, hairLayer, "characters/hair/male/messy1"));
            appearances[female, hair].Add("Messy 1", new TileSet(content, 64, 64, hairLayer, "characters/hair/female/messy1"));
        }

        public static Sprite GetSprite(Character.sex charSex, Character.slot charSlot, string key)
        {
            Sprite sprite = new Sprite();
            if (key != null)
            {
                sprite.LoadContent(appearances[(int)charSex, (int)charSlot][key]);
                SetAnimation(sprite);
            }
            return sprite;
        }
        
        public static string[] GetKeys(Character.sex charSex, Character.slot charSlot)
        {
            return appearances[(int)charSex, (int)charSlot].Keys.ToArray();
        }

        public static string RandomApperanceSlotKey(Character.sex charSex, Character.slot charSlot)
        {
            var keys = GetKeys(charSex, charSlot);
            Random random = new Random();
            return keys[random.Next(0, keys.Length-1)]; 
        }

        private static void SetAnimation(Sprite sprite)
        {
            sprite.AddAnimation("cast_up", new int[]{ 0, 1, 2, 3, 4, 5, 6 });
            sprite.AddAnimation("cast_left", new int[]{ 13, 14, 15, 16, 17, 18, 19 });
            sprite.AddAnimation("cast_down", new int[]{ 26, 27, 28, 29, 30, 31, 32 });
            sprite.AddAnimation("cast_right", new int[] { 39, 40, 41, 42, 43, 44, 45 });
            sprite.AddAnimation("thrust_up", new int[] { 52, 53, 54, 55, 56, 57, 58, 59 });
            sprite.AddAnimation("thrust_left", new int[] { 65, 66, 67, 68, 69, 70, 71, 72 });
            sprite.AddAnimation("thrust_down", new int[] { 78, 79, 80, 81, 82, 83, 84, 85 });
            sprite.AddAnimation(Character.state.thrust + ""+Character.direction.right, new int[] { 91, 92, 93, 94, 95, 96, 97, 98 });
            sprite.AddAnimation(Character.state.walk + "" + Character.direction.up, new int[] { 105, 106, 107, 108, 109, 110, 111, 112 });
            sprite.AddAnimation(Character.state.stop + "" + Character.direction.up, new int[] { 104 });
            sprite.AddAnimation(Character.state.walk + "" + Character.direction.left, new int[] { 118, 119, 120, 121, 122, 123, 124, 125 });
            sprite.AddAnimation(Character.state.stop + "" + Character.direction.left, new int[] { 117 });
            sprite.AddAnimation(Character.state.walk + "" + Character.direction.down, new int[] { 131, 132, 133, 134, 135, 136, 137, 138 });
            sprite.AddAnimation(Character.state.stop + "" + Character.direction.down, new int[] { 130 });
            sprite.AddAnimation(Character.state.walk + "" + Character.direction.right, new int[] { 144, 145, 146, 147, 148, 149, 150, 151 });
            sprite.AddAnimation(Character.state.stop + "" + Character.direction.right, new int[] { 143 });

        }
        // skeleton BODIES
        //appearances[skeleton, (int)slot.body].Add("skeleton", new Appearance("characters/body/male/skeleton", "Undead"));

        //    // MALE BODIES
        //    //appearances[male, (int)slot.body].Add("no facial", new Appearance("No Facial Hair"));
        //    appearances[male, (int)slot.body].Add("light", new Appearance("characters/body/male/light", "Male Human Light Skin Tone"));
        //    //appearances[male, (int)slot.body].Add("dark", new Appearance("characters/body/male/dark", "Male Human Dark Skin Tone"));
        //    //appearances[male, (int)slot.body].Add("dark2", new Appearance("characters/body/male/dark2", "Male Human Dark Skin Tone 2"));
        //    //appearances[male, (int)slot.body].Add("tanned", new Appearance("characters/body/male/tanned", "Male Human Tanned Skin Tone"));
        //    //appearances[male, (int)slot.body].Add("tanned2", new Appearance("characters/body/male/tanned2", "Male Human Tanned Skin Tone 2"));
        //    //appearances[male, (int)slot.body].Add("darkelf", new Appearance("characters/body/male/darkelf", "Male Dark Elf"));
        //    //appearances[male, (int)slot.body].Add("darkelf2", new Appearance("characters/body/male/darkelf2", "Male Dark Elf 2"));
        //    //appearances[male, (int)slot.body].Add("orc", new Appearance("characters/body/male/orc", "Male Orc"));
        //    //appearances[male, (int)slot.body].Add("redorc", new Appearance("characters/body/male/red_orc", "Male Red Orc"));

        //    // FEMALE BODIES
        //    appearances[female, (int)slot.body].Add("light", new Appearance("characters/body/female/light", "Female Human Light Skin Tone"));
        //    appearances[female, (int)slot.body].Add("dark", new Appearance("characters/body/female/dark", "Female Human Dark Skin Tone"));
        //    appearances[female, (int)slot.body].Add("dark2", new Appearance("characters/body/female/dark2", "Female Human Dark Skin Tone 2"));
        //    appearances[female, (int)slot.body].Add("tanned", new Appearance("characters/body/female/tanned", "Female Human Tanned Skin Tone"));
        //    appearances[female, (int)slot.body].Add("tanned2", new Appearance("characters/body/female/tanned2", "Female Human Tanned Skin Tone 2"));
        //    appearances[female, (int)slot.body].Add("darkelf", new Appearance("characters/body/female/darkelf", "Female Dark Elf"));
        //    appearances[female, (int)slot.body].Add("darkelf2", new Appearance("characters/body/female/darkelf2", "Female Dark Elf 2"));
        //    appearances[female, (int)slot.body].Add("orc", new Appearance("characters/body/female/orc", "Female Orc"));
        //    appearances[female, (int)slot.body].Add("redorc", new Appearance("characters/body/female/red_orc", "Female Red Orc"));

        //    // MALE FACIAL
        //   // appearances[male, (int)slot.facial].Add("no facial", new Appearance("No Facial Hair"));
        //    appearances[male, (int)slot.facial].Add("beard", new Appearance("characters/facial/male/beard/black", "Beard"));
        //    appearances[male, (int)slot.facial].Add("big stache", new Appearance("characters/facial/male/bigstache", "Big Stache"));
        //    appearances[male, (int)slot.facial].Add("fiveoclock", new Appearance("characters/facial/male/fiveoclock", "Five O'Clock Shadow"));
        //    appearances[male, (int)slot.facial].Add("frenchstache", new Appearance("characters/facial/male/frenchstache", "French Stache"));
        //    appearances[male, (int)slot.facial].Add("mustache", new Appearance("characters/facial/male/mustache", "Mustache"));

        //    // FEMALE FACIAL
        //    appearances[female, (int)slot.facial].Add("no facial", new Appearance("No Facial Hair"));

        //    // skeleton FACIAL
        //    appearances[skeleton, (int)slot.facial].Add("no facial", new Appearance("No Facial Hair"));

        //    // MALE HAIR
        //    appearances[male, (int)slot.hair].Add("no hair", new Appearance("No Hair"));
        //    appearances[male, (int)slot.hair].Add("bangs", new Appearance("characters/hair/male/bangs", "Bangs"));
        //    appearances[male, (int)slot.hair].Add("bangs long", new Appearance("characters/hair/male/bangslong", "Bangs Long"));
        //    appearances[male, (int)slot.hair].Add("bangs long 2", new Appearance("characters/hair/male/bangslong2", "Bangs Long 2"));
        //    appearances[male, (int)slot.hair].Add("bangs short", new Appearance("characters/hair/male/bangsshort", "Bangs Short"));
        //    appearances[male, (int)slot.hair].Add("bed head", new Appearance("characters/hair/male/bedhead", "BedHead"));
        //    appearances[male, (int)slot.hair].Add("bunches", new Appearance("characters/hair/male/bunches", "Bunches"));
        //    appearances[male, (int)slot.hair].Add("jewfro", new Appearance("characters/hair/male/jewfro", "Jewfro"));
        //    appearances[male, (int)slot.hair].Add("long", new Appearance("characters/hair/male/long", "Long"));
        //    appearances[male, (int)slot.hair].Add("long hawk", new Appearance("characters/hair/male/longhawk", "Long Hawk"));
        //    appearances[male, (int)slot.hair].Add("long knot", new Appearance("characters/hair/male/longknot", "Long Knot"));
        //    appearances[male, (int)slot.hair].Add("loose", new Appearance("characters/hair/male/loose", "Loose"));
        //    appearances[male, (int)slot.hair].Add("messy1", new Appearance("characters/hair/male/messy1", "Messy"));
        //    appearances[male, (int)slot.hair].Add("messy2", new Appearance("characters/hair/male/messy2", "Messy 2"));
        //    appearances[male, (int)slot.hair].Add("mohawk", new Appearance("characters/hair/male/mohawk", "Mohawk"));
        //    appearances[male, (int)slot.hair].Add("page", new Appearance("characters/hair/male/page", "Page"));
        //    appearances[male, (int)slot.hair].Add("page2", new Appearance("characters/hair/male/page2", "Page 2"));
        //    appearances[male, (int)slot.hair].Add("parted", new Appearance("characters/hair/male/parted", "Parted"));
        //    appearances[male, (int)slot.hair].Add("pixie", new Appearance("characters/hair/male/pixie", "Pixie"));
        //    appearances[male, (int)slot.hair].Add("plain", new Appearance("characters/hair/male/plain", "plain"));
        //    appearances[male, (int)slot.hair].Add("ponytail", new Appearance("characters/hair/male/ponytail", "Pony Tail"));
        //    appearances[male, (int)slot.hair].Add("ponytail2", new Appearance("characters/hair/male/ponytail2", "Pony Tail 2"));
        //    appearances[male, (int)slot.hair].Add("princess", new Appearance("characters/hair/male/princess", "Princess"));
        //    appearances[male, (int)slot.hair].Add("shorthawk", new Appearance("characters/hair/male/shorthawk", "Short Hawk"));
        //    appearances[male, (int)slot.hair].Add("shortknot", new Appearance("characters/hair/male/shortknot", "Short Knot"));
        //    appearances[male, (int)slot.hair].Add("shoulderr", new Appearance("characters/hair/male/shoulderr", "Shoulder"));
        //    appearances[male, (int)slot.hair].Add("swoop", new Appearance("characters/hair/male/swoop", "Swoop"));
        //    appearances[male, (int)slot.hair].Add("unkempt", new Appearance("characters/hair/male/unkempt", "unkempt"));
        //    appearances[male, (int)slot.hair].Add("xlong", new Appearance("characters/hair/male/xlong", "Extra Long"));
        //    appearances[male, (int)slot.hair].Add("xlong knot", new Appearance("characters/hair/male/xlongknot", "Extra Long knot"));
        //    // FEMALE HAIR
        //    appearances[female, (int)slot.hair].Add("bangs", new Appearance("characters/hair/female/bangs", "Bangs"));
        //    appearances[female, (int)slot.hair].Add("bangs long", new Appearance("characters/hair/female/bangslong", "Bangs Long"));
        //    appearances[female, (int)slot.hair].Add("bangs long 2", new Appearance("characters/hair/female/bangslong2", "Bangs Long 2"));
        //    appearances[female, (int)slot.hair].Add("bangs short", new Appearance("characters/hair/female/bangsshort", "Bangs Short"));
        //    appearances[female, (int)slot.hair].Add("bed head", new Appearance("characters/hair/female/bedhead", "BedHead"));
        //    appearances[female, (int)slot.hair].Add("bunches", new Appearance("characters/hair/female/bunches", "Bunches"));
        //    appearances[female, (int)slot.hair].Add("jewfro", new Appearance("characters/hair/female/jewfro", "Jewfro"));
        //    appearances[female, (int)slot.hair].Add("long", new Appearance("characters/hair/female/long", "Long"));
        //    appearances[female, (int)slot.hair].Add("long hawk", new Appearance("characters/hair/female/longhawk", "Long Hawk"));
        //    appearances[female, (int)slot.hair].Add("long knot", new Appearance("characters/hair/female/longknot", "Long Knot"));
        //    appearances[female, (int)slot.hair].Add("loose", new Appearance("characters/hair/female/loose", "Loose"));
        //    appearances[female, (int)slot.hair].Add("messy1", new Appearance("characters/hair/female/messy1", "Messy"));
        //    appearances[female, (int)slot.hair].Add("messy2", new Appearance("characters/hair/female/messy2", "Messy 2"));
        //    appearances[female, (int)slot.hair].Add("mohawk", new Appearance("characters/hair/female/mohawk", "Mohawk"));
        //    appearances[female, (int)slot.hair].Add("page", new Appearance("characters/hair/female/page", "Page"));
        //    appearances[female, (int)slot.hair].Add("page2", new Appearance("characters/hair/female/page2", "Page 2"));
        //    appearances[female, (int)slot.hair].Add("parted", new Appearance("characters/hair/female/parted", "Parted"));
        //    appearances[female, (int)slot.hair].Add("pixie", new Appearance("characters/hair/female/pixie", "Pixie"));
        //    appearances[female, (int)slot.hair].Add("plain", new Appearance("characters/hair/female/plain", "plain"));
        //    appearances[female, (int)slot.hair].Add("ponytail", new Appearance("characters/hair/female/ponytail", "Pony Tail"));
        //    appearances[female, (int)slot.hair].Add("ponytail2", new Appearance("characters/hair/female/ponytail2", "Pony Tail 2"));
        //    appearances[female, (int)slot.hair].Add("princess", new Appearance("characters/hair/female/princess", "Princess"));
        //    appearances[female, (int)slot.hair].Add("shorthawk", new Appearance("characters/hair/female/shorthawk", "Short Hawk"));
        //    appearances[female, (int)slot.hair].Add("shortknot", new Appearance("characters/hair/female/shortknot", "Short Knot"));
        //    appearances[female, (int)slot.hair].Add("shoulderr", new Appearance("characters/hair/female/shoulderr", "Shoulder"));
        //    appearances[female, (int)slot.hair].Add("swoop", new Appearance("characters/hair/female/swoop", "Swoop"));
        //    appearances[female, (int)slot.hair].Add("unkempt", new Appearance("characters/hair/female/unkempt", "unkempt"));
        //    appearances[female, (int)slot.hair].Add("xlong", new Appearance("characters/hair/female/xlong", "Extra Long"));
        //    appearances[female, (int)slot.hair].Add("xlong knot", new Appearance("characters/hair/female/xlongknot", "Extra Long knot"));
        //    //skeleton Hair
        //    appearances[skeleton, (int)slot.hair].Add("no hair", new Appearance("No Hair"));
        //    appearances[skeleton, (int)slot.hair].Add("mohawk", new Appearance("characters/hair/male/mohawk", "Mohawk"));
        //    appearances[skeleton, (int)slot.hair].Add("messy1", new Appearance("characters/hair/male/messy1", "Messy"));
        //    appearances[skeleton, (int)slot.hair].Add("messy2", new Appearance("characters/hair/male/messy2", "Messy 2"));
        //}


        /// <summary>
        /// Creates a random apperanceKey list for the supplyed Gender
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        //public static List<AppearanceKey> RandomApperance(Character.sex sex)
        //{
        //    List<AppearanceKey> randomApperance = new List<AppearanceKey>();
        //    Random random = new Random();
        //    var numberSlots = Enum.GetNames(typeof(slot)).Length;
        //    for (int i = 0; i < numberSlots; i++)
        //    {
        //        var keys = appearances[(int)sex, i].Keys.ToList();
        //        string key = keys[random.Next(0, keys.Count)];
        //        randomApperance.Add(new AppearanceKey((int)sex, i, key));
        //    }
        //    return randomApperance;
        //}

        //public static Appearance GetAppearance(Character.sex sex, slot slot, string key)
        //{
        //    Appearance appearance;
        //    appearances[(int)sex, (int)slot].TryGetValue(key, out appearance);
        //    return appearance;
        //}
        //public static Appearance GetAppearance(AppearanceKey key)
        //{
        //    Appearance appearance;
        //    appearances[(int)key.sex, (int)key.slot].TryGetValue(key.key, out appearance);
        //    return appearance;
        //}
        //public static Sprite GetAppearaceSprite(AppearanceKey key)
        //{

        //}

    }
}
