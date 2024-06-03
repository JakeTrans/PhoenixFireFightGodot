using FireFight.CharacterObjects;
using Godot;
using System;

public partial class player : Area2D
{
	// Called when the node enters the scene tree for the first time.
	private Character Character { get; set; }

	public override void _Ready()
	{
		Character = new Character(7, 0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
