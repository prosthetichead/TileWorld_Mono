using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;



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

        public Appearance(string texturePath, string name, ContentManager content, int width = 64, int height = 64)
        {
            this.texturePath = texturePath;
            texture = content.Load<Texture2D>(texturePath);
            this.name = name;
            this.width = width;
            this.height = height;
        }
    }

    public static class Appearances
    {
        public enum slot { body, facial, hair};

        private static Dictionary<String, Appearance>[,] appearances;
        
        private static void InitialiseAppearances()
        {
            appearances = new Dictionary<string, Appearance>[2, 3];
            var numberSlots = Enum.GetNames(typeof(slot)).Length;
            for (int i = 0; i < numberSlots; i++)
            {
                appearances[0, i] = new Dictionary<string, Appearance>(); //male
                appearances[1, i] = new Dictionary<string, Appearance>(); //female
            }
        }

        public static void LoadContent(ContentManager content)
        {
            int male = (int)Character.sex.male;
            int female = (int)Character.sex.female;

            //start up the list
            InitialiseAppearances();
            
            // MALE BODIES
            appearances[male, (int)slot.body].Add("light", new Appearance("characters/body/male/light", "Male Human Light Skin Tone", content));
            appearances[male, (int)slot.body].Add("dark", new Appearance("characters/body/male/dark", "Male Human Dark Skin Tone", content));
            appearances[male, (int)slot.body].Add("skeleton", new Appearance("characters/body/male/skeleton", "Male Undead", content));
            
            // FEMALE BODIES
            appearances[female, (int)slot.body].Add("light", new Appearance("characters/body/female/light", "Female Human Light Skin Tone", content));
            appearances[female, (int)slot.body].Add("orc", new Appearance("characters/body/female/orc", "Female Orc", content));
            appearances[female, (int)slot.body].Add("dark", new Appearance("characters/body/female/dark", "Female Human Dark Skin Tone", content));

            // MALE FACIAL
            appearances[male, (int)slot.facial].Add("no facial", new Appearance("Male No Facial Hair"));
            appearances[male, (int)slot.facial].Add("beard black", new Appearance("characters/facial/male/beard/black", "Male Black Beard", content));
            appearances[male, (int)slot.facial].Add("beard blonde", new Appearance("characters/facial/male/beard/blonde", "Male Blonde Beard", content));

            // FEMALE FACIAL
            appearances[female, (int)slot.facial].Add("no facial", new Appearance("Female No Facial Hair"));
            appearances[female, (int)slot.facial].Add("beard black", new Appearance("characters/facial/female/beard/black", "Female Black Beard", content));
            appearances[female, (int)slot.facial].Add("beard blonde", new Appearance("characters/facial/female/beard/blonde", "Female Blonde Beard", content));
            appearances[female, (int)slot.facial].Add("beard blue", new Appearance("characters/facial/female/beard/blue", "Female Blue Beard", content));

            // MALE HAIR
            appearances[male, (int)slot.hair].Add("no hair", new Appearance("Male No Hair"));
            appearances[male, (int)slot.hair].Add("xlong black", new Appearance("characters/hair/male/xlong/black", "Male Black Extra Long", content));
            appearances[male, (int)slot.hair].Add("plain black", new Appearance("characters/hair/male/plain/black", "Male Black Plain", content));
            appearances[male, (int)slot.hair].Add("mohawk white", new Appearance("characters/hair/male/mohawk/white", "Male White Mohawk", content));

            // FEMALE HAIR
            appearances[female, (int)slot.hair].Add("no hair", new Appearance("Female No Hair"));
            appearances[female, (int)slot.hair].Add("bunches black", new Appearance("characters/hair/female/bunches/black", "Female Black Bunches", content));
            appearances[female, (int)slot.hair].Add("bunches red", new Appearance("characters/hair/female/bunches/redhead", "Female Red Bunches", content));
            appearances[female, (int)slot.hair].Add("xlong pink", new Appearance("characters/hair/female/xlong/pink", "Female Pink Extra Long", content));
            appearances[female, (int)slot.hair].Add("xlong pink2", new Appearance("characters/hair/female/xlong/pink2", "Female Pink 2 Extra Long", content));

        }

        public static List<AppearanceKey> RandomApperance(Character.sex sex)
        {
            List<AppearanceKey> randomApperance = new List<AppearanceKey>();
            Random random = new Random();
            var numberSlots = Enum.GetNames(typeof(slot)).Length;
            for (int i = 0; i < numberSlots; i++)
            {
                var keys = appearances[(int)sex, i].Keys.ToList();
                string key = keys[random.Next(0, keys.Count-1)];
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
