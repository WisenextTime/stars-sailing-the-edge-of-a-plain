using Godot;
using System;

namespace starsailing.lib;

public static class IndexImage
{
	public const string none = "index.none" , missing = "index.missing";
}
public static class ImageLayer
{
	public const int background = 0 , behindUnit = 1, withUnit = 2, beforeUnit = 3, UI = 4 , beforeUI = 5 , onTop = 6;
}