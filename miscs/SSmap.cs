using Godot;
using Godot.Collections;
using System;
using System.Collections;

public partial class SSmap : Json
{
    public Vector2 Size;
    public string Author;
    public string[] GROUND;
    public int[] PHYSICS;
    public int[] AGENT;
    public int[] AVOID;
    public int[] HEIGHT;
    public Dictionary OBJECT;
    public SSmap(string file_path){
        if(DirAccess.Open(file_path.GetBaseDir()).FileExists(file_path)){
            Data=FileAccess.Open(file_path,FileAccess.ModeFlags.Read).GetAsText();
            Dictionary contains=(Dictionary)Data;
            Size = (Vector2)contains["size"];
            Author = (string)contains["author"];
            contains = (Dictionary)contains["contains"];
            GROUND = (string[])contains["GROUND"];
            PHYSICS = (int[])contains["PHYSICS"];
            AGENT = (int[])contains["AGENT"];
            HEIGHT = (int[])contains["HEIGHT"];
            AVOID = (int[])contains["AVOID"];
            OBJECT = (Dictionary)contains["OBJECT"];
        }
    }
}
