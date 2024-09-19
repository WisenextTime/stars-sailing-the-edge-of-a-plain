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
        grass = new GroundTile("grass")
        {
            name = "grass",
            image = "草地1.png"
        };
        stone = new GroundTile("stone")
        {
            name = "stone",
            image = "砂岩0.png",
            humidity = 2
        };
        water = new WaterTile("water")
        {
            name = "water",
            image = "水域0.png"
        };
    }
}