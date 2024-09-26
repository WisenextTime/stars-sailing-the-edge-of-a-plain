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

public partial class MapEditor : Control
{


	Camera2D camera;
	public Dictionary map;
    private System.Collections.Generic.Dictionary<string,int> tileIndex = new();

	Sprite2D editorTile = new();
	TileMapLayer Tiles;
    //PackedScene editorTile;

    Array<int> size;
	Array<Dictionary> tiles;
	Vector2I TilePos;
    private OptionButton chooseTile;

	bool MousePressed = false;
    bool EraserPressed = false;

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
        size = (Array<int>)map["size"];
        tiles = (Array<Dictionary>)map["tiles"];
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

			chooseTile = GetNode<OptionButton>("UI/tool/chooseTile");
			chooseTile.AddIconItem(editorTile.Texture , tile.Key, id);

		}
        Tiles.TileSet.AddSource(tileSet, 0);
    }

	private void SetTiles()
	{
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

    public override void _UnhandledInput(InputEvent @event)
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
		else if(@event is InputEventMouseButton input2)
		{
			switch (input2.ButtonIndex)
			{
				case MouseButton.WheelUp: { camera.Zoom = (camera.Zoom.X >= 8) ? new Vector2(8, 8) : camera.Zoom += new Vector2(1,1); break; }
                case MouseButton.WheelDown: { camera.Zoom = (camera.Zoom.X <= 1/8) ? new Vector2(1,1) : camera.Zoom -= new Vector2(1, 1); break; }
                case MouseButton.Left: {MousePressed = input2.Pressed;break;}
                case MouseButton.Right: { EraserPressed = input2.Pressed; break; }
            }
			//GD.Print(input2.ButtonIndex);
		}
	}

    public override void _PhysicsProcess(double delta)
    {
		Vector2I pos = new Vector2I(Mathf.FloorToInt(GetGlobalMousePosition().X/32) , Mathf.FloorToInt(GetGlobalMousePosition().Y / 32));
		if (pos.X >= 0 && pos.X < size[0] && pos.Y >= 0 && pos.Y < size[1])
		{
			GetNode<Label>("UI/bar/info").Text = ($"({pos.X} , {pos.Y})--{tiles[pos.Y * size[0] + pos.X]["id"]}");
			TilePos = pos;
		}
		else
		{
			GetNode<Label>("UI/bar/info").Text = "";
			TilePos = new Vector2I(-1,-1);
		}

        if (TilePos.X != -1 && TilePos.Y != -1)
        {
            if (MousePressed)
            {
                tiles[TilePos.Y * size[0] + TilePos.X]["id"] = chooseTile.GetItemText(chooseTile.Selected);
                Tiles.SetCell(TilePos, 0, new Vector2I(0, 0), tileIndex[(string)tiles[TilePos.Y * size[0] + TilePos.X]["id"]]);
            }
			else if (EraserPressed)
			{
                tiles[TilePos.Y * size[0] + TilePos.X]["id"] = "air";
                Tiles.SetCell(TilePos, 0, new Vector2I(0, 0), tileIndex["air"]);
            }
        }

    }
}
