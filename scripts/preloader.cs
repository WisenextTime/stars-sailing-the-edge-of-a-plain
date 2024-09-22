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
	}
}
