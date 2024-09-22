using System;
using System.Data.Common;
using System.Text.Json.Serialization;
using Godot;

using starsailing.lib;

namespace starsailing.canvas;

public class Image
{
    public static ImageTexture LoadImage(string name , string type , string modPath="res:/")
    {
        string _path_dir = "null";
        string _path = "null";
        if (name == IndexImage.missing)
        {
            _path = "res://assets/images/missing.png";
        }
        else if(name == IndexImage.none)
        {
            _path = "res://assets/images/blank.png";
        }
        else
        {
            switch(type)
            {
                case "tile":
                {
                    _path_dir=modPath+"/assets/images/tile/";
                    break;
                }
                case "unit":
                {
                    _path_dir = modPath + "/assets/images/unit/";
                    break;
                }
                default:
                {
                    _path= "res://assets/images/missing.png";
                    break;
                }

            }
            if(_path=="null")
            {
                _path = _path_dir+name;
            }
        }
        return ImageTexture.CreateFromImage(Godot.Image.LoadFromFile(_path));
    }
}