using Godot;
using starsailing.core;
using starsailing.canvas;
using System;

public partial class DebugTile : Control
{
	public Tile tile;
	public override void _Ready()
	{
		GetNode<Label>("name").Text = tile.name;
		GetNode<Sprite2D>("image").Texture = starsailing.canvas.Image.LoadImage(tile.image,"tile");
	}
}
