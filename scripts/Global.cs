using System;
using Godot;

using starsailing.lib;
using starsailing.core;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace starsailing;

[GlobalClass]
public partial class Global : Node
{
     public static Global Instance { get; private set; }
     public Dictionary<string,Tile> Tiles = new();
     public Dictionary<string,Item> Items = new();
     public Dictionary<string, Mapobject> Mapobjects = new();

    public int OStype;

    public string  GameMap;

     public override void _Ready()
     {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            OStype = 0;
        }
        else
        {
            OStype = 1;
        }
        Instance = this;
     }
}
