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
public enum MoveType { none, ground, water, lava, forbid, waterbridge }
/*
用于判断地块行为
none 留空,没有物理碰撞,也无法寻路到达,*不建议使用*
ground 为地面,并且可以设置高度,作为最基本的地块
water 为水面,供水上单位行走
lava 为岩浆,不允许任何非空中单位通行
forbid 为不可通行的区域
waterbridge 为浅水,大部分地面大型单位和水上小型单位都可以通过
*/