//废弃

shader_type canvas_item;

void fragment()
{
	if(FRAGCOORD.y<250.0)
	{
		COLOR.a=(200.0-FRAGCOORD.y)/50.0;
	}
}