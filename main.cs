using FireFight.CharacterObjects;
using FireFightGodot;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Formats.Asn1.AsnWriter;

public partial class main : Node
{
	private PackedScene Soldier = (PackedScene)GD.Load("res://Units/Soldier.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
		StoredData.CurrentSoldierNode = StoredData.Soldiers[0];
		StoredData.CurrentSoldierNode.Character.Selected = true;
		Sprite2D Sprite = (Sprite2D)StoredData.CurrentSoldierNode.GetChild(0);
		Sprite.Texture = StoredData.SelectedSprite;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
