using FireFightGodot;
using Godot;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using System.Security.AccessControl;
using FireFight.Classes;
using Azure.Identity;
using FireFight.Functions;

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

		TargetList = StoredData.Soldiers.Where(x => x.Character.Selected == false && x.Character.KnockedOut == false).OrderBy(x => x.Character.INTSkillFactor).ToList();

		TargetList = TargetList.Where(x => GameFunctions.IsTargetWithinArc(StoredData.CurrentSoldierNode.Character.GetRotation(), (float)GetTargetBearing((int)x.Character.Xpos, (int)x.Character.Ypos), 180) == true).ToList();

		if (TargetList.Count == 0)
		{
			LoadPopup("No Target Available");
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
		System.Diagnostics.Debug.Print("aim");
		StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken.Add(ActionsPossible.Aim);
	}

	private void _on_end_turn_pressed()
	{
		//Take Actions
		System.Diagnostics.Debug.Print("end turn ran");

		foreach (ActionsPossible action in StoredData.CurrentSoldierNode.Character.ActionsForTurn.ActionsTaken)
		{
			System.Diagnostics.Debug.Print(action.ToString());

			if (action == ActionsPossible.FireSingle || action == ActionsPossible.FireBurst)
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
		bool GameEnd = DetectGameEnd();
		if (GameEnd == true)
		{
			LoadPopup("Game Over");
		}

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
		if (StoredData.CurrentSoldierNode.Character.CurrentTarget != null)
		{
			BearingText.Text = "Bearing: " + GetTargetBearing().ToString();
			RangeText.Text = "Range: " + GetRangeToTarget().ToString();
		}
		else
		{
			BearingText.Text = "Bearing: No Target Selected";
			RangeText.Text = "Range: No Target Selected";
		}
	}

	public void LoadPopup(string PopupText)
	{
		// Load and show the PopupScene
		Node Popupscene = ResourceLoader.Load<PackedScene>("res://PopupScene.tscn").Instantiate();

		((Label)Popupscene.GetChild(1)).Text = PopupText;

		AddChild(Popupscene);
	}

	public bool DetectGameEnd()
	{
		//Check for end of game
		//TODO: Check for end of game

		List<IGrouping<uint, Soldier>> sides = StoredData.Soldiers.Where(x => x.Character.KnockedOut == false).GroupBy(x => x.Character.Sidereference).ToList();

		if (sides.Count == 1) //only one side
		{
			return true;
		}
		else
		{
			return false;
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
}
