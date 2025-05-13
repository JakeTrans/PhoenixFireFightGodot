using FireFight.Classes;
using FireFight.Functions;
using FireFightGodot;
using FireFightLibrary.Classes;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;

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
        setupHud();
    }

    private void AddandLogActions(ActionsPossible actionsPossible)
    {
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(actionsPossible);
        System.Diagnostics.Debug.Print(actionsPossible.ToString());
        StoredData.CurrentSoldierNode.MessageLog.Add(actionsPossible.ToString());
        GhostFunction(actionsPossible);
    }

    private void _on_n_pressed()
    {
        AddandLogActions(ActionsPossible.MoveN);
    }

    private void _on_nw_pressed()
    {
        AddandLogActions(ActionsPossible.MoveNW);
    }

    private void _on_ne_pressed()
    {
        AddandLogActions(ActionsPossible.MoveNE);
    }

    private void _on_e_pressed()
    {
        AddandLogActions(ActionsPossible.MoveE);
    }

    private void _on_se_pressed()
    {
        AddandLogActions(ActionsPossible.MoveSE);
    }

    private void _on_s_pressed()
    {
        AddandLogActions(ActionsPossible.MoveS);
    }

    private void _on_sw_pressed()
    {
        AddandLogActions(ActionsPossible.MoveSW);
    }

    private void _on_w_pressed()
    {
        AddandLogActions(ActionsPossible.MoveW);
    }

    private void _on_anti_clockwise_pressed()
    {
        AddandLogActions(ActionsPossible.RotateAntiClockWise);
    }

    private void _on_clockwise_pressed()
    {
        AddandLogActions(ActionsPossible.RotateClockwise);
    }

    private void _on_fire_pressed()
    {
        AddandLogActions(ActionsPossible.FireSingle);
    }

    private void _on_reload_pressed()
    {
        AddandLogActions(ActionsPossible.Reload);
    }

    private void _on_lineofsight_pressed()
    {
        Soldier currentTarget = StoredData.Soldiers.Where(x => x.Character == StoredData.CurrentSoldierNode.Character.CurrentTarget).First();

        Node2D targetnode = (Node2D)currentTarget;

        if (StoredData.CurrentSoldierNode.CheckLOS(targetnode) == true)
        {
            System.Diagnostics.Debug.Print("Los");
        }
        else
        {
            System.Diagnostics.Debug.Print("No Los");
        }
    }

    private void _on_target_pressed()
    {
        System.Diagnostics.Debug.Print("target");
        TargetList = StoredData.Soldiers.Where(x => x.Character.Selected == false && x.Character.KnockedOut == false).OrderBy(x => x.Character.INTSkillFactor).ToList();

        TargetList = TargetList.Where(x => GameFunctions.IsTargetWithinArc(StoredData.CurrentSoldierNode.Character.GetRotation(), (float)GetTargetBearing((int)x.Character.Xpos, (int)x.Character.Ypos), 180) == true).ToList();

        if (TargetList.Count == 0)
        {
            StoredData.CurrentSoldierNode.MessageLog.Add("No Target Available");
            return;
        }

        Soldier soldier = null;

        if (StoredData.CurrentSoldierNode.Character.CurrentTarget != null)
        {
            //clear current target

            Soldier oldTarget = StoredData.Soldiers.Where(x => x.Character == StoredData.CurrentSoldierNode.Character.CurrentTarget).First();
            ((Sprite2D)oldTarget.FindChild("Targeted")).Visible = false;

            //set new target
            int oldindex = -1;

            for (int i = 0; i < TargetList.Count; i++)
            {
                //bearing test
                bool inArc = GameFunctions.IsTargetWithinArc(StoredData.CurrentSoldierNode.Character.GetRotation(), (float)GetTargetBearing(), 180);
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
        // Show targeted sprite
        ((Sprite2D)soldier.FindChild("Targeted")).Visible = true;
    }

    private void _on_aim_pressed()
    {
        AddandLogActions(ActionsPossible.Aim);
    }

    private void _on_end_turn_pressed()
    {
        StoredData.CurrentSoldierNode.MessageLog.Clear();

        Sprite2D Ghostimage = StoredData.CurrentSoldierNode.GhostSprite;
        Ghostimage.Position = new Vector2((StoredData.CurrentSoldierNode.AnimatedSprite.Position.X), (StoredData.CurrentSoldierNode.AnimatedSprite.Position.Y));
        Ghostimage.Visible = false;

        EndTurnImpulseBase();
    }

    private void EndTurnImpulseBase()
    {
        Impulses impulses = new Impulses();

        //TODO , has everyone ended turn?
        StoredData.CurrentSoldierNode.Character.turnTaken = true;
        StoredData.CurrentSoldierNode.MessageLog.Clear();

        if (StoredData.Soldiers.Where(x => x.Character.turnTaken == false).Count() > 0)
        {
            //Next Solder

            System.Diagnostics.Debug.Print("Next Soldier ran");
        }
        else
        {
            System.Diagnostics.Debug.Print("end turn ran");
            //Gather actions

            foreach (Soldier Unit in StoredData.Soldiers)
            {
                impulses.AddActionsToImpulse(Unit.Character);
            }
            impulses.SortAllImpulseListByINTSkill();

            //run impulse in order

            //Bundle Impulses
            List<Impulse> ImpulseAction = impulses.ImpulseList1.Concat(impulses.ImpulseList2).Concat(impulses.ImpulseList3).Concat(impulses.ImpulseList4).ToList();
            //Run Actions
            foreach (Impulse Impulse in ImpulseAction)
            {
                if (Impulse.Character.KnockedOut == false) // no action if knocked out
                {
                    ActionsPossible action = Impulse.Action;

                    System.Diagnostics.Debug.Print(action.ToString());

                    if (action == ActionsPossible.FireSingle || action == ActionsPossible.FireBurst)
                    {
                        Soldier CurrenttargetNode = StoredData.Soldiers.Where(x => x.Character.Name == Impulse.Character.CurrentTarget.Name).FirstOrDefault();

                        DamageResult Result = Impulse.Character.DoAction(action);

                        if (Result != null)
                        {
                            if (Result.Disabling == false)
                            {
                                CurrenttargetNode.MessageLog.Add("Shot Hit the for " + Result.HitLocation.Trim() + " for " + Result.DamageAmount);
                            }
                            else
                            {
                                CurrenttargetNode.MessageLog.Add("Shot Hit the for " + Result.HitLocation.Trim() + " for " + Result.DamageAmount);
                            }
                        }
                        else
                        {
                            CurrenttargetNode.MessageLog.Add("Shot Taken Missing");
                        }

                        foreach (Soldier soldier in StoredData.Soldiers)
                        {
                            if (soldier.Character.KnockedOut == true)
                            {
                                ((Sprite2D)soldier.FindChild("Downed")).Visible = true;
                            }
                        }
                    }
                    else
                    {
                        Impulse.Character.DoAction(action);
                    }

                    Soldier CurrentSoldierNode = StoredData.Soldiers.Where(x => x.Character.Name == Impulse.Character.Name).FirstOrDefault();

                    CurrentSoldierNode.GlobalPosition = new Vector2(CurrentSoldierNode.Character.Xpos, CurrentSoldierNode.Character.Ypos);

                    CurrentSoldierNode.GlobalRotationDegrees = CurrentSoldierNode.Character.GetRotation();
                }
            }
            foreach (Soldier soldier in StoredData.Soldiers)
            {
                soldier.Character.turnTaken = false;
                soldier.Character.ActionsForTurn.ActionsTaken.Clear();
            }

            //Check for end of game
            //TODO: Check for end of game
            int? GameEnd = DetectGameEnd();
            if (GameEnd != null)
            {
                RichTextLabel GameOver = (RichTextLabel)GetNode("GameOver");
                GameOver.Text = "Game Over. Side " + GameEnd + " has won!";

                GameOver.Visible = true;
            }
        }

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

        //Clear targetted sprite

        foreach (Soldier soldier in StoredData.Soldiers)
        {
            ((Sprite2D)soldier.FindChild("Selected")).Visible = false;
            ((Sprite2D)soldier.FindChild("Targeted")).Visible = false;
        }

                //set new sprite

                ((Sprite2D)FilteredList[oldindex].FindChild("Selected")).Visible = false;

        StoredData.CurrentSoldierNode.Character.Selected = false;
        //Next Characters turn
        if (oldindex + 1 < FilteredList.Count)
        {
            ((Sprite2D)FilteredList[oldindex + 1].FindChild("Selected")).Visible = true;

            StoredData.CurrentSoldierNode = FilteredList[oldindex + 1];
            StoredData.CurrentSoldierNode.Character.Selected = true;
        }
        else
        {
            ((Sprite2D)FilteredList[0].FindChild("Selected")).Visible = true;

            StoredData.CurrentSoldierNode = FilteredList[0];
            StoredData.CurrentSoldierNode.Character.Selected = true;
        }

        if (StoredData.CurrentSoldierNode.Character.CurrentTarget != null)
        {
            foreach (Soldier soldier in StoredData.Soldiers)
            {
                if (soldier.Character.Name == StoredData.CurrentSoldierNode.Character.CurrentTarget.Name)
                {
                    ((Sprite2D)soldier.FindChild("Targeted")).Visible = true;
                }
            }
        }
    }

    private void setupHud()
    {
        RichTextLabel WeaponName = (RichTextLabel)GetNode("WeaponName");
        WeaponName.Text = StoredData.CurrentSoldierNode.Character.GetEquippedWeapon().Name;
        RichTextLabel CurrentUnit = (RichTextLabel)GetNode("CurrentUnit");
        CurrentUnit.Text = "Selected Unit Name: " + StoredData.CurrentSoldierNode.Character.Name;
        RichTextLabel CurrentAmmo = (RichTextLabel)GetNode("AmmoCount");
        CurrentAmmo.Text = StoredData.CurrentSoldierNode.Character.GetEquippedWeapon().CurrentAmmo.ToString();

        RichTextLabel BearingText = (RichTextLabel)GetNode("Bearing");
        RichTextLabel RangeText = (RichTextLabel)GetNode("Range");

        RichTextLabel ActionCount = (RichTextLabel)GetNode("ActionCount");
        ActionCount.Text = StoredData.CurrentSoldierNode.Character.CombatAction.ToString();

        RichTextLabel ActionsTakenCount = (RichTextLabel)GetNode("ActionsTakenCount");
        ActionsTakenCount.Text = (StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Count).ToString();

        if (StoredData.CurrentSoldierNode.Character.CombatAction == StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Count())
        {
            SetUIState(true);
        }
        else
        {
            SetUIState(false);
        }

        if (StoredData.CurrentSoldierNode.Character.CombatAction != StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Count())
        {
            if (StoredData.CurrentSoldierNode.Character.CurrentTarget != null)
            {
                BearingText.Text = "Bearing: " + GetTargetBearing().ToString();
                RangeText.Text = "Range: " + GetRangeToTarget().ToString();

                ((Button)GetNode("Aim")).Disabled = false;

                ((Button)GetNode("Fire")).Disabled = false;
            }
            else
            {
                BearingText.Text = "Bearing: No Target Selected";
                RangeText.Text = "Range: No Target Selected";

                ((Button)GetNode("Aim")).Disabled = true;

                ((Button)GetNode("Fire")).Disabled = true;
            }
        }
    }

    private void SetUIState(bool Disabled)
    {
        ((Button)GetNode("NW")).Disabled = Disabled;
        ((Button)GetNode("W")).Disabled = Disabled;
        ((Button)GetNode("SW")).Disabled = Disabled;
        ((Button)GetNode("S")).Disabled = Disabled;
        ((Button)GetNode("SE")).Disabled = Disabled;
        ((Button)GetNode("E")).Disabled = Disabled;
        ((Button)GetNode("NE")).Disabled = Disabled;
        ((Button)GetNode("N")).Disabled = Disabled;
        ((Button)GetNode("Reload")).Disabled = Disabled;
        ((Button)GetNode("Target")).Disabled = Disabled;
        ((Button)GetNode("Anti-Clockwise")).Disabled = Disabled;
        ((Button)GetNode("Clockwise")).Disabled = Disabled;
        ((Button)GetNode("Undo")).Disabled = Disabled;
        ((Button)GetNode("Aim")).Disabled = Disabled;
        ((Button)GetNode("Fire")).Disabled = Disabled;
    }

    public int? DetectGameEnd()
    {
        //Check for end of game
        //TODO: Check for end of game

        List<IGrouping<uint, Soldier>> sides = StoredData.Soldiers.Where(x => x.Character.KnockedOut == false).GroupBy(x => x.Character.Sidereference).ToList();

        if (sides.Count == 1) //only one side
        {
            return Convert.ToInt32(sides[0].Key);
        }
        else
        {
            return null;
        }
    }

    public double GetTargetBearing()
    {
        //TODO: Get target bearing
        return GameFunctions.CalculateBearing((int)StoredData.CurrentSoldierNode.Character.Xpos, (int)StoredData.CurrentSoldierNode.Character.Ypos, (int)StoredData.CurrentSoldierNode.Character.CurrentTarget.Xpos, (int)StoredData.CurrentSoldierNode.Character.CurrentTarget.Ypos);
    }

    public double GetTargetBearing(int TargetX, int TargetY)
    {
        //TODO: Get target bearing
        return GameFunctions.CalculateBearing((int)StoredData.CurrentSoldierNode.Character.Xpos, (int)StoredData.CurrentSoldierNode.Character.Ypos, TargetX, TargetY);
    }

    public uint GetRangeToTarget()
    {
        //TODO: Get Range to target

        return GameFunctions.RangeFinder(StoredData.CurrentSoldierNode.Character, StoredData.CurrentSoldierNode.Character.CurrentTarget, StoredData.CurrentSoldierNode.Character.MapScale);
    }

    private void _on_undo_pressed()
    {
        // Replace with function body.
        System.Diagnostics.Debug.Print("Undo");
        StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Clear();
        StoredData.CurrentSoldierNode.MessageLog.Clear();
    }

    private void GhostFunction(ActionsPossible actionsPossible)
    {
        Sprite2D Ghostimage = StoredData.CurrentSoldierNode.GhostSprite;
        CollisionShape2D GhostCollisionShape = StoredData.CurrentSoldierNode.GhostCollisionShape;
        Ghostimage.Visible = true;

        switch (actionsPossible)
        {
            case ActionsPossible.MoveN:
                Ghostimage.Position = new Vector2((Ghostimage.Position.X), (Ghostimage.Position.Y - 10));
                break;

            case ActionsPossible.MoveNE:
                Ghostimage.Position = new Vector2((Ghostimage.Position.X + 10), (Ghostimage.Position.Y - 10));
                break;

            case ActionsPossible.MoveE:
                Ghostimage.Position = new Vector2((Ghostimage.Position.X + 10), (Ghostimage.Position.Y));
                break;

            case ActionsPossible.MoveSE:
                Ghostimage.Position = new Vector2((Ghostimage.Position.X + 10), (Ghostimage.Position.Y + 10));
                break;

            case ActionsPossible.MoveS:
                Ghostimage.Position = new Vector2((Ghostimage.Position.X), (Ghostimage.Position.Y + 10));
                break;

            case ActionsPossible.MoveSW:
                Ghostimage.Position = new Vector2((Ghostimage.Position.X - 10), (Ghostimage.Position.Y + 10));
                break;

            case ActionsPossible.MoveW:
                Ghostimage.Position = new Vector2((Ghostimage.Position.X - 10), (Ghostimage.Position.Y));
                break;

            case ActionsPossible.MoveNW:
                Ghostimage.Position = new Vector2((Ghostimage.Position.X - 10), (Ghostimage.Position.Y - 10));
                break;

            case ActionsPossible.RotateClockwise:
                GhostRotatefunction(true);
                break;

            case ActionsPossible.RotateAntiClockWise:
                GhostRotatefunction(false);
                break;
        }

        GhostCollisionShape.Position = Ghostimage.Position;
    }

    public GeneralFunctions.CardinalDirectionsSquare Facing { get; set; }

    private void GhostRotatefunction(bool Clockwise)
    {
        if (Clockwise == true)
        {
            if ((int)Facing == 8)
            {
                Facing = (GeneralFunctions.CardinalDirectionsSquare)1;
            }
            else
            {
                Facing = Facing + 1;
            }
        }
        else
        {
            if ((int)Facing == 1)
            {
                Facing = (GeneralFunctions.CardinalDirectionsSquare)8;
            }
            else
            {
                Facing = Facing - 1;
            }
        }
    }

    public uint GetRotation() // System  to assume all character models face north and rotation is clockwise
    {
        switch (Facing)
        {
            case GeneralFunctions.CardinalDirectionsSquare.North:
                return 0;

            case GeneralFunctions.CardinalDirectionsSquare.NorthEast:
                return 45;

            case GeneralFunctions.CardinalDirectionsSquare.East:
                return 90;

            case GeneralFunctions.CardinalDirectionsSquare.SouthEast:
                return 135;

            case GeneralFunctions.CardinalDirectionsSquare.South:
                return 180;

            case GeneralFunctions.CardinalDirectionsSquare.SouthWest:
                return 225;

            case GeneralFunctions.CardinalDirectionsSquare.West:
                return 270;

            case GeneralFunctions.CardinalDirectionsSquare.NorthWest:
                return 315;

            default:
                throw new NotImplementedException("Direction not recogised--" + Facing.ToString());
        }
    }
}