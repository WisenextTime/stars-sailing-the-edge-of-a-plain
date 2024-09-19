using Godot;
using System;

using starsailing.core;
using starsailing.lib;
using starsailing.conten;
using Microsoft.VisualBasic;

namespace starsailing;
public partial class Preloader : Control
{
    Label info;
    private   void Preload()
    {
        info.Text += "\nLoading tiles...";
        Tiles.PreloadTile();
        info.Text += "\nLoading items...";
        Items.PreloadItems();
    }

    public override void _Ready()
    {
        info = GetNode<Label>("background/info");
        Preload();
    }
}