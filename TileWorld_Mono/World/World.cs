using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Threading;

namespace TileWorld_Mono
{
    class World
    {
        private string worldName;
        private int range = 3;
        private int tileWidth;
        private int tileHeight;
        private int chunkWidth;
        private int chunkHeight;
        private IDictionary<string, Chunk> chunkDictonary = new Dictionary<string, Chunk>();


        private int currentChunkX;
        private int currentChunkY;

        private Vector2 camaraPos = new Vector2(0, 0);
        private Vector2 playerPos = new Vector2(0, 0);

        private TileSet GroundTiles;
        private TileSet TileEntites;
        private TileSet TileTrees;

        private SpriteFont fontTiny;
        //private ContentManager Content;

        public World( string worldName, int chunkWidth, int chunkHeight, int tileWidth, int tileHeight)
        {
            this.worldName = worldName;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.chunkWidth = chunkWidth;
            this.chunkHeight = chunkHeight;

            AddNewChunks((int)playerPos.X, (int)playerPos.Y);
        }

        public void LoadContent(ContentManager content)
        {
                fontTiny = content.Load<SpriteFont>(@"fonts\Font-PF Arma Five");
                GroundTiles = new TileSet(content, tileWidth, tileHeight, "tileSets/groundTiles");
                TileEntites = new TileSet(content, tileWidth, tileHeight, "tileSets/TileEntites");
                TileTrees = new TileSet(content, 64, 128, "tileSets/TileTrees");
        }

        public Vector2 getCenterOfTile(Vector2 position)
        {
            Vector2 returnVector2 = new Vector2((position.X / tileWidth), (position.Y/ tileHeight));
            return returnVector2;
        }


        public Chunk getChunk(int tileNumberX, int tileNumberY)
        {
            //chunks root position used for the chunk key
            int chunkX = getChunkX(tileNumberX);
            int chunkY = getChunkY(tileNumberY);
            //chunk key used as dictonary Key
            string chunkKey = chunkX + "," + chunkY;


            //is chunk in dictonary?
            if (chunkDictonary.ContainsKey(chunkKey)) //yes, get chunk
            {
                return chunkDictonary[chunkKey];
            }
            else //no? Return null
            {
                return null;// AddNewChunks(tileNumberX, tileNumberY);
            }
        }

        public Cell[] getCellArray(Cell cell)
        {

            Cell[] CellArray = new Cell[4];
            int X = (int)cell.tilePosition.X;
            int Y = (int)cell.tilePosition.Y;

            CellArray[0] = getCell(X + 1, Y);
            CellArray[1] = getCell(X - 1, Y);
            CellArray[2] = getCell(X, Y + 1);
            CellArray[3] = getCell(X, Y - 1);
           

            return CellArray;

        }


        public Cell getCellFromPixelPos(Vector2 position)
        {
            return getCell((int)(position.X / tileWidth), (int)(position.Y / tileWidth));
        }
        public Cell getCell(int X, int Y)
        {        
            int chunkX = getChunkX(X);
            int chunkY = getChunkY(Y);
            string chunkKey = chunkX + "," + chunkY;
            
            //position in the chunk
            int chunkPosX;
            chunkPosX = X % chunkWidth;
            if (chunkPosX < 0)
                chunkPosX = (chunkWidth) + chunkPosX;
            
            int chunkPosY;
            chunkPosY = Y % chunkHeight;
            if (chunkPosY < 0)
                chunkPosY = (chunkHeight) + chunkPosY;

            if (chunkDictonary.ContainsKey(chunkKey))
                return chunkDictonary[chunkKey].GetBackgroundCell(chunkPosX, chunkPosY);
            else
                return new Cell(0); //return a 0 error cell
        }


        private void AddNewChunks(int x, int y)
        {

            int XStart = x - (chunkWidth * range);
            int XEnd = x + (chunkWidth * range);
            int YStart = y - (chunkHeight * range);
            int YEnd = y + (chunkHeight * range);


            int chunkX;
            int chunkY;
            string chunkKey;

           for (int i = 0; i < chunkDictonary.Count; i++)
           {
            chunkDictonary.ElementAt(i).Value.markedForDelete = true;
           }

           for (int X = XStart; X <= XEnd; X =X + chunkWidth)
           {
             for (int Y = YStart; Y <= YEnd; Y = Y + chunkHeight)
            {
                    chunkX = getChunkX(X);
                    chunkY = getChunkY(Y);
                    chunkKey = chunkX + "," + chunkY;



                    if (chunkDictonary.ContainsKey(chunkKey))
                    {
                        //update last used time chunk is already in the dictonary but is still needed
                        chunkDictonary[chunkKey].UpdateLastUsed(); //also unmarks the chunk for delete.
                    }
                    else
                    {
                        //create the chunk add the chunk to the dictonary
                        chunkDictonary.Add(chunkKey, new Chunk(tileWidth,tileHeight,chunkWidth, chunkHeight, chunkX, chunkY, worldName));
                        chunkDictonary[chunkKey].initialize();
                    }
                }
            }   
                       
        }
        private void CleanUpChunks()
        {
            
            KeyValuePair<string, Chunk>[] ChunkDicArray = chunkDictonary.ToArray();
            for (int i = 0; i < ChunkDicArray.Count(); i++)
            {
                if (ChunkDicArray[i].Value.markedForDelete)
                {
                    //write any NPC currently inside the chunk to the NPCData list inside the chunk
                    //remove the NPC from the world
                    //ChunkDicArray[i].Value.writeChunkToHDD();
                    //chunkDictonary.Remove(ChunkDicArray[i].Key);
                }
            }
        }
        
