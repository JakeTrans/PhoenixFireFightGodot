using Godot;
using System;

public partial class hud : CanvasLayer
{
	private player PlayerNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerNode = GetNode<player>("/root/Main/Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_n_pressed()
	{
		System.Diagnostics.Debug.Print("n");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveN);
	}

	private void _on_nw_pressed()
	{
		System.Diagnostics.Debug.Print("nw");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveNW);
	}

	private void _on_ne_pressed()
	{
		System.Diagnostics.Debug.Print("ne");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveNE);
	}

	private void _on_e_pressed()
	{
		System.Diagnostics.Debug.Print("e");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveE);
	}

	private void _on_se_pressed()
	{
		System.Diagnostics.Debug.Print("se");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveSE);
	}

	private void _on_s_pressed()
	{
		System.Diagnostics.Debug.Print("s");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveS);
	}

	private void _on_sw_pressed()
	{
		System.Diagnostics.Debug.Print("sw");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveSW);
	}

	private void _on_w_pressed()
	{
		System.Diagnostics.Debug.Print("w");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveW);
	}

	private void _on_fire_pressed()
	{
		System.Diagnostics.Debug.Print("fire");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.FireSingle);
	}

	private void _on_reload_pressed()
	{
		System.Diagnostics.Debug.Print("reload");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.Reload);
	}

	private void _on_target_pressed()
	{
		System.Diagnostics.Debug.Print("target");
		//PlayerNode.Character.CurrentTarget = GetNode<player>("/root/Main/Target").Character;
		PlayerNode.Character.CurrentTarget = GetNode<Target>("/root/Main/Target").Character;
	}

	private void _on_aim_pressed()
	{
		System.Diagnostics.Debug.Print("aim");
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.Aim);
	}

	private void _on_end_turn_pressed()
	{
		PlayerNode.Character.DoAllActions();
		System.Diagnostics.Debug.Print("end turn ran");

		PlayerNode.GlobalPosition = new Vector2(PlayerNode.Character.Xpos, PlayerNode.Character.Ypos);
		PlayerNode.Character.ActionsForTurn.ActionsTaken.Clear();
	}
}
