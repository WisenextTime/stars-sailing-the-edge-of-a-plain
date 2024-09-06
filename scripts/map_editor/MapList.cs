using Godot;
using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public partial class MapList : ColorRect
{	private Window del_win;
	private Window new_win;
	private Godot.Collections.Dictionary maps;
	private int DeviceType;
	[Export]PackedScene mapButton;
	public string Now_map{set;get;}="";
	public string Now_name{set;get;}="";
	private string new_map_name;
	private string map_dir;
	private void _on_close_pressed(){
		GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
	}
    public override void _Ready()
    {
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
			DeviceType = 0;
			map_dir="C://StarsSailing/maps/";
		}else{
			DeviceType = 1;
		}
		ReloadMap();
		del_win = GetNode<Window>("delete");
		new_win = GetNode<Window>("new");
    }
	private void LoadMapFiles(string dir_path){
		DirAccess dir = DirAccess.Open(dir_path);
		GD.Print(dir);
		foreach(var file in dir.GetFiles()){
			if (file.GetExtension()=="ssmap"){
				maps.Add(file,dir_path+file);
			}
		}
	}

	private void ReloadMap(){
		maps=new();
		foreach(var map_button in GetTree().GetNodesInGroup("mapButton")){
			map_button.QueueFree();
		}
		LoadMapFiles(map_dir);
		ViewMaps();
	}

	private void ViewMaps(){
		foreach(var (name, map) in maps){
			var map_instantiate = mapButton.Instantiate<MapButton>();
			map_instantiate.Sname = map_instantiate.Text=name.ToString().GetBaseName();
			map_instantiate.Smap = map.ToString();
			GetNode("list/list").AddChild(map_instantiate);
		}
	}
    public override void _PhysicsProcess(double delta)
    {
        if (Now_map!=""){
			GetNode<RichTextLabel>("info").Text="[font size=30]Path : "+Now_map.ToString()+"[/font]\n";
			GetNode<Button>("oprationButton/Button1").Disabled=false;
			GetNode<Button>("oprationButton/Button2").Disabled=false;
			//GetNode<Button>("opretionButton/Button3").Disabled=false;
		}else{
			GetNode<RichTextLabel>("info").Text="";
			GetNode<Button>("oprationButton/Button1").Disabled=true;
			GetNode<Button>("oprationButton/Button2").Disabled=true;
			//GetNode<Button>("opretionButton/Button3").Disabled=true;
		}
		new_map_name=GetNode<LineEdit>("new/name").Text;
		if(new_map_name==""){
			GetNode<Button>("new/OK").Disabled=true;
		}else{
			GetNode<Button>("new/OK").Disabled=false;
		}
    }
	public void _map_edit(){

	}

	public void _map_delete(){
		GetNode<ColorRect>("background2").Visible=true;
		del_win.Visible=true;
	}
	public void _map_delete_yes(){
		
		GetNode<ColorRect>("background2").Visible=false;
		del_win.Visible=false;
		DirAccess dir = DirAccess.Open(Now_map.GetBaseDir());
		dir.Remove(Now_map);
		ReloadMap();
		Now_map="";
		Now_name="";
	}
	public void _map_button_no(){
		
		GetNode<ColorRect>("background2").Visible=false;
		del_win.Visible=false;
		new_win.Visible=false;
	}

	public void _map_new(){
		GetNode<ColorRect>("background2").Visible=true;
		new_win.Visible=true;
	}
	public void _map_new_ok(){
		new_win.Visible=false;
		DirAccess dir = DirAccess.Open(map_dir);
		if(dir.FileExists(new_map_name+".ssmap")){
			Throw_error(1);
		}else{
			GetNode<ColorRect>("background2").Visible=false;
			using var new_map = Godot.FileAccess.Open(map_dir+new_map_name+".ssmap",Godot.FileAccess.ModeFlags.Write);
			new_map.StoreString("");
			Now_map="";
			Now_name="";
			ReloadMap();
		}

	}

	private void Throw_error(int type){
		if(type==1){
			GetNode<Window>("error1").Visible=true;
		}
	}

	public void _error_ok(){
		GetNode<ColorRect>("background2").Visible=false;
		GetNode<Window>("error1").Visible=false;
	}
	
}
