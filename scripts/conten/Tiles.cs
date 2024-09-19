using Godot;
using System;
using starsailing.core;

class Tiles
{
    public static Tile grass,stone,ice,muddy,water,bridge,lava,air;
    public void Preload()
    {
        grass = new GroundTile("grass"){
            name="grass",
            image="草地1.png"
        };
        stone = new GroundTile("stone"){
            name="stone",
            image="砂岩0.png"
        };
        water = new WaterTile("water")
        {
            name = "water",
            image = "水域0.png"
        };
    }
}