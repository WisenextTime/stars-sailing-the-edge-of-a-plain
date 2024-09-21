using System;
using Godot;

using starsailing.lib;
using starsailing.core;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace starsailing;

[GlobalClass]
public partial class Global : Node
{
     public static Global Instance { get; private set; }
     public List<Tile> Tiles = new();
     public List<Item> Items = new();
     public override void _Ready()
     {
          Instance = this;
     }
}
