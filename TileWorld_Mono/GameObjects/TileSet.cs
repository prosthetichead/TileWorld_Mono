using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TileWorld_Mono
{
    class TileSet
    {
        public Texture2D tileSetTexture;
        public int tileWidth;
        public int tileHeight;
        

        public TileSet(ContentManager content, int tileWidth, int tileHeight, string tileSetTextureName)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.tileSetTexture = content.Load<Texture2D>(tileSetTextureName);
        }

       public  Rectangle getSourceRectangle(int ID)
        {
            int rectangleX = ID % (tileSetTexture.Width / tileWidth);
            int rectangleY = ID / (tileSetTexture.Width / tileWidth);

            return new Rectangle(rectangleX * tileWidth, rectangleY * tileHeight, tileWidth, tileHeight);
        }
        

        public void draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin, int tileID, Color color)
        {
            spriteBatch.Draw(tileSetTexture,
                    new Rectangle((int)position.X, (int)position.Y, tileWidth, tileHeight),
                    getSourceRectangle(tileID),
                    color, 0f, origin, SpriteEffects.None, CalculateDepth(position));
        }

        public void draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin, int tileID, Color color, float Depth)
        {
            spriteBatch.Draw(tileSetTexture,
            new Rectangle((int)position.X, (int)position.Y, tileWidth, tileHeight),
            getSourceRectangle(tileID),
            color, 0f, origin, SpriteEffects.None, Depth);
        }

        private  float CalculateDepth(Vector2 pos)
        {
            return (pos.Y + pos.X / 1000) / 1000;
        }
    }
    }