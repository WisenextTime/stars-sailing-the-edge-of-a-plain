using Godot;
using System;
using starsailing.lib;
using Godot.Collections;
public partial class DebugNewMap : Control
{
	public override void _Ready()
	{
		Dictionary map = MapParser.NewBlankMap("TestMap","WisenextTime",new Vector2I(100,100));
		Json map_file = new();
		map_file.Parse(Json.Stringify(map));
		ResourceSaver.Save(map_file , "res://assets/maps/TestMapLarge.json");
	}
}

