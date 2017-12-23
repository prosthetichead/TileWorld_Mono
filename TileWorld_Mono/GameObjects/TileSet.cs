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
        private Texture2D tileSetTexture;
        private int tileWidth;
        private int tileHeight;
        

        public TileSet(ContentManager content, int tileWidth, int tileHeight, string tileSetTextureName)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.tileSetTexture = content.Load<Texture2D>(tileSetTextureName);
        }

        public TileSet(Texture2D tileSetTexture, int tileWidth, int tileHeight)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.tileSetTexture = tileSetTexture;
        }

        private  Rectangle GetSourceRectangle(int ID)
        {
            int rectangleX = ID % (tileSetTexture.Width / tileWidth);
            int rectangleY = ID / (tileSetTexture.Width / tileWidth);

            return new Rectangle(rectangleX * tileWidth, rectangleY * tileHeight, tileWidth, tileHeight);
        }
        

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin, int tileID, Color color)
        {
            spriteBatch.Draw(tileSetTexture,
                    new Rectangle((int)position.X, (int)position.Y, tileWidth, tileHeight),
                    GetSourceRectangle(tileID),
                    color, 0f, origin, SpriteEffects.None, CalculateDepth(position));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin, int tileID, Color color, float Depth)
        {
            spriteBatch.Draw(tileSetTexture,
            new Rectangle((int)position.X, (int)position.Y, tileWidth, tileHeight),
            GetSourceRectangle(tileID),
            color, 0f, origin, SpriteEffects.None, Depth);
        }

        private  float CalculateDepth(Vector2 pos)
        {
            return (pos.Y + pos.X / 1000) / 1000;
        }
    }
    }