using FireFight.Classes;
using FireFightGodot;
using Godot;
using System;
using System.Linq;

public partial class main : Node
{
	private PackedScene Soldier = (PackedScene)GD.Load("res://Units/Soldier.tscn");
	private Camera2D camera;
	private Vector2 cameraSpeed = new Vector2(500, 500);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CreateSoliders();
		//CreateTerrain();
		camera = GetNode<Camera2D>("MainViewPoint");
	}

	private void CreateSoliders()
	{
		for (uint i = 1; i < StoredData.NumberOfSoldiers + 1; i++)
		{
			Node CurrentSoldier = Soldier.Instantiate();

			AddChild(CurrentSoldier);

			Soldier SoldierInstance = (Soldier)CurrentSoldier;
			StoredData.Soldiers.Add(SoldierInstance);
			SoldierInstance.Character.Xpos = (200 * i);

			SoldierInstance.Character.Ypos = (200 * i);
			SoldierInstance.GlobalPosition = new Vector2(SoldierInstance.Character.Xpos, SoldierInstance.Character.Ypos);
			SoldierInstance.Character.Facing = FireFight.Functions.GeneralFunctions.CardinalDirectionsSquare.North;
			SoldierInstance.Character.Sidereference = i;
		}

		StoredData.Soldiers = StoredData.Soldiers.OrderByDescending(x => x.Character.INTSkillFactor).ToList();
		StoredData.CurrentSoldierNode = StoredData.Soldiers[0];
		StoredData.CurrentSoldierNode.Character.Selected = true;

		((Sprite2D)StoredData.CurrentSoldierNode.FindChild("Selected")).Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ScrollingFunctions(delta);
	}

	private void ScrollingFunctions(double delta)
	{
		Vector2 movement = new Vector2();

		if (Input.IsActionPressed("ui_right"))
		{
			movement.X += 1;
		}
		if (Input.IsActionPressed("ui_left"))
		{
			movement.X -= 1;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			movement.Y += 1;
		}
		if (Input.IsActionPressed("ui_up"))
		{
			movement.Y -= 1;
		}

		camera.Position += movement * cameraSpeed * new Vector2((float)delta, (float)delta);
	}
}
