using Godot;
using System;
using FireFight;
using FireFight.CharacterObjects;

public partial class Target : Node
{
	// Called when the node enters the scene tree for the first time.
	private Character Character { get; set; }

	public override void _Ready()
	{
		Character = new Character(7, 1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
