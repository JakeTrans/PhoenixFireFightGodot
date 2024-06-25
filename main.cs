using FireFight.CharacterObjects;
using FireFightGodot;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Formats.Asn1.AsnWriter;

public partial class main : Node
{
    private PackedScene Soldier = (PackedScene)GD.Load("res://Soldier.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Node Soldier1 = Soldier.Instantiate();

        //AddChild(Soldier1);

        //Soldier a = (Soldier)Soldier1;
        //StoredData.Soldiers.Add(a);
        //a.Character.Xpos = 200;

        //a.Character.Ypos = 300;
        //a.GlobalPosition = new Vector2(a.Character.Xpos, a.Character.Ypos);

        //Node Soldier2 = Soldier.Instantiate();

        //AddChild(Soldier2);

        //Soldier b = (Soldier)Soldier2;
        //StoredData.Soldiers.Add(b);
        //b.Character.Xpos = 300;

        //b.Character.Ypos = 400;
        //b.GlobalPosition = new Vector2(b.Character.Xpos, b.Character.Ypos);

        for (uint i = 1; i < 5; i++)
        {
            Node CurrentSoldier = Soldier.Instantiate();

            AddChild(CurrentSoldier);

            Soldier SoldierInstance = (Soldier)CurrentSoldier;
            StoredData.Soldiers.Add(SoldierInstance);
            SoldierInstance.Character.Xpos = (100 * i);

            SoldierInstance.Character.Ypos = (100 * i);
            SoldierInstance.GlobalPosition = new Vector2(SoldierInstance.Character.Xpos, SoldierInstance.Character.Ypos);
        }

        StoredData.Soldiers = StoredData.Soldiers.OrderByDescending(x => x.Character.INTSkillFactor).ToList();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}