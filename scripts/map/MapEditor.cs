using Godot;
using Godot.Collections;
using starsailing;
using starsailing.lib;
using System.Collections.Generic;

public partial class MapEditor : Control
{
	Dictionary map;
	PackedScene editorTile = ResourceLoader.Load<PackedScene>("res://scenes/prefab/editor_tile.tscn");
	public override void _Ready()
	{
		if (IsInGroup("DEBUG"))
		{
			map = MapParser.ParseMap("res://assets/maps/TestMapLarge.json").Duplicate();
			DrawMap();
		}
	}

	private void DrawMap()
	{
		Array<int> size = (Array<int>)map["size"];
		Array<Dictionary> tiles = (Array<Dictionary>)map["tiles"];

		for (int x = 0; x < size[0]; x++)
		{
			for (int y = 0; y < size[1]; y++)
			{
				SetTile(new Vector2I(x, y), Global.Instance.Tiles[(string)tiles[x * size[1] + y]["id"]].image);
			}
		}
	}
	private void SetTile(Vector2I position, string image)
	{
		Sprite2D now_tile = (Sprite2D)editorTile.Instantiate();
		now_tile.Texture = starsailing.canvas.Image.LoadImage(image, "tile");
		now_tile.GlobalPosition=position*16;
		AddChild(now_tile);
	}
}
