using Godot;
using System;
using starsailing.core;
using starsailing.lib;

namespace starsailing.conten;
class Tiles
{
    public static Tile grass, stone, ice, muddy, water, bridge, lava, air;
    public static void PreloadTile()
    {

        Global.Instance.Tiles.Add(grass = new GroundTile("grass")
        {
            name = "grass",
            image = "草地1.png"
        });
        Global.Instance.Tiles.Add(stone = new GroundTile("stone")
        {
            name = "stone",
            image = "砂岩0.png",
            humidity = 2
        });
        Global.Instance.Tiles.Add(water = new WaterTile("water")
        {
            name = "water",
            image = "水域0.png"
        });
        Global.Instance.Tiles.Add(ice = new GroundTile("ice")
        {
            name = "ice",
            temperature = 3,
            humidity = 10,
            rough = 1.5f
        });
        Global.Instance.Tiles.Add(muddy = new GroundTile("muddy")
        {
            name = "muddy",
            humidity = 7,
            rough = 0.7f
        });
        Global.Instance.Tiles.Add(bridge = new Tile("bridge")
        {
            name = "bridge",
            humidity = 10,
            moveType = Tile.MoveType.waterbridge
        });
        Global.Instance.Tiles.Add(lava = new Tile("lava")
        {
            name = "lava",
            temperature = 10,
            moveType = Tile.MoveType.lava
        });
        Global.Instance.Tiles.Add(air = new Tile("air")
        {
            name = "air",
            moveType = Tile.MoveType.forbid
        });
    }
}