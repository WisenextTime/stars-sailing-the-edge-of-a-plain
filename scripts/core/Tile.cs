using Godot;
using System;
using starsailing.lib;
using starsailing;

namespace starsailing.core;


public partial class Tile : Resource
{
    protected string id;
    public string name;
    public enum MoveType { none, ground, water, lava, forbid, waterbridge }
    /*
    用于判断地块行为
    none 留空,没有物理碰撞,也无法寻路到达,*不建议使用*
    ground 为地面,并且可以设置高度,作为最基本的地块
    water 为水面,供水上单位行走
    lava 为岩浆,不允许任何非空中单位通行
    forbid 为不可通行的区域
    waterbridge 为浅水,大部分地面大型单位和水上小型单位都可以通过
    */
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
