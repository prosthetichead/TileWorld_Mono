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
        //private bool collision = false;
        //private bool liquid = false;
        //private bool hurt = false;
        public enum CellType { Water, Grass, Sand}
        
        private int tileDrawID;
        private int cost = 0;
        private int tileWidth = 32;
        private int tileHeight = 32;
        private Vector2 tilePosition;
        private CellType cellType = CellType.Grass;
        
        private Color color = Color.White;
        private Color debugColor = Color.White;

        public int Cost { get { return cost; } }
        public Vector2 PixelPosition { get { return new Vector2(tilePosition.X * tileWidth, tilePosition.Y * tileHeight); } }
        public Vector2 TilePosition { get { return tilePosition; } }
        public int TileDrawID { get { return tileDrawID; } set { tileDrawID = value; } }



        public Cell(int tileDrawID, Vector2 tilePosition, int tileWidth, int tileHeight)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.tilePosition = tilePosition;
            this.tileDrawID = tileDrawID;
        }
     
        
        
        
        //Cell Whaighting for A* pathfinding
        public void EnterCell()
        {
            cost += 100;
        }
        public void ExitCell()
        {
            cost -= 100;
        }
        public void EnterCellintoPath()
        {
            cost += 10;
            debugColor = Color.RosyBrown;
        }
        public void ExitCellintoPath()
        {
            cost -= 10;
            debugColor = Color.White;
        }

   }
}
