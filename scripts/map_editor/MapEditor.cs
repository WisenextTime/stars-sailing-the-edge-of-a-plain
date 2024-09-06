using Godot;
using System;

public partial class MapEditor : Node2D
{
	public Json MapData;
    public override void _Ready()
    {
       if(MapData==null){
		throwError(1);
		return;
	   }
    }

	private void throwError(int error){
		if(error==1){
			
		}
	}
}
