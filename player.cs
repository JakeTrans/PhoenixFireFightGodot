using FireFight.CharacterObjects;
using Godot;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

public partial class player : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public Character Character { get; set; }

    public override void _Ready()
    {
        Character = new Character(7, 0);
        Character.Xpos = (uint)Position.X;
        Character.Ypos = (uint)Position.Y;
        Character.CurrentTarget = null;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}