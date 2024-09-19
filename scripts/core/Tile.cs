using Godot;
using System;
using System.Data.Common;
namespace starsailing.core

{
    public partial class Tile : Resource
    {
        protected string id;
        public string name;
        public enum MoveType{ground,water,lava,forbid,waterbridge,none}
        public MoveType moveType=MoveType.none;
        public string image;
        public Vector2I size = new(16,16);
        public float rough = 1;
        public Tile(string id){
            this.id=id;
        }

    }
    public partial class GroundTile : Tile
    {
        public GroundTile(string id) : base(id)
        {
            moveType = MoveType.ground; 
        }
    }
    public partial class WaterTile : Tile
    {
        public WaterTile(string id) : base(id)
        {
            moveType = MoveType.water;
        }
    }
}