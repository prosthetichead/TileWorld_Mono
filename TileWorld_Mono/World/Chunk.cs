using System;
using Microsoft.Xna.Framework;

namespace TileWorld_Mono
{

    public struct ChunkData
    {
        
    }

    class Chunk
    {
        private DateTime timeLastUsed;
        private DateTime timeCreated;
        private DateTime timeModified;
        private int seed;
        private string worldName;
        private int chunkRootPosX;
        private int chunkRootPosY;
        private Cell[,] cells; private bool markedForClean = false;
        private int tileWidth;
        private int tileHeight;
        
        public Chunk(int chunkRootPosX, int chunkRootPosY, string worldName)
        {
            this.worldName = worldName;
            seed = worldName.GetHashCode();
            Noise2.SetSeed(seed);

            timeLastUsed = DateTime.Now;
            timeCreated = DateTime.Now;
            timeModified = DateTime.Now;


            this.chunkRootPosX = chunkRootPosX;
            this.chunkRootPosY = chunkRootPosY;
            
            cells = new Cell[World.chunkWidth, World.chunkHeight];
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
            string chunkFileName = chunkData.WorldName + "\\" + chunkData.ChunkRootPosX + "," + chunkData.ChunkRootPosY + ".chunk";
            
            float sand = .1f;
            float water = 0f;
            float grass = 2; //it shouldnt EVER return more then 1, but it does..

            float factor =.001f;
            
            int itX = 0;
            int itY = 0;
            for (int y = chunkData.ChunkTilesHeight * chunkData.ChunkRootPosY; y < (chunkData.ChunkTilesHeight * chunkData.ChunkRootPosY) + chunkData.ChunkTilesHeight; y++)
            {
                for (int x = chunkData.ChunkTilesWidth * chunkData.ChunkRootPosX; x < ((chunkData.ChunkTilesWidth * chunkData.ChunkRootPosX) + chunkData.ChunkTilesWidth); x++)
                {
                    float noise = getNoise(factor, x, y, 100);
                    //float leftNoise = getNoise(factor, x-1, y, 100);
                    //float rightNoise = getNoise(factor, x+1, y, 100);
                    //float topNoise = getNoise(factor, x, y-1, 100);
                    //float bottomNoise = getNoise(factor, x, y+1, 100);
                    //float leftTopNoise = getNoise(factor, x - 1, y - 1, 100);
                    //float rightTopNoise = getNoise(factor, x+1, y-1, 100);
                    //float leftBottomNoise = getNoise(factor, x-1, y+1, 100);
                    //float rightBottomNoise = getNoise(factor, x+1, y+1, 100);

                    //create the cell
                    chunkData.ChunkBackGroundLayer[itX, itY] = new Cell(0, new Vector2(x, y), tileWidth, tileHeight);
                    if (noise <= water)//Water Cell
                    {
                        chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 5; //is a liquid
                    }
                    else if (noise <= sand)//Sand Cell
                    {
                        chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 2; //Normal Sand!


                       // if (leftNoise <= water) //left tile is water, place a left water to right sand tile
                       //     chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 80;
                       // if (rightNoise <= water) //right tile is water, place a right water to left sand tile
                       //     chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 82;
                       // if (topNoise <= water) //top tile water, place a top water to bottom sand tile
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 61;
                       // if (bottomNoise <= water) //bottom tile water, place a bottom water to top sand tile
                       //     chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 101;

                        //clean up for Top Left
                        //if ((leftNoise >= water) & (topNoise >= water) & (leftTopNoise <= water))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 42;
                        //if ((leftNoise <= water) & (topNoise <= water) & (leftTopNoise <= water))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 60;

                        //clean up for Top Right
                        //if ((rightNoise >= water) & (topNoise >= water) & (rightTopNoise <= water))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 41;
                        //if ((rightNoise <= water) & (topNoise <= water) & (rightTopNoise <= water))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 62;

                        //clean up for Bottom Left
                       // if ((leftNoise >= water) & (bottomNoise >= water) & (leftBottomNoise <= water))
                       //     chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 22;
                       // if ((leftNoise <= water) & (bottomNoise <= water) & (leftBottomNoise <= water))
                       //     chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 100;
                        //clean up for Bottom right
                       // if ((rightNoise >= water) & (bottomNoise >= water) & (rightBottomNoise <= water))
                       //     chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 21;
                       // if ((rightNoise <= water) & (bottomNoise <= water) & (rightBottomNoise <= water))
                       //     chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 102;


                    }
                    else if (noise <= grass)//Grass cell
                    {
                        chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 1;
                        //if (leftNoise >= water & leftNoise <= sand) //left tile is sand
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 85;
                       // if (rightNoise >= water & rightNoise <= sand) //right tile is sand
                       //     chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 83;
                      //  if (topNoise >= water & topNoise <= sand) //top tile sand
                      //      chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 104;
                      //  if (bottomNoise >= water & bottomNoise <= sand) //top tile sand
                      //      chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 64;


                        // top left cleanup
                        //if ((leftNoise >= water & leftNoise <= sand) & (topNoise >= water & topNoise <= sand) & (leftTopNoise >= water & leftTopNoise <= sand))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 24;
                        //if ((leftNoise >= sand) & (topNoise >= sand) & (leftTopNoise >= water & leftTopNoise <= sand))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 105;

                        ////Top Right Cleanup
                        //if ((rightNoise >= water & rightNoise <= sand) & (topNoise >= water & topNoise <= sand) & (rightTopNoise >= water & rightTopNoise <= sand))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 25;
                        //if ((rightNoise >= sand) & (topNoise >= sand) & (rightTopNoise >= water & rightTopNoise <= sand))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 103;

                        ////Bottom Left Cleanup
                        //if ((leftNoise >= water & leftNoise <= sand) & (bottomNoise >= water & bottomNoise <= sand) & (leftBottomNoise >= water & leftBottomNoise <= sand))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 44;
                        //if ((leftNoise >= sand) & (bottomNoise >= sand) & (leftBottomNoise >= water & leftBottomNoise <= sand))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 65;

                        ////Bottom Right Cleanup
                        //if ((rightNoise >= water & rightNoise <= sand) & (bottomNoise >= water & bottomNoise <= sand) & (rightBottomNoise >= water & rightBottomNoise <= sand))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 45;
                        //if ((rightNoise >= sand) & (bottomNoise >= sand) & (rightBottomNoise >= water & rightBottomNoise <= sand))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 63;
                          
                        //if ((leftNoise <= water) & (topNoise <= water) & (leftTopNoise <= water))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 48;
                        //if ((leftNoise >= water) & (topNoise >= water) & (leftTopNoise <= water))
                        //    chunkData.ChunkBackGroundLayer[itX, itY].TileDrawID = 44;
                    }
                    ++itX;
                }
                itX = 0;
                ++itY;
            }
        }

