using Godot;
using System;
using starsailing.lib;

namespace starsailing.core;


public partial class Item : Resource
{
    //Item仅作为为渲染层提供装饰的类,只有最基本的id,图像,图层和尺寸,并不包含物理,寻路等逻辑
    //做装饰物应优先考虑Item而不是Tile,因为Item消耗性能更少
    protected string id;
    public string name;
    public Vector2I size = new(16, 16);
    public string image = IndexImage.missing;
    public int layer = ImageLayer.beforeUnit;
    public Item(string id)
    {
        this.id = id;
    }
}