using Godot;
using System;
using starsailing.lib;
using starsailing;
using Godot.NativeInterop;
using System.Collections.Generic;

namespace starsailing.core;


public partial class Mapobject : Resource
{
    public List<Vector2> Collision = new();
    public MoveType moveType = MoveType.none;
    public enum ObjectType{terrain,trigger}
    //terrain 地形,用于判定该区域所属地形
    //trigger 触发,用于触发某种事件
    public ObjectType objectType;
    public string ObjectName;
    public Mapobject(string name,ObjectType type = ObjectType.terrain)
    {
        ObjectName = name;
        objectType = type;
    }
}