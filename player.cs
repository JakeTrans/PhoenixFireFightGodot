using FireFight.CharacterObjects;
using Godot;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

public partial class player : Area2D
{
    // Called when the node enters the scene tree for the first time.
    public Character Character { get; set; }

    public override void _Ready()
    {
        //D:\\Git\\FireFightGodot\\Data\\

        var a = Path.GetDirectoryName(Assembly.GetAssembly(GetType()).Location);

        Character = new Character(7, 0);
        //Character.Xpos = (uint)Position.X;
        //Character.Ypos = (uint)Position.Y;

        //Target targetnode = (Target)GetNode<Area2D>("Target");

        //Character.CurrentTarget = targetnode.Character;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}