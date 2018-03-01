using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TileWorld_Mono
{
    class World
    {
        private string worldName;
        private int range = 2;
        public static readonly int tileWidth = 32;
        public static readonly int tileHeight = 32;
        public static readonly int chunkWidth = 64;
        public static readonly int chunkHeight = 64;
        
        private IDictionary<string, Chunk> chunkDictonary = new Dictionary<string, Chunk>();
        

        private int currentChunkX;
        private int currentChunkY;

        private TileSet GroundTiles;

        
        public World( string worldName )
        {
            this.worldName = worldName;
            
            // convert world name to seed value
            var md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(worldName));
            var seed = BitConverter.ToInt32(hashed, 0);

            Noise2.SetSeed(seed, .006f);
            
        }

        public void LoadContent(ContentManager content)
        {
            GroundTiles = new TileSet(content, tileWidth, tileHeight, "tileSets/groundTiles");
        }
        
        public Chunk GetChunk(int tileNumberX, int tileNumberY)
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

        public Cell[] GetCellArray(Cell cell)
        {

            Cell[] CellArray = new Cell[4];
            int X = (int)cell.TilePosition.X;
            int Y = (int)cell.TilePosition.Y;

            CellArray[0] = GetCell(X + 1, Y);
            CellArray[1] = GetCell(X - 1, Y);
            CellArray[2] = GetCell(X, Y + 1);
            CellArray[3] = GetCell(X, Y - 1);

            return CellArray;
        }


        public Cell GetCellFromPixelPos(Vector2 position)
        {
            return GetCell((int)(position.X / tileWidth), (int)(position.Y / tileWidth));
        }
        public Cell GetCell(int X, int Y)
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
            {
                return new Cell(Cell.CellType.Error, new Vector2(X, Y));
            }
        }


        private void UpdateChunks()
        {
            int XStart = currentChunkX - range;
            int XEnd = currentChunkX + range;
            int YStart = currentChunkY - range;
            int YEnd = currentChunkY + range;
            
            for (int X = XStart; X <= XEnd; X++)
            {
                for (int Y = YStart; Y <= YEnd; Y++)
                {
                    //int chunkX = getChunkX(X);
                    //int chunkY = getChunkY(Y);
                    string chunkKey = X + "," + Y;

                    
                    if (!chunkDictonary.ContainsKey(chunkKey))
                    {
                        //TODO: Look on the HDD for the chunk.

                        
                        //create the chunk add the chunk to the dictonary
                        var chunk = new Chunk(X, Y, worldName);
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(chunk);
                        chunkDictonary.Add(chunkKey, chunk);

                       // FileSystem.WriteTextLocalStorage(worldName + "\\" + chunkKey)
                        FileSystem.WriteTextLocalStorage(worldName + "\\" + chunkKey, json);

                    }
                }
            }
        }



        private void CleanUpChunks()
        {
            
            KeyValuePair<string, Chunk>[] ChunkDicArray = chunkDictonary.ToArray();
            for (int i = 0; i < ChunkDicArray.Count(); i++)
            {
               // if (ChunkDicArray[i].Value.markedForDelete)
               // {
                    //write any NPC currently inside the chunk to the NPCData list inside the chunk
                    //remove the NPC from the world
                    //ChunkDicArray[i].Value.writeChunkToHDD();
                    //chunkDictonary.Remove(ChunkDicArray[i].Key);
               // }
            }
        }
        
        private Boolean IsNewChunk(Vector2 position)
        {
            int x = (int)position.X / tileWidth;
            int y = (int)position.Y / tileHeight;

            int chunkX = getChunkX(x);
            int chunkY = getChunkY(y);
            
            if ((currentChunkX == chunkX) & (currentChunkY == chunkY))
            {
                //this is the same chunk no need to update chunk dictonary!
                return false; 
            }
            else //we are in a new chunk
            {
                Game.debugConsole.WriteLine("Changed Chunks " + chunkX + "," + chunkY);
                currentChunkX = chunkX; //update current chunk root pos
                currentChunkY = chunkY;
                return true; 
            }  
        }


        public void Update(GameTime gameTime)
        {
            //check the position of the camera are we in looking at a new chunk?
            Vector2 position = Game.camera.Position;

            IsNewChunk(position);
            UpdateChunks();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle visArea = Game.camera.VisibleArea;
            Vector2 camPosition = Game.camera.Position;
            Vector2 origin = Vector2.Zero;
            
            Cell cell;
            
            int tilesWide = visArea.Width / tileWidth;
            int tilesHigh = visArea.Height / tileHeight;

            for (int tX = -1; tX <= tilesWide+1; tX++)
            {
                for (int tY = -1; tY <= tilesHigh+1; tY++)
                {

                    int tileX = visArea.X + (tX * tileWidth);
                    int tileY = visArea.Y + (tY * tileHeight);
                    cell = GetCellFromPixelPos(new Vector2(tileX, tileY));

                    Vector2 tileDrawPos = cell.PixelPosition; // new Vector2(cell.tilePosition.X * tileWidth, cell.tilePosition.Y * tileHeight);
                    GroundTiles.Draw(spriteBatch, tileDrawPos, origin, cell.TileDrawID, Color.White);

                    if (Game.debugMode)
                    {
                        if ( cell.PixelPosition.X <= camPosition.X + 200 && cell.PixelPosition.X >= camPosition.X - 200 && cell.PixelPosition.Y <= camPosition.Y + 200 && cell.PixelPosition.Y >= camPosition.Y - 200)
                        {
                            spriteBatch.DrawString(Fonts.ArmaFive, cell.TilePosition.X + "," + cell.TilePosition.Y, new Vector2(tileDrawPos.X , tileDrawPos.Y + 20), Color.White, 0f, origin, 1f, SpriteEffects.None, .0000000012f);
                            spriteBatch.DrawString(Fonts.ArmaFive, cell.TilePosition.X + "," + cell.TilePosition.Y, new Vector2(tileDrawPos.X + 2, tileDrawPos.Y + 22), Color.Black, 0f, origin, 1f, SpriteEffects.None, .0000000011f);                           
                        }
                    }
                }
            }
        }



        #region Get ChunkPos
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