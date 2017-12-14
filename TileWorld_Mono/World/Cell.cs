using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TileWorld_Mono
{
    public class Cell
    {   
        public int tileID;
        public bool collision = false;
        public bool liquid = false;
        public bool hurt = false;
        private int occupiedCount = 0;
        public int cost = 0;


        public Vector2 tilePosition;
        public Vector2 pixelPosition;
        public Vector2 pixelPositionCenter;
        public string chunkID = "NA";
        public Color color = Color.White;
        public Color debugColor = Color.White;
        
        public int OccupiedCount { get { return occupiedCount; } }


        public Cell(int tileID)
        {
            this.tileID = tileID;
        }

        public Cell(int tileID, bool collision, bool liquid, bool hurt)
        {   
            this.tileID = tileID;
            this.collision= collision;
            this.liquid =  liquid;
            this.hurt = hurt;
        }

        //Cell Whaighting for A* pathfinding
        public void EnterCell()
        {
            occupiedCount += 100;
        }
        public void ExitCell()
        {
            occupiedCount -= 100;
        }
        public void EnterCellintoPath()
        {
            occupiedCount += 10;
            debugColor = Color.RosyBrown;
        }
        public void ExitCellintoPath()
        {
            occupiedCount -= 10;
            debugColor = Color.White;
        }

   }
}
