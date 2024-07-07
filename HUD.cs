using FireFightGodot;
using Godot;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using System.Security.AccessControl;
using FireFight.Classes;
using Azure.Identity;

public partial class hud : CanvasLayer
{
    private List<Soldier> TargetList;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Load and show the PopupScene
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        setupHud();
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

    private void _on_anti_clockwise_pressed()
    {
        // Replace with function body.
        System.Diagnostics.Debug.Print("ACW");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.RotateAntiClockWise);
    }

    private void _on_clockwise_pressed()
    {
        // Replace with function body.
        System.Diagnostics.Debug.Print("CS");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.RotateClockwise);
    }

    private void _on_fire_pressed()
    {
        if (StoredData.CurrentSoldierNode.Character.CurrentTarget == null)
        {
            System.Diagnostics.Debug.Print("no target");
            LoadPopup("No Target Selected");
            return;
        }

        System.Diagnostics.Debug.Print("fire");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.FireSingle);
    }

    private void _on_reload_pressed()
    {
        //LoadPopup("Reload");
        System.Diagnostics.Debug.Print("reload");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.Reload);
    }

    private void _on_target_pressed()
    {
        System.Diagnostics.Debug.Print("target");

        //TargetList = StoredData.Soldiers.Where(x => x.Character.Selected == false).OrderBy(x => x.Character.INTSkillFactor).ToList();
        TargetList = StoredData.Soldiers.Where(x => x.Character.Selected == false && x.Character.KnockedOut == false).OrderBy(x => x.Character.INTSkillFactor).ToList();

        Soldier soldier = null;

        if (StoredData.CurrentSoldierNode.Character.CurrentTarget != null)
        {
            //clear current target

            Soldier oldTarget = StoredData.Soldiers.Where(x => x.Character == StoredData.CurrentSoldierNode.Character.CurrentTarget).First();

            Sprite2D resetSprite = (Sprite2D)oldTarget.GetChild(0);
            resetSprite.Texture = StoredData.DefaultSprite;

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
        TargetSprite.Texture = StoredData.TargetedSprite;
    }

    private void _on_aim_pressed()
    {
        System.Diagnostics.Debug.Print("aim");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(FireFight.Classes.ActionsPossible.Aim);
    }

    private void _on_end_turn_pressed()
    {
        //Take Actions
        System.Diagnostics.Debug.Print("end turn ran");
        //StoredData.CurrentSoldierNode.Character.DoAllActions();

        // Loop through StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken

        foreach (FireFight.Classes.ActionsPossible action in StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken)
        {
            System.Diagnostics.Debug.Print(action.ToString());

            if (action == FireFight.Classes.ActionsPossible.FireSingle || action == FireFight.Classes.ActionsPossible.FireBurst)
            {
                DamageResult Result = StoredData.CurrentSoldierNode.Character.DoAction(action);
                if (Result != null)
                {
                    if (Result.Disabling == false)
                    {
                        LoadPopup("Shot Taken -  Hit the for " + Result.HitLocation.Trim() + " for " + Result.DamageAmount);
                    }
                    else
                    {
                        LoadPopup("Shot Taken -  Hit the for " + Result.HitLocation.Trim() + " for " + Result.DamageAmount + " was disabling");
                    }
                }
                else
                {
                    LoadPopup("Shot Taken - Missed");
                }
            }
            else
            {
                StoredData.CurrentSoldierNode.Character.DoAction(action);
            }
        }

        //Check for end of game
        //TODO: Check for end of game

        StoredData.CurrentSoldierNode.GlobalPosition = new Vector2(StoredData.CurrentSoldierNode.Character.Xpos, StoredData.CurrentSoldierNode.Character.Ypos);

        StoredData.CurrentSoldierNode.GlobalRotationDegrees = StoredData.CurrentSoldierNode.Character.GetRotation();

        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Clear();

        List<Soldier> FilteredList = StoredData.Soldiers.Where(x => x.Character.KnockedOut == false).OrderBy(x => x.Character.INTSkillFactor).ToList();

        int oldindex = -1;
        for (int i = 0; i < FilteredList.Count; i++)
        {
            if (FilteredList[i].Character.Name == StoredData.CurrentSoldierNode.Character.Name)
            {
                oldindex = i;
                break;
            }
        }

        //set new sprite

        Sprite2D DeselectedSprite = (Sprite2D)FilteredList[oldindex].GetChild(0);
        DeselectedSprite.Texture = StoredData.DefaultSprite;
        StoredData.CurrentSoldierNode.Character.Selected = false;
        //Next Characters turn
        if (oldindex + 1 < FilteredList.Count)
        {
            Sprite2D SelectedSprite = (Sprite2D)FilteredList[oldindex + 1].GetChild(0);
            SelectedSprite.Texture = StoredData.SelectedSprite;
            StoredData.CurrentSoldierNode = FilteredList[oldindex + 1];
            StoredData.CurrentSoldierNode.Character.Selected = true;
        }
        else
        {
            Sprite2D SelectedSprite = (Sprite2D)FilteredList[0].GetChild(0);
            SelectedSprite.Texture = StoredData.SelectedSprite;
            StoredData.CurrentSoldierNode = FilteredList[0];
            StoredData.CurrentSoldierNode.Character.Selected = true;
        }
    }

    private void setupHud()
    {
        // Sprite2D DeselectedSprite = (Sprite2D)StoredData.Soldiers[oldindex].GetChild(0);

        RichTextLabel WeaponName = (RichTextLabel)GetNode("WeaponName");
        WeaponName.Text = StoredData.CurrentSoldierNode.Character.GetEquippedWeapon().Name;
        RichTextLabel CurrentUnit = (RichTextLabel)GetNode("CurrentUnit");
        CurrentUnit.Text = "Selected Unit Name: " + StoredData.CurrentSoldierNode.Character.Name;
        RichTextLabel CurrentAmmo = (RichTextLabel)GetNode("AmmoCount");
        CurrentAmmo.Text = StoredData.CurrentSoldierNode.Character.GetEquippedWeapon().CurrentAmmo.ToString();
    }

    public void LoadPopup(string PopupText)
    {
        // Load and show the PopupScene
        Node Popupscene = ResourceLoader.Load<PackedScene>("res://PopupScene.tscn").Instantiate();

        ((Label)Popupscene.GetChild(1)).Text = PopupText;

        AddChild(Popupscene);
    }
}