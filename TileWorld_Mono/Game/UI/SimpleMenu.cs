using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileWorld_Mono
{
    class SimpleMenu  
    {
        public class SimpleMenuItem
        {
            public string text;
            public SpriteFont font;
            public Color color;

            public SimpleMenuItem(string text, SpriteFont font, Color color)
            {
                this.text = text;
                this.font = font;
                this.color = color;
            }
        }

        private Rectangle menuRect;
        private List<SimpleMenuItem> menuItems;
        private int currentIndex;
        private Texture2D menuBackground;
        private Color selectedColor;
        private Color backgroundColor;

        public SimpleMenu(Rectangle rectangle, Color selectedColor, Color backgroundColor)
        {
            menuRect = rectangle;

            this.selectedColor = selectedColor;
            this.backgroundColor = backgroundColor;

            menuItems = new List<SimpleMenuItem>();
            currentIndex = 0;
        }

        
        public void LoadContent()
        {
            menuBackground = new Texture2D(Game.Instance.GraphicsDevice, 1, 1);
            menuBackground.SetData(new[] { backgroundColor });
        }

        public void Initialize(List<SimpleMenuItem> menuItems)
        {
            this.menuItems = menuItems;
        }

        public void Update(GameTime gameTime)
        {
            if (Inputs.IsActionTriggered(Inputs.Action.MoveUp))
            {
                currentIndex--;
                Game.debugConsole.WriteLine("Simple Menu index Move Up");
            }
            else if (Inputs.IsActionTriggered(Inputs.Action.MoveDown))
            {
                currentIndex++;
                Game.debugConsole.WriteLine("Simple Menu Index Move Down");
            }


            if (currentIndex < 0)
                currentIndex = 0;
            else if (currentIndex >= menuItems.Count)
                currentIndex = menuItems.Count-1;

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw background
            spriteBatch.Draw(menuBackground, menuRect, Color.White);

            Vector2 textPos = new Vector2(menuRect.X + 5, menuRect.Y + 5);
            for(int i =0;i< menuItems.Count;i++)
            {
                SimpleMenuItem item = menuItems[i];
                if(currentIndex == i)
                {
                    //Selected Item
                    spriteBatch.DrawString(Fonts.ArmaFive, item.text, textPos, selectedColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                }
                else
                {
                    //Unselected Item
                    spriteBatch.DrawString(Fonts.ArmaFive, item.text, textPos, item.color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
                }
                
                textPos.Y += 10;
            }
        }
    }
}
