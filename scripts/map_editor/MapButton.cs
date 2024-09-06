using Godot;
using System;

public partial class MapButton : Button
{
	public string Sname{set;get;}
	public string Smap{set;get;}
	MapList parent;
    public override void _Ready()
    {
       parent=GetTree().Root.GetNode<MapList>("MapList");
    }
    private void _on_toggled(bool toggled_on){
		if (toggled_on){
			parent.Now_map = Smap;
			parent.Now_name = Sname;
		}

	}
}