using FireFightGodot.Terrain;
using Godot;
using System;

public partial class BlockingTerrain : BaseTerrain
{
	public BlockingTerrain(Height height, LineofSight lineOfSight) : base(height, lineOfSight)
	{
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		height = Height.Blocking;
		lineOfSight = LineofSight.Blocking;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