        public Cell GetBackgroundCell(int x, int y)
        {
            Cell cell;
            if (x >= chunkData.ChunkTilesWidth || y >= chunkData.ChunkTilesHeight)
                cell = new Cell(0, new Vector2(x, y), tileWidth, tileHeight);
            else
                cell = chunkData.ChunkBackGroundLayer[x, y];
             return cell;
        }

        public void SetBackgroundCell(int x, int y, Cell newCell)
        {
            chunkData.ChunkBackGroundLayer[x, y] = newCell;
        }

        public void UpdateLastUsed()
        {
            chunkData.TimeLastUsed = DateTime.Now;
            markedForDelete = false;
        }

        public DateTime GetLastUsed()
        {
            return chunkData.TimeLastUsed;
        }

       // public void writeChunkToHDD()
       // {
        //    IFormatter formatter = new BinaryFormatter();
        //    Stream stream = new FileStream("World\\" + chunkData.ChunkRootPosX + ","+chunkData.ChunkRootPosY+ ".bin", FileMode.Create, FileAccess.Write, FileShare.None);
        //    formatter.Serialize(stream, chunkData);
        //    stream.Close();
        //}
        //private void readChunkFromHDD() {
        //    //Open the file written above and read values from it.
        //    Stream stream = File.Open("World\\" + chunkData.ChunkRootPosX + "," + chunkData.ChunkRootPosY + ".bin", FileMode.Open);
        //    IFormatter formatter = new BinaryFormatter();
        //    chunkData = (ChunkData)formatter.Deserialize(stream);
        //    stream.Close();
        //}
    }
}

