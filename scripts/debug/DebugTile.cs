using Godot;
using starsailing.core;
using starsailing.canvas;
using System;

public partial class DebugTile : Control
{
	public Tile tile;
	private RichTextLabel INFO;
	public override void _Ready()
	{
		GetNode<Label>("name").Text = tile.name;
		GetNode<Sprite2D>("image").Texture = starsailing.canvas.Image.LoadImage(tile.image,"tile");
		INFO=GetNode<RichTextLabel>("INFO");
		INFO.Text += "\nid : "+tile.id;
		INFO.Text += "\nname : " + tile.name;
		INFO.Text += "\nimage : " + tile.image;
		INFO.Text += "\nmove type : " + tile.moveType.ToString();
		INFO.Text += "\nsize : " + tile.size.ToString();
		INFO.Text += "\nrough : " + tile.rough.ToString();
		INFO.Text += "\ntemperature : " + tile.temperature.ToString();
		INFO.Text += "\nhumidity : " + tile.humidity.ToString();
	}
	public void _on_mouse_entered()
	{
		INFO.Visible = true;
	}
	public void _on_mouse_exited()
	{
		INFO.Visible = false;
	}
}
