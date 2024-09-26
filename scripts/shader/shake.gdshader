shader_type canvas_item;
uniform float speed = 5.0;

void fragment() 
{
	if(COLOR != vec4(0,0,0,1))
	{
	COLOR.a = (sin(TIME*speed)+2.0)/3.0*COLOR.a;
	}
}
