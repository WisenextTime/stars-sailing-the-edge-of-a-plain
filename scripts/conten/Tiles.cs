using Godot;
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
            name = TranslationServer.Translate("TILE_GRASS_NAME"),
            image = "草地1.png"
        };
        Global.Instance.Tiles["stone"] = stone = new GroundTile("stone")
        {
            name = TranslationServer.Translate("TILE_STONE_NAME"),
            image = "砂岩2.png",
            humidity = 2
        };
        Global.Instance.Tiles["water"] = water = new WaterTile("water")
        {
            name = TranslationServer.Translate("TILE_WATER_NAME"),
            image = "水域0.png"
        };
        Global.Instance.Tiles["ice"] = ice = new GroundTile("ice")
        {
            name = TranslationServer.Translate("TILE_ICE_NAME"),
            temperature = 3,
            humidity = 10,
            rough = 1.5f
        };
        Global.Instance.Tiles["muddy"] = muddy = new GroundTile("muddy")
        {
            name = TranslationServer.Translate("TILE_MUDDY_NAME"),
            humidity = 7,
            rough = 0.7f
        };
        Global.Instance.Tiles["bridge"] = bridge = new Tile("bridge")
        {
            name = TranslationServer.Translate("TILE_BRIDGE_NAME"),
            humidity = 10,
            moveType = MoveType.waterbridge
        };
        Global.Instance.Tiles["lava"] = lava = new Tile("lava")
        {
            name = TranslationServer.Translate("TILE_LAVA_NAME"),
            temperature = 10,
            moveType = MoveType.lava
        };
        Global.Instance.Tiles["air"] = air = new Tile("air")
        {
            name = TranslationServer.Translate("TILE_AIR_NAME"),
            moveType = MoveType.forbid,
            image = IndexImage.none
        };
    }
}