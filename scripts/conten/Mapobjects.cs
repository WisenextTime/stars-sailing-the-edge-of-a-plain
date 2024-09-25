using Godot;
using System;
using starsailing.core;
using starsailing.lib;

namespace starsailing.conten;

class Mapobjects
{
    public static Mapobject ground , water , lava , ice , muddy , air , bridge;
    public static void PreloadTerrain()
    {

        Global.Instance.Mapobjects["ground"] = ground = new Mapobject("ground");
    }
}