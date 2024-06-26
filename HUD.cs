using FireFightGodot;
using Godot;
using System;

public partial class hud : CanvasLayer
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //CurrentSoldierNode = GetNode<player>("/root/Main/Player");
        //CurrentSoldierNode = StoredData.Soldiers[0];
        //CurrentSoldierNode = StoredData.Soldiers[0];
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void _on_n_pressed()
    {
        System.Diagnostics.Debug.Print("n");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveN);
    }

    private void _on_nw_pressed()
    {
        System.Diagnostics.Debug.Print("nw");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveNW);
    }

    private void _on_ne_pressed()
    {
        System.Diagnostics.Debug.Print("ne");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveNE);
    }

    private void _on_e_pressed()
    {
        System.Diagnostics.Debug.Print("e");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveE);
    }

    private void _on_se_pressed()
    {
        System.Diagnostics.Debug.Print("se");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveSE);
    }

    private void _on_s_pressed()
    {
        System.Diagnostics.Debug.Print("s");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveS);
    }

    private void _on_sw_pressed()
    {
        System.Diagnostics.Debug.Print("sw");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveSW);
    }

    private void _on_w_pressed()
    {
        System.Diagnostics.Debug.Print("w");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.MoveW);
    }

    private void _on_fire_pressed()
    {
        System.Diagnostics.Debug.Print("fire");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.FireSingle);
    }

    private void _on_reload_pressed()
    {
        System.Diagnostics.Debug.Print("reload");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.Reload);
    }

    private void _on_target_pressed()
    {
        System.Diagnostics.Debug.Print("target");
        //PlayerNode.Character.CurrentTarget = GetNode<player>("/root/Main/Target").Character;
        //CurrentSoldierNode.Character.CurrentTarget = GetNode<Target>("/root/Main/Target").Character;
    }

    private void _on_aim_pressed()
    {
        System.Diagnostics.Debug.Print("aim");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.Aim);
    }

    private void _on_end_turn_pressed()
    {
        StoredData.CurrentSoldierNode.Character.DoAllActions();
        System.Diagnostics.Debug.Print("end turn ran");

        StoredData.CurrentSoldierNode.GlobalPosition = new Vector2(StoredData.CurrentSoldierNode.Character.Xpos, StoredData.CurrentSoldierNode.Character.Ypos);
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Clear();
    }
}