using Godot;
using System;

public partial class MainMenu : ColorRect
{
    public override void _PhysicsProcess(double _delta){
        
    }
private void _on_button_4_pressed(){
	GetTree().ChangeSceneToFile("res://scenes/map_list.tscn");
	}
private void _on_button_5_pressed(){
	GetTree().Quit();
	}
}
