using Godot;
using System;
using starsailing.core;
using starsailing.lib;

namespace starsailing.conten;

class Items
{
    public static Item tree;
    public static void PreloadItems()
    {

        Global.Instance.Items.Add(tree = new Item("tree")
        {
            name = "tree",
        });
    }
}