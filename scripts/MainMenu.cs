using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnQuitPressed()
	{
		GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
		GetTree().Quit();
	}
	public void OnMapEditorPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/editor_map_list.tscn");
	}
}
