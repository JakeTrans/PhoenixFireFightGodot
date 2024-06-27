using FireFightGodot;
using Godot;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;

public partial class hud : CanvasLayer
{
    private List<Soldier> TargetList;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
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

        TargetList = StoredData.Soldiers.Where(x => x.Character.Selected == false).OrderBy(x => x.Character.INTSkillFactor).ToList();

        Soldier soldier = null;

        if (StoredData.CurrentSoldierNode.Character.CurrentTarget != null)
        {
            //clear current target

            Soldier oldTarget = StoredData.Soldiers.Where(x => x.Character == StoredData.CurrentSoldierNode.Character.CurrentTarget).First();

            Sprite2D resetSprite = (Sprite2D)oldTarget.GetChild(0);
            resetSprite.Texture = oldTarget.DefaultSprite;

            //set new target
            int oldindex = -1;

            for (int i = 0; i < TargetList.Count; i++)
            {
                if (TargetList[i].Character.Name == StoredData.CurrentSoldierNode.Character.CurrentTarget.Name)
                {
                    oldindex = i;
                    break;
                }
            }

            if (oldindex == -1)
            {
                StoredData.CurrentSoldierNode.Character.CurrentTarget = TargetList[0].Character;
                soldier = TargetList[0];
            }

            if (oldindex + 1 < TargetList.Count)
            {
                StoredData.CurrentSoldierNode.Character.CurrentTarget = TargetList[oldindex + 1].Character;
                soldier = TargetList[oldindex + 1];
            }
            else
            {
                StoredData.CurrentSoldierNode.Character.CurrentTarget = TargetList[0].Character;
                soldier = TargetList[0];
            }
        }
        else
        {
            StoredData.CurrentSoldierNode.Character.CurrentTarget = TargetList[0].Character;
            soldier = TargetList[0];
        }
        //set new sprite
        Sprite2D TargetSprite = (Sprite2D)soldier.GetChild(0);
        TargetSprite.Texture = soldier.TargetedSprite;
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