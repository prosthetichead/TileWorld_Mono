using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace TileWorld_Mono
{

    public struct ChunkData
    {
        
    }

    [JsonObject(MemberSerialization.Fields)]
    class Chunk
    {
        private DateTime timeLastUsed;
        private DateTime timeCreated;
        private DateTime timeModified;
        private string worldName;
        private int chunkRootPosX;
        private int chunkRootPosY;
        private Cell[,] cells;
        
        public Chunk(int chunkRootPosX, int chunkRootPosY, string worldName)
        {
            this.worldName = worldName;
            
            timeLastUsed = DateTime.Now;
            timeCreated = DateTime.Now;
            timeModified = DateTime.Now;


            this.chunkRootPosX = chunkRootPosX;
            this.chunkRootPosY = chunkRootPosY;
            
            cells = new Cell[World.chunkWidth, World.chunkHeight];


            CreateChunk();
        }
        
        private float getNoise(float factor, int x, int y, int z)
        {
            return (float)Noise2.Noise(2 * x * factor, 2 * y * factor, z * factor) + (float)Noise2.Noise(4 * x * factor, 4 * y * factor, z * factor) + (float)Noise2.Noise(8 * x * factor, 8 * y * factor, z * factor);
                       
        }

        /// <summary>
        /// Reads in chunk file from text file or if file dosnt exists creates a new chunk
        /// creates the main background layer this layer must be created before others as it determains were things will spawn and be placed
        /// </summary>
        /// <returns></returns>
        private void CreateChunk()
        {
            string chunkFileName = worldName + "\\" + chunkRootPosX + "," + chunkRootPosY + ".chunk";
            
            float sand = .1f;
            float water = 0f;
            float grass = 2; //it shouldnt EVER return more then 1, but it does..

            float factor =.001f;
            
            int cX = 0;
            int cY = 0;
            for (int Y = World.chunkHeight * chunkRootPosY; Y < (World.chunkHeight * chunkRootPosY) + World.chunkHeight; Y++)
            {
                for (int X = World.chunkWidth * chunkRootPosX; X < ((World.chunkWidth * chunkRootPosX) + World.chunkWidth); X++)
                {
                    float noise = getNoise(factor, X, Y, 100);

                    //create the cell
                    if (noise <= water)//Water Cell
                        cells[cX, cY] = new Cell(Cell.CellType.Water, new Vector2(X, Y));
                    else if (noise <= sand)//Sand Cell
                        cells[cX, cY] = new Cell(Cell.CellType.Sand, new Vector2(X, Y));
                    else if (noise <= grass)//Grass cell
                        cells[cX, cY] = new Cell(Cell.CellType.Grass, new Vector2(X, Y));
                    ++cX;
                }
                cX = 0;
                ++cY;
            }
           
        }

        public Cell GetBackgroundCell(int x, int y)
        {
            Cell cell;
            if (x >= World.chunkWidth || y >= World.chunkHeight)
                cell = new Cell(Cell.CellType.Error, new Vector2(x, y));
            else
                cell = cells[x, y];
             return cell;
        }

        public void SetBackgroundCell(int x, int y, Cell newCell)
        {
            cells[x, y] = newCell;
        }




        //private void readChunkFromHDD() {
        //    //Open the file written above and read values from it.
        //    Stream stream = File.Open("World\\" + chunkData.ChunkRootPosX + "," + chunkData.ChunkRootPosY + ".bin", FileMode.Open);
        //    IFormatter formatter = new BinaryFormatter();
        //    chunkData = (ChunkData)formatter.Deserialize(stream);
        //    stream.Close();
        //}
    }
}

