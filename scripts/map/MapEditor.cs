using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using starsailing;
using starsailing.core;
using starsailing.lib;
using starsailing.canvas;
using System.Reflection.PortableExecutable;
using starsailing.conten;

public partial class MapEditor : Node2D
{


	Camera2D camera;
	public Dictionary map;
    private System.Collections.Generic.Dictionary<string,int> tileIndex = new();

	Sprite2D editorTile = new();
	TileMapLayer Tiles;
	//PackedScene editorTile;

	public override void _Ready()
	{
		camera = GetNode<Camera2D>("camera");
		Tiles = GetNode<TileMapLayer>("tiles");
		//editorTile = ResourceLoader.Load<PackedScene>("res://scenes/prefab/editor_tile.tscn");
		editorTile.Name = "editorTile";
		GetTiles();
		if (IsInGroup("DEBUG"))
		{
			map = MapParser.ParseMap("res://assets/maps/TestMap.json").Duplicate();
			//map = MapParser.ParseMap("res://assets/maps/TestMapLarge.json").Duplicate();
		}
		else
		{
			map = MapParser.ParseMap(Global.Instance.GameMap);
		}
		SetTiles();
	}

	private void GetTiles()
	{
        var tileSet = new TileSetScenesCollectionSource();
        foreach (KeyValuePair<string, Tile> tile in Global.Instance.Tiles)
		{
			editorTile.Texture = starsailing.canvas.Image.LoadImage(Global.Instance.Tiles[tile.Key].image,"tile");
			PackedScene nowTile = new();
			nowTile.Pack(editorTile);			
			var id = tileSet.CreateSceneTile(nowTile, tileSet.GetNextSceneTileId());			
			tileIndex[tile.Key] = id;
		}
        Tiles.TileSet.AddSource(tileSet, 0);
    }

	private void SetTiles()
	{
		Array<int> size = (Array<int>)map["size"];
		Array<Dictionary> tiles = (Array<Dictionary>)map["tiles"];
		for (int i = 0; i < Math.Ceiling(size[0] / 10.0); i++)
		{
			for (int j = 0; j < Math.Ceiling(size[1] / 10.0); j++)
			{
                //GD.Print("Chunk " + GD.VarToStr(new Vector2I(i, j)) + GD.VarToStr(new Vector2I(Math.Min(10, size[0] - i * 10), Math.Min(10, size[1] - j * 10))));
                SetChunkTile(new Vector2I(i * 10, j * 10), new Vector2I(Math.Min(10, size[0] - i * 10), Math.Min(10, size[1] - j * 10)), tiles,size);
            }
        }

	}

	private void SetChunkTile(Vector2I startPos,Vector2I chunkSize , Array<Dictionary> tiles , Array<int> size)
	{
        for (int x = startPos.X; x < chunkSize.X + startPos.X; x++)
        {
			for (int y = startPos.Y; y < chunkSize.Y + startPos.Y; y++)
			{
				Tiles.SetCell(new Vector2I(x, y), 0, new Vector2I(0, 0), tileIndex[(string)tiles[y*size[0]+x]["id"]]);
				//Tiles.SetCell(new Vector2I(startPos.X + x, startPos.Y + y), 0, new Vector2I(0, 0), tileIndex["grass"]);
				//GD.Print(GD.VarToStr(new Vector2I(x,y)));
            }
        }
    }

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey input)
		{
			var _carema_x = camera.GlobalPosition.X;
			var _carema_y = camera.GlobalPosition.Y;
			switch (input.Keycode)
			{
				case Key.Left:
					{
						camera.GlobalPosition = new Vector2(_carema_x - 10, _carema_y);
						break;
					}
				case Key.Right:
					{
						camera.GlobalPosition = new Vector2(_carema_x + 10, _carema_y);
						break;
					}
				case Key.Up:
					{
						camera.GlobalPosition = new Vector2(_carema_x, _carema_y - 10);
						break;
					}
				case Key.Down:
					{
						camera.GlobalPosition = new Vector2(_carema_x, _carema_y + 10);
						break;
					}
			}
		}
	}
}