        public Boolean testIsNewChunk(int x, int y)
        {
            x = x / tileWidth;
            y = y / tileHeight;

            int chunkX = getChunkX(x);
            int chunkY = getChunkY(y);
            
            if ((currentChunkX == chunkX) & (currentChunkY == chunkY))
            {
                //this is the same chunk still no need to update chunk dictonary!
                return false; 
            }
            else //we are in a new chunk
            {
                //Thread addChunksThread = new Thread(() => AddNewChunks(x, y));
                AddNewChunks(x, y);

                CleanUpChunks();
                //Thread thread = new Thread(CleanUpChunks);
                //thread.Name = "Clean Up Chunks";
                //thread.Start();
                currentChunkX = chunkX; //update current chunk root pos
                currentChunkY = chunkY;
                return true; 
            }  
        }


        public void Update(GameTime gameTime, Vector2 camaraPosition)
        {
            camaraPos = camaraPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            Vector2 firstSquare = new Vector2(camaraPos.X / GroundTiles.tileWidth, camaraPos.Y / GroundTiles.tileHeight);
            Vector2 squareOffset = new Vector2(camaraPos.X % GroundTiles.tileWidth, camaraPos.Y % GroundTiles.tileHeight);
            Vector2 origin = Vector2.Zero;
            int pixelPosX;
            int pixelPosY;
            int tilePosX;
            int tilePosY;
            Cell cell;


            for (int y = -1; y < 100; y++)
            {
                for (int x = -1; x < 100; x++)
                {

                    pixelPosX = (x * GroundTiles.tileWidth) - (int)squareOffset.X;
                    pixelPosY = (y * GroundTiles.tileHeight) - (int)squareOffset.Y;
                    tilePosX = x + (int)firstSquare.X;
                    tilePosY = y + (int)firstSquare.Y;
                    Color cellColor;
                    
                    if ((cell = getCell(tilePosX, tilePosY)) == null)
                    {
                        cell = new Cell(0);
                    }

                    if (Game.debugMode)
                        cellColor = cell.debugColor;
                    else
                        cellColor = cell.color;
                    
                    //Draw Cell
                    GroundTiles.draw(spriteBatch, new Vector2(pixelPosX, pixelPosY), origin, cell.tileID, cellColor, .0001f);

                    if (Game.debugMode)
                    {
                        int Xpos = tilePosX - (int)(playerPos.X / tileWidth);
                        int Ypos = tilePosY - (int)(playerPos.Y / tileHeight);
                        //if ((Math.Abs(tilePosX - (int)(playerPos.X / tileWidth)) < 5) & (Math.Abs(tilePosY - (int)(playerPos.Y / tileHeight)) < 5))
                        //{
                            spriteBatch.DrawString(fontTiny, cell.tilePosition.X + ", " + cell.tilePosition.Y, new Vector2(pixelPosX + 2, pixelPosY + 2), Color.White, 0f, origin, 1f, SpriteEffects.None, 1);
                            spriteBatch.DrawString(fontTiny, cell.tilePosition.X + ", " + cell.tilePosition.Y, new Vector2(pixelPosX + 3, pixelPosY + 3), Color.Black, 0f, origin, 1f, SpriteEffects.None, .9f);

                           // spriteBatch.DrawString(fontTiny, cell.chunkID, new Vector2(pixelPosX + 2, pixelPosY + 10), Color.White, 0f, origin, 1f, SpriteEffects.None, 1);
                           // spriteBatch.DrawString(fontTiny, cell.chunkID, new Vector2(pixelPosX + 3, pixelPosY + 11), Color.Black, 0f, origin, 1f, SpriteEffects.None, .9f);
                        //}
                          //spriteBatch.DrawString(fontTiny, " " + Camara.calculateDepth(new Vector2(pixelPosX, pixelPosY)), new Vector2(pixelPosX + 2, pixelPosY + 20), Color.White, 0f, origin, 1f, SpriteEffects.None, 1);

                    }
                }// END X ForLoop
            } // END Y ForLoop

        }


        #region Get ChunkPos
        //finds the chuck pos using x y cords
        private int getChunkX(int x)
        {
            int test;
            int chunkX;
            test = x % chunkWidth;
            if (test < 0)
                chunkX = (x - chunkWidth) / chunkWidth;
            else
                chunkX = x / chunkWidth;


            return chunkX;
        }
        private int getChunkY(int y)
        {
            int test;
            int chunkY;
            test = y % chunkHeight;
            if (test < 0)
                chunkY = (y - chunkHeight) / chunkHeight;
            else
                chunkY = y / chunkHeight;

            return chunkY;
        }
        #endregion
    
    }
}