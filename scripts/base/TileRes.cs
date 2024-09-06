using Godot;
using System;
using System.Collections;

public partial class TileRes : Resource
{
    [ExportGroup("core","Core_")]
    [Export]public string Core_id="";
    [Export]public string Core_name="";
    [Export]public string Core_type="ground";
    [ExportGroup("image","Image_")]
    [Export]public string Image_iamge="";
    [Export]public Vector2 Image_size=new(16,16);
    [ExportGroup("layer","Layer_")]
    [Export]public int Layer_physics=0;
    [Export]public int Layer_agent=0;
    [Export]public int Layer_avoid=0;
}
