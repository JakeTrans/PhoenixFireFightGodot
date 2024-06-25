using FireFight.CharacterObjects;
using Godot;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

public partial class main : Node
{
    private PackedScene Soldier = (PackedScene)GD.Load("res://Soldier.tscn");

    public List<Node> Soldiers = new List<Node>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Node Soldier1 = Soldier.Instantiate();

        AddChild(Soldier1);

        Soldiers.Add(Soldier1);
        Soldier a = (Soldier)Soldier1;
        a.Character.Xpos = 200;

        a.Character.Ypos = 300;
        a.GlobalPosition = new Vector2(a.Character.Xpos, a.Character.Ypos);

        Node Soldier2 = Soldier.Instantiate();

        AddChild(Soldier2);

        Soldiers.Add(Soldier2);

        Soldier b = (Soldier)Soldier2;
        b.Character.Xpos = 300;

        b.Character.Ypos = 400;
        b.GlobalPosition = new Vector2(b.Character.Xpos, b.Character.Ypos);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}