using Godot;
using System;
using starsailing.lib;
using starsailing;

namespace starsailing.core;


public partial class Tile : Resource
{
    public string id{get;protected set;}
    public string name;
    public MoveType moveType = MoveType.none;
    public string image = IndexImage.missing;
    public Vector2I size = new(16, 16);
    public float rough = 1f;
    //粗糙度决定单位在该地块上行走的加速度减速度修正,数值越低越光滑
    //注意,粗糙度应该是一个不为零的正浮点数
    public float temperature = 5;
    public float humidity = 5;
    //温度和潮湿度作为随机地图生成地块的判定

    public Tile(string id)
    {
        this.id = id;
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
        humidity = 10;
    }
}
