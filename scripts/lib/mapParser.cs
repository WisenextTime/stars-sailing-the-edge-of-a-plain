using Godot;
using Godot.Collections;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Xml;

namespace starsailing.lib;

public partial class MapParser
{
	public static Dictionary ParseMap(string file)
	{
		Json source = ResourceLoader.Load<Json>(file);
		Dictionary map = (Dictionary)source.Data;
		return map;
	}
	public static Dictionary NewBlankMap(string name, string author, Vector2I size = new Vector2I(), string type = "tilemap")
	{
		Dictionary map = new() { ["name"] = name, ["author"] = author, ["type"] = type };
		if (type == "tilemap")
		{
			Array _size = new() { size.X, size.Y };
			map["size"] = _size;
			Array _tile = new();
			for (int i = 0; i < size.X * size.Y; i++)
			{
				Dictionary tile = new() { ["id"] = "grass", ["height"] = 0 };
				_tile.Add(tile);
			}
			map["tiles"] = _tile.Duplicate();
			map["item"] = (Array)new();
		}
		else
		{

		}
		return map;

	}
}