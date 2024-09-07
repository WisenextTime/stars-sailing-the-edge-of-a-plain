using Godot;
using System;

public partial class MapEditor : Node2D
{
	public SSmap MapData;
	private PackedScene tile_res;

    public override void _Ready()
	{
		if (MapData == null && OS.IsDebugBuild())MapData=new("C://StarsSailing/maps/Test.ssmap");
		//Only Windows debug.
		if (MapData == null)
		{
			throwError(1);
			return;
		}
		tile_res = GD.Load<PackedScene>("res://scenes/res/tile.tscn");
		PrintMap();
	}

	private void throwError(int error)
	{
		if (error == 1)
		{
			GetTree().ChangeSceneToFile("res://scenes/map_list.tscn");
		}
	}
	private void PrintMap()
	{
		for(int y=0;y<MapData.Size.Y;y++){
			for (int x = 0; x < MapData.Size.X; x++){

			}
		}
	}
	private void place_tile(Vector2 pos){
		var now_tile = tile_res.Instantiate<EditorTile>();
        TileRes tile_res_data = new()
        {
            Core_id = MapData.GROUND[(int)(pos.Y * MapData.Size.X + pos.X)]
        };

    }
}
