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

        Global.Instance.Tiles["grass"] = grass = new GroundTile("grass")
        {
            name = "grass",
            image = "草地1.png"
        };
        Global.Instance.Tiles["stone"] = stone = new GroundTile("stone")
        {
            name = "stone",
            image = "砂岩0.png",
            humidity = 2
        };
        Global.Instance.Tiles["water"] = water = new WaterTile("water")
        {
            name = "water",
            image = "水域0.png"
        };
        Global.Instance.Tiles["ice"] = ice = new GroundTile("ice")
        {
            name = "ice",
            temperature = 3,
            humidity = 10,
            rough = 1.5f
        };
        Global.Instance.Tiles["muddy"] = muddy = new GroundTile("muddy")
        {
            name = "muddy",
            humidity = 7,
            rough = 0.7f
        };
        Global.Instance.Tiles["bridge"] = bridge = new Tile("bridge")
        {
            name = "bridge",
            humidity = 10,
            moveType = MoveType.waterbridge
        };
        Global.Instance.Tiles["lava"] = lava = new Tile("lava")
        {
            name = "lava",
            temperature = 10,
            moveType = MoveType.lava
        };
        Global.Instance.Tiles["air"] = air = new Tile("air")
        {
            name = "air",
            moveType = MoveType.forbid,
            image = IndexImage.none
        };
    }
}