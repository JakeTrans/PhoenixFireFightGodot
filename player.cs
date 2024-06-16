using FireFight.CharacterObjects;
using FireFight.Classes;
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
		Character.MapScale = 100;
		Character.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
		Character.RangedWeapons[0].Equipped = true;
		Character.CurrentAimAmount = 20;

		//

		//Character.FireFunction();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Node2D targetNode = GetNode<Node2D>("/root/Main/Target");
		//Target targetObject = (Target)targetNode;
		//Character.CurrentTarget = targetObject.Character;
		//Character.FireFunction();
	}
}
