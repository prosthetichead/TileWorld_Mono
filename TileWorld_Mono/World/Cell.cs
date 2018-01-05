using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;

namespace TileWorld_Mono
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Cell
    {

        public enum CellType { Error, Water, Sand, Grass}

        [JsonProperty] private int cost = 0;
        [JsonProperty] private Vector2 tilePosition;
        [JsonProperty] private CellType cellType = CellType.Error;
        [JsonProperty] private Color color = Color.White;
        [JsonProperty] private Color debugColor = Color.White;


        public Vector2 PixelPosition { get { return new Vector2(tilePosition.X * World.tileWidth, tilePosition.Y * World.tileHeight); } }
        public Vector2 TilePosition { get { return tilePosition; } }
        public int TileDrawID { get { return (int)cellType; } } //change later to work out whats around it? Will need to move this to Chuck



        public Cell(CellType celltype, Vector2 tilePosition)
        {
            this.tilePosition = tilePosition;
            this.cellType = celltype;
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
