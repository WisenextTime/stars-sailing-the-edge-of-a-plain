using Godot;
using System;
using starsailing.canvas;
using starsailing.core;
using starsailing;
using System.Collections.Generic;

public partial class ObjectDebug : FlowContainer
{
	public override void _Ready()
	{
		PackedScene tileIns = GD.Load<PackedScene>("res://scenes/prefab/debug tile.tscn");
        foreach (KeyValuePair<string,Tile> tile in Global.Instance.Tiles)
		{
			DebugTile newTile = (DebugTile)tileIns.Instantiate();
			newTile.tile=tile.Value;
			AddChild(newTile);
		}
	}
}
