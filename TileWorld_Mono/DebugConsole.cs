using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace TileWorld_Mono
{
    public class DebugConsole
    {
        struct DebugLine
        {
            public string text;
            public string time;
            public string type;
            public Color color;
            public DebugLine(string text, string type, Color color)
            {
                this.text = text;
                this.type = type;
                this.color = color;
                this.time = DateTime.Now.ToString();
            }

            public string GetLineText()
            {
                return "[" + time + "][" + type + "] : " + text;
            }

        }

        List<DebugLine> lines;
        Color defaultColor = Color.GreenYellow;
        GraphicsDevice graphicsDevice;
        Texture2D consoleBackground;


        Framerate framerate;

        float currentHeight;
        public float CurrentHeight { get { return currentHeight; } }
        int maxHeight;

        bool visible = false;
        
        public DebugConsole(GraphicsDevice graphicsDevice, int maxHeight)
        {
            this.graphicsDevice = graphicsDevice;
            lines = new List<DebugLine>();
            currentHeight = 0;
            this.maxHeight = maxHeight;
            lines.Add(new DebugLine("Debug Logging Started", "Info", defaultColor));

            framerate = new Framerate(50);
        }

        [Conditional("DEBUG")]
        public void LoadContent(ContentManager content)
        {
            //setup console background
            consoleBackground = new Texture2D(graphicsDevice, 1, 1);
            consoleBackground.SetData(new[] { new Color(0,0,0,.6f) });
        }

        [Conditional("DEBUG")]
        public void WriteLine(object msg, string type = "Info")
        {
            var debugline = new DebugLine(msg.ToString(), type, defaultColor);
            lines.Add(debugline);
            System.Diagnostics.Debug.WriteLine(debugline.GetLineText());
        }

        [Conditional("DEBUG")]
        public void Update(GameTime gameTime)
        {
            if (Inputs.IsKeyTriggered(Microsoft.Xna.Framework.Input.Keys.OemTilde) )
            {
                visible = !visible;
            }

            //update framrate counter
            framerate.Update(gameTime.ElapsedGameTime.TotalSeconds);
            if (visible && currentHeight <= maxHeight)
            {
                currentHeight = currentHeight + (gameTime.ElapsedGameTime.Milliseconds/4);
            }
            else if(!visible && currentHeight > 0)
            {
                currentHeight = currentHeight - (gameTime.ElapsedGameTime.Milliseconds / 4);
            }
        }

        [Conditional("DEBUG")]
        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentHeight>0)
            {
                var bounds = new Rectangle(0, 0, graphicsDevice.Viewport.Width, (int)currentHeight);

                spriteBatch.Draw(consoleBackground, bounds, Color.White);
                Vector2 textPos = new Vector2(bounds.X + 10, bounds.Bottom - 25);
                int lineNumber = lines.Count - 1;
                while (textPos.Y > (bounds.Top+10) && lineNumber >= 0)
                {
                    var line = lines[lineNumber];
                    spriteBatch.DrawString(Fonts.ArmaFive, line.GetLineText(), textPos, line.color, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1);
                    textPos.Y -= 20;
                    lineNumber -= 1;
                }
                if (currentHeight > 10)
                {
                    spriteBatch.DrawString(Fonts.ArmaFive, "FPS: " + Math.Round(framerate.framerate, 2), new Vector2(5, -6), Color.Yellow, 0f, Vector2.Zero, 3f, SpriteEffects.None, 1);
                }
            }
        }
    }
}
