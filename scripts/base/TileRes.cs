using Godot;
using System;
using System.Collections;

public partial class TileRes : Resource
{
    [ExportGroup("core", "Core_")]
    [Export] public string Core_id = "";
    [Export] public string Core_name = "";
    [Export] public string Core_type = "ground";
    [ExportGroup("image", "Image_")]
    [Export] public Texture2D Image_iamge;
    [Export] public Vector2 Image_size = new(16, 16);
    [ExportGroup("layer", "Layer_")]
    [Export(PropertyHint.Flags,"under,land,low_atitude,high_atitude,SEA,GROUND,AIR,BUILDING")] public uint Layer_physics{get;set;} = 0;
    [Export(PropertyHint.Flags, "ground,lava,water,waterbridge,ice,cliff_1,cliff_2,cliff_3,cliff_4,void")] public uint Layer_agent { get; set; } = 0;
    [Export] public bool Layer_avoid;
}
