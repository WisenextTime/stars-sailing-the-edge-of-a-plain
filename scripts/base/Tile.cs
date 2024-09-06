using Godot;
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;

public partial class EditorTile : StaticBody2D

{
	private string name;
    public string id;
    private string type;
    private Sprite2D node_image;
    private NavigationRegion2D node_agent;
    private NavigationObstacle2D node_avoid;
    public uint height = 0;
	 [Export]public TileRes tile_res;

    public override void _Ready()
    {
        node_image = GetNode<Sprite2D>("image");
        node_agent = GetNode<NavigationRegion2D>("agent");
        node_avoid = GetNode<NavigationObstacle2D>("avoid");
        preload();
    }
    private void preload(){
        tile_res ??= GD.Load<TileRes>("res://assests/tiles/TestTile.tres");
        name=tile_res.Core_name;
        id=tile_res.Core_id;
        type=tile_res.Core_type;
        node_image.Texture = tile_res.Image_iamge;
        node_image.Scale = tile_res.Image_size/16;
        node_image.Frame=4;
        CollisionLayer=tile_res.Layer_physics;
        node_agent.NavigationLayers=tile_res.Layer_agent;
        node_avoid.AvoidanceEnabled=tile_res.Layer_avoid;
    }
}
