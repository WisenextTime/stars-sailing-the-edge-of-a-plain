using Godot;
using starsailing.lib;
using System;
using starsailing;

public partial class EditorMapList : Control
{
	PackedScene ListButton;

	public string Map = "";

	Popup NewMap;
    public override void _Ready()
	{
		NewMap = GetNode<Popup>("operations/NEW/NewMap");
        ListButton = ResourceLoader.Load<PackedScene>("res://scenes/prefab/ListButton.tscn");
		LoadMapFrom("res://assets/maps/");
		if (Global.Instance.OStype == 0)
		{
			LoadMapFrom("C://StarsSailing/maps/");
		}
    }

	private void LoadMapFrom(string path)
	{
		foreach(var file in DirAccess.Open(path).GetFiles())
		{
			if(file.GetExtension() == "json")
			{
				Button button = ListButton.Instantiate<Button>();
				button.Text = (string)MapParser.ParseMap(path+file)["name"];
				button.TooltipText = path + file;
				GetNode("List/List").AddChild(button);
			}
		}
	}

    public override void _PhysicsProcess(double delta)
    {
		if (Map != "")
		{
			GetNode<Button>("operations/DEL").Disabled = false;
		}
		else
		{
            GetNode<Button>("operations/DEL").Disabled = true;
        }
        NewMap.GetNode<Button>("Button_OK").Disabled = NewMap.GetNode<LineEdit>("Name").Text == "";
    }

	public void OnBackPressed()
	{
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }
	public void OnNewPressed()
	{
		NewMap.Popup();
		NewMap.GetNode<LineEdit>("Name").Text = "";
	}
	public void OnButtonNewMapCancel()
	{
		NewMap.Visible = false;
    }
    public void OnButtonNewMapOK()
    {
        NewMap.Visible = false;
    }
}
