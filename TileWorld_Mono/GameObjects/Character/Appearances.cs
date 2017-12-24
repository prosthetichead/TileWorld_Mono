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
    public class AppearanceKey
    {
        public int sex;
        public int slot;
        public string key;

        public AppearanceKey(Character.sex sex, Appearances.slot slot, string key)
        {
            this.sex = (int)sex;
            this.slot = (int)slot;
            this.key = key;
        }
        public AppearanceKey(int sex, int slot, string key)
        {
            this.sex = sex;
            this.slot = slot;
            this.key = key;
        }
    }

    public class Appearance
    {
        private bool hasTexture = true;
        private Texture2D texture;
        private string texturePath;
        private string name;
        private int width;
        private int height;

        public Texture2D Texture => texture;
        public string Name => name;
        public int Width => width;
        public int Height => height;
        public bool HasTexture => hasTexture;

        public Appearance(string name, int width = 64, int height = 64)
        {
            this.name = name;
            this.width = width;
            this.height = height;
            this.texturePath = "";
            hasTexture = false;
        }

        public Appearance(string texturePath, string name, int width = 64, int height = 64)
        {
            this.texturePath = texturePath;
            this.name = name;
            this.width = width;
            this.height = height;
        }
        public void LoadContent(ContentManager content)
        {
            if(hasTexture = true && !String.IsNullOrWhiteSpace(texturePath))
                texture = content.Load<Texture2D>(texturePath);
            else
                texture = null;
        }
    }

    public static class Appearances
    {
        public enum slot { body, facial, hair};

        private static Dictionary<String, Appearance>[,] appearances;
        private static int numberSexes;
        private static int numberSlots;
        
        static Appearances()
        {
            numberSlots = Enum.GetNames(typeof(slot)).Length;
            numberSexes = Enum.GetNames(typeof(Character.sex)).Length;

            appearances = new Dictionary<string, Appearance>[numberSexes, numberSlots];
            for (int h = 0; h < numberSexes; h++)
            {
                for (int i = 0; i < numberSlots; i++)
                {
                    appearances[h, i] = new Dictionary<string, Appearance>();
                }
            }
            int male = (int)Character.sex.male;
            int female = (int)Character.sex.female;
            int skeleton = (int)Character.sex.skeleton;
            
            // skeleton BODIES
            appearances[skeleton, (int)slot.body].Add("skeleton", new Appearance("characters/body/male/skeleton", "Undead"));

            // MALE BODIES
            appearances[male, (int)slot.body].Add("light", new Appearance("characters/body/male/light", "Male Human Light Skin Tone"));
            appearances[male, (int)slot.body].Add("dark", new Appearance("characters/body/male/dark", "Male Human Dark Skin Tone"));
            appearances[male, (int)slot.body].Add("dark2", new Appearance("characters/body/male/dark2", "Male Human Dark Skin Tone 2"));
            appearances[male, (int)slot.body].Add("tanned", new Appearance("characters/body/male/tanned", "Male Human Tanned Skin Tone"));
            appearances[male, (int)slot.body].Add("tanned2", new Appearance("characters/body/male/tanned2", "Male Human Tanned Skin Tone 2"));
            appearances[male, (int)slot.body].Add("darkelf", new Appearance("characters/body/male/darkelf", "Male Dark Elf"));
            appearances[male, (int)slot.body].Add("darkelf2", new Appearance("characters/body/male/darkelf2", "Male Dark Elf 2"));
            appearances[male, (int)slot.body].Add("orc", new Appearance("characters/body/male/orc", "Male Orc"));
            appearances[male, (int)slot.body].Add("redorc", new Appearance("characters/body/male/red_orc", "Male Red Orc"));
            
            // FEMALE BODIES
            appearances[female, (int)slot.body].Add("light", new Appearance("characters/body/female/light", "Female Human Light Skin Tone"));
            appearances[female, (int)slot.body].Add("dark", new Appearance("characters/body/female/dark", "Female Human Dark Skin Tone"));
            appearances[female, (int)slot.body].Add("dark2", new Appearance("characters/body/female/dark2", "Female Human Dark Skin Tone 2"));
            appearances[female, (int)slot.body].Add("tanned", new Appearance("characters/body/female/tanned", "Female Human Tanned Skin Tone"));
            appearances[female, (int)slot.body].Add("tanned2", new Appearance("characters/body/female/tanned2", "Female Human Tanned Skin Tone 2"));
            appearances[female, (int)slot.body].Add("darkelf", new Appearance("characters/body/female/darkelf", "Female Dark Elf"));
            appearances[female, (int)slot.body].Add("darkelf2", new Appearance("characters/body/female/darkelf2", "Female Dark Elf 2"));
            appearances[female, (int)slot.body].Add("orc", new Appearance("characters/body/female/orc", "Female Orc"));
            appearances[female, (int)slot.body].Add("redorc", new Appearance("characters/body/female/red_orc", "Female Red Orc"));

            // MALE FACIAL
            appearances[male, (int)slot.facial].Add("no facial", new Appearance("Male No Facial Hair"));
            appearances[male, (int)slot.facial].Add("beard", new Appearance("characters/facial/beard", "Beard"));
            appearances[male, (int)slot.facial].Add("big stache", new Appearance("characters/facial/bigstache", "Big Stache"));
            appearances[male, (int)slot.facial].Add("fiveoclock", new Appearance("characters/facial/fiveoclock", "Five O'Clock Shadow"));
            appearances[male, (int)slot.facial].Add("frenchstache", new Appearance("characters/facial/frenchstache", "French Stache"));
            appearances[male, (int)slot.facial].Add("mustache", new Appearance("characters/facial/mustache", "Mustache"));
            
            // FEMALE FACIAL
            appearances[female, (int)slot.facial].Add("no facial", new Appearance("No Facial Hair"));
            appearances[female, (int)slot.facial].Add("beard", appearances[male, (int)slot.facial]["beard"]);
            appearances[female, (int)slot.facial].Add("big stache", appearances[male, (int)slot.facial]["big stache"]);
            appearances[female, (int)slot.facial].Add("fiveoclock", appearances[male, (int)slot.facial]["fiveoclock"]);
            appearances[female, (int)slot.facial].Add("frenchstache", appearances[male, (int)slot.facial]["frenchstache"]);
            appearances[female, (int)slot.facial].Add("mustache", appearances[male, (int)slot.facial]["mustache"]);

            // skeleton FACIAL
            appearances[skeleton, (int)slot.facial].Add("no facial", new Appearance("No Facial Hair"));

            // MALE HAIR
            appearances[male, (int)slot.hair].Add("no hair", new Appearance("No Hair"));
            appearances[male, (int)slot.hair].Add("bangs", new Appearance("characters/hair/bangs", "Bangs"));
            appearances[male, (int)slot.hair].Add("bangs long", new Appearance("characters/hair/bangslong", "Bangs Long"));
            appearances[male, (int)slot.hair].Add("bangs long 2", new Appearance("characters/hair/bangslong2", "Bangs Long 2"));
            appearances[male, (int)slot.hair].Add("bangs short", new Appearance("characters/hair/bangsshort", "Bangs Short"));
            appearances[male, (int)slot.hair].Add("bed head", new Appearance("characters/hair/bedhead", "BedHead"));
            appearances[male, (int)slot.hair].Add("bunches", new Appearance("characters/hair/bunches", "Bunches"));
            appearances[male, (int)slot.hair].Add("jewfro", new Appearance("characters/hair/jewfro", "Jewfro"));
            appearances[male, (int)slot.hair].Add("long", new Appearance("characters/hair/long", "Long"));
            appearances[male, (int)slot.hair].Add("long hawk", new Appearance("characters/hair/longhawk", "Long Hawk"));
            appearances[male, (int)slot.hair].Add("long knot", new Appearance("characters/hair/longknot", "Long Knot"));
            appearances[male, (int)slot.hair].Add("loose", new Appearance("characters/hair/loose", "Loose"));
            appearances[male, (int)slot.hair].Add("messy1", new Appearance("characters/hair/messy1", "Messy"));
            appearances[male, (int)slot.hair].Add("messy2", new Appearance("characters/hair/messy2", "Messy 2"));
            appearances[male, (int)slot.hair].Add("mohawk", new Appearance("characters/hair/mohawk", "Mohawk"));
            appearances[male, (int)slot.hair].Add("page", new Appearance("characters/hair/page", "Page"));
            appearances[male, (int)slot.hair].Add("page2", new Appearance("characters/hair/page2", "Page 2"));
            appearances[male, (int)slot.hair].Add("parted", new Appearance("characters/hair/parted", "Parted"));
            appearances[male, (int)slot.hair].Add("pixie", new Appearance("characters/hair/pixie", "Pixie"));
            appearances[male, (int)slot.hair].Add("plain", new Appearance("characters/hair/plain", "plain"));
            appearances[male, (int)slot.hair].Add("ponytail", new Appearance("characters/hair/ponytail", "Pony Tail"));
            appearances[male, (int)slot.hair].Add("ponytail2", new Appearance("characters/hair/ponytail2", "Pony Tail 2"));
            appearances[male, (int)slot.hair].Add("princess", new Appearance("characters/hair/princess", "Princess"));
            appearances[male, (int)slot.hair].Add("shorthawk", new Appearance("characters/hair/shorthawk", "Short Hawk"));
            appearances[male, (int)slot.hair].Add("shortknot", new Appearance("characters/hair/shortknot", "Short Knot"));
            appearances[male, (int)slot.hair].Add("shoulderr", new Appearance("characters/hair/shoulderr", "Shoulder"));
            appearances[male, (int)slot.hair].Add("swoop", new Appearance("characters/hair/swoop", "Swoop"));
            appearances[male, (int)slot.hair].Add("unkempt", new Appearance("characters/hair/unkempt", "unkempt"));
            appearances[male, (int)slot.hair].Add("xlong", new Appearance("characters/hair/xlong", "Extra Long"));
            appearances[male, (int)slot.hair].Add("xlong knot", new Appearance("characters/hair/xlongknot", "Extra Long knot"));
            // FEMALE HAIR
            foreach (var a in appearances[male, (int)slot.hair])
                appearances[female, (int)slot.hair].Add(a.Key, a.Value);
            //skeleton Hair
            appearances[skeleton, (int)slot.hair].Add("no hair", new Appearance("No Hair"));
            appearances[skeleton, (int)slot.hair].Add("mohawk", new Appearance("characters/hair/mohawk", "Mohawk"));
            appearances[skeleton, (int)slot.hair].Add("messy1", new Appearance("characters/hair/messy1", "Messy"));
            appearances[skeleton, (int)slot.hair].Add("messy2", new Appearance("characters/hair/messy2", "Messy 2"));
        }

        public static void LoadContent(ContentManager content)
        {

            //load the texturefile for all of the apperances
            for (int h = 0; h < numberSexes; h++)
            {
                for (int i = 0; i < numberSlots; i++)
                {
                    var aDics  = appearances[h, i];
                    foreach (var aDic in aDics)
                        aDic.Value.LoadContent(content);
                }
            }
        }

        /// <summary>
        /// Creates a random apperanceKey list for the supplyed Gender
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        public static List<AppearanceKey> RandomApperance(Character.sex sex)
        {
            List<AppearanceKey> randomApperance = new List<AppearanceKey>();
            Random random = new Random();
            var numberSlots = Enum.GetNames(typeof(slot)).Length;
            for (int i = 0; i < numberSlots; i++)
            {
                var keys = appearances[(int)sex, i].Keys.ToList();
                string key = keys[random.Next(0, keys.Count)];
                randomApperance.Add(new AppearanceKey((int)sex, i, key));
            }
            return randomApperance;
        }

        public static Appearance GetAppearance(Character.sex sex, slot slot, string key)
        {
            Appearance appearance;
            appearances[(int)sex, (int)slot].TryGetValue(key, out appearance);
            return appearance;
        }
        public static Appearance GetAppearance(AppearanceKey key)
        {
            Appearance appearance;
            appearances[(int)key.sex, (int)key.slot].TryGetValue(key.key, out appearance);
            return appearance;
        }

    }
}
