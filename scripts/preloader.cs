using Godot;
using System;
using starsailing.conten;
using starsailing.lib;
using starsailing.core;

namespace starsailing;

public partial class Preloader : Control
{
	Label info;
	private void Preload()
	{
		info.Text += "\nLoading tiles...";
		Tiles.PreloadTile();
		info.Text += "\nLoading items...";
		Items.PreloadItems();
	}

	public override void _Ready()
	{
		info = GetNode<Label>("background/info");
		Preload();

        //GetTree().ChangeSceneToFile("res://scenes/debug/object debug.tscn");
        //TileDebug

        //GetTree().ChangeSceneToFile("res://scenes/debug/debug_new_map.tscn");
        //GetTree().ChangeSceneToFile("res://scenes/debug/map debug.tscn");
        Global.Instance.GameMap = "res://assets/maps/TestMap.json";
        GetTree().ChangeSceneToFile("res://scenes/map_editor.tscn");
        //GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
        //TileMapDebug
    }
}
