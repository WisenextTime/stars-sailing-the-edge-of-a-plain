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

	static List<Button> Buttons = new();

	Camera2D camera;
	public Dictionary map;
    private System.Collections.Generic.Dictionary<string,int> tileIndex = new();

	Sprite2D editorTile = new();
	TileMapLayer Tiles;
    TileMapLayer TileHeight;
    //PackedScene editorTile;

    Array<int> size;
	Array<Dictionary> tiles;
	Vector2I TilePos;
    private OptionButton chooseTile;

	bool MousePressed = false;
    bool EraserPressed = false;
	bool MouseReleased = true;


	Vector2 MousePosition = new();

    public override void _Ready()
	{
		camera = GetNode<Camera2D>("camera");
		Tiles = GetNode<TileMapLayer>("tiles");
        TileHeight = GetNode<TileMapLayer>("tileHeight");
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
		GetButtons();
        SetTiles();

        camera.GlobalPosition = new Vector2(size[0]*16, size[1] * 16);
    }

	private void GetButtons()
	{
		foreach(Button button in GetNode("UI/tool/pens").GetChildren())
		{
			Buttons.Add(button);
		}
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
			chooseTile.AddIconItem(editorTile.Texture , tile.Value.name, id);

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
                TileHeight.SetCell(new Vector2I(x, y), 0, new Vector2I(0, (int)tiles[y * size[0] + x]["height"]));
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
                case Key.Space:
                    {
                        camera.GlobalPosition = new Vector2(size[0] * 16, size[1] * 16);
                        break;
                    }
            }
		}
		else if (@event is InputEventMouseButton input2)
		{
			switch (input2.ButtonIndex)
			{
				case MouseButton.WheelUp: { camera.Zoom = (camera.Zoom.X >= 8) ? new Vector2(8, 8) : camera.Zoom += new Vector2(1, 1); break; }
				case MouseButton.WheelDown: { camera.Zoom = (camera.Zoom.X <= 1 / 8) ? new Vector2(1, 1) : camera.Zoom -= new Vector2(1, 1); break; }
				case MouseButton.Left: 
					{
						MousePressed = input2.Pressed;
						if (!input2.Pressed) { MouseReleased = true; }
						break;
					}
				case MouseButton.Right: { EraserPressed = input2.Pressed; break; }
			}
		}
		else if (@event is InputEventMouseMotion input3)
		{
			MousePosition = input3.Relative;
		}
	}

	public override async void _PhysicsProcess(double delta)
	{
		Vector2I pos = new(Mathf.FloorToInt(GetGlobalMousePosition().X / 32), Mathf.FloorToInt(GetGlobalMousePosition().Y / 32));
		if (pos.X >= 0 && pos.X < size[0] && pos.Y >= 0 && pos.Y < size[1])
		{
			GetNode<Label>("UI/bar/info").Text = ($"({pos.X} , {pos.Y}) - {Global.Instance.Tiles[(string)tiles[pos.Y * size[0] + pos.X]["id"]].name} - {tiles[pos.Y * size[0] + pos.X]["height"]}");
			TilePos = pos;
		}
		else
		{
			GetNode<Label>("UI/bar/info").Text = "";
			TilePos = new Vector2I(-1, -1);
		}
		TileOperation();
		TileHeight.Visible = GetNode<CheckButton>("UI/tool/HeightMap").ButtonPressed;
	}

	private void TileOperation() 
	{ 

        if (TilePos.X != -1 && TilePos.Y != -1)
        {
			if (MousePressed)
			{
				if (Buttons[0].ButtonPressed)
				{
                    camera.GlobalPosition -= MousePosition / camera.Zoom.X * 2f;
					MousePosition = new();
				}
				else if (Buttons[1].ButtonPressed)
				{
					foreach (string id in tileIndex.Keys)
					{
						if (tileIndex[id] == chooseTile.GetItemId(chooseTile.Selected))
						{
							tiles[TilePos.Y * size[0] + TilePos.X]["id"] = id;
						}

					}
					Tiles.CallDeferred(TileMapLayer.MethodName.SetCell, TilePos, 0, new Vector2I(0, 0), chooseTile.GetItemId(chooseTile.Selected));
				} 
				else if (Buttons[2].ButtonPressed)
				{
					Tiles.CallDeferred(TileMapLayer.MethodName.SetCell,TilePos, 0, new Vector2I(0, 0), tileIndex["air"]);
					tiles[TilePos.Y * size[0] + TilePos.X]["id"] = "air";
                }
                else if (Buttons[3].ButtonPressed)
				{
					chooseTile.Select(tileIndex[(string)tiles[TilePos.Y * size[0] + TilePos.X]["id"]] - 1);

                }
                else if (Buttons[4].ButtonPressed && MouseReleased)
                {
                    tiles[TilePos.Y * size[0] + TilePos.X]["height"] = Math.Min(9 , (int)tiles[TilePos.Y * size[0] + TilePos.X]["height"] + 1);
					MouseReleased = false;
                    TileHeight.CallDeferred(TileMapLayer.MethodName.SetCell, TilePos, 0, new Vector2I(0, (int)tiles[TilePos.Y * size[0] + TilePos.X]["height"]));
                }
                else if (Buttons[5].ButtonPressed && MouseReleased)
                {
                    tiles[TilePos.Y * size[0] + TilePos.X]["height"] = Math.Max(0 , (int)tiles[TilePos.Y * size[0] + TilePos.X]["height"] - 1);
					MouseReleased = false;
                    TileHeight.CallDeferred(TileMapLayer.MethodName.SetCell, TilePos, 0, new Vector2I(0, (int)tiles[TilePos.Y * size[0] + TilePos.X]["height"]));
                }
            }
        }

    }
}
