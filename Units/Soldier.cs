using FireFight.CharacterObjects;
using FireFight.Classes;
using FireFightGodot;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Soldier : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public Character Character { get; set; }

    public CollisionShape2D GhostCollisionShape;

    public AnimatedSprite2D AnimatedSprite;

    public Sprite2D GhostSprite;

    private Animation ActiveAnimation;

    public List<string> MessageLog;

    private enum Animation
    {
        Idle,
        Melee,
        Move,
        reload,
        Shoot
    }

    public override void _Ready()
    {
        MessageLog = new List<string>();
        Random rnd = new Random();
        Character = new Character(7, 0);
        Character.Name = rnd.Next(1, 10000).ToString();
        Character.Xpos = (uint)Position.X;
        Character.Ypos = (uint)Position.Y;
        Character.CurrentTarget = null;
        Character.MapScale = 100;
        Character.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
        Character.RangedWeapons[0].Equipped = true;
        Character.CurrentAimAmount = 20;
        ActiveAnimation = Animation.Idle;

        GhostSprite = GetNode<Sprite2D>("Ghost");

        AnimatedSprite = GetNode<AnimatedSprite2D>("SoldierspriteAnimated");

        GhostCollisionShape = GetNode<Sprite2D>("Ghost").GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>("CollisionShape2DGhost");

        GD.Print("Soldier Ready: " + Character.Name);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        //Running animation for Idle
        //_animatedSprite.Play("Idle");
        switch (ActiveAnimation)
        {
            case Animation.Idle:
                AnimatedSprite.Play("Idle");
                break;

            case Animation.Melee:
                AnimatedSprite.Play("Melee");
                break;

            case Animation.Move:
                AnimatedSprite.Play("Move");
                break;

            case Animation.reload:
                AnimatedSprite.Play("reload");
                break;

            case Animation.Shoot:
                AnimatedSprite.Play("Shoot");
                break;
        }
        SetMessages();
    }

    public void SetMessages()
    {
        string Logtext = "";
        RichTextLabel MessageNode = (RichTextLabel)GetNode("Messages");

        foreach (string Message in MessageLog)
        {
            Logtext = Logtext + Message + "\n";
        }
        MessageNode.Text = Logtext;

        MessageNode.Visible = true;
    }

    public bool CheckLOS(Node2D Target)
    {
        var spaceState = GetWorld2D().DirectSpaceState;
        var query = PhysicsRayQueryParameters2D.Create(this.Position, Target.Position);
        query.CollideWithAreas = true;
        var result = spaceState.IntersectRay(query);

        Object Collider = result["collider"] as Object;

        if ((GodotObject)result["collider"] == Target)
        {
            GD.Print("true");
            return true;
        }
        GD.Print("true");
        return false;
    }

    private void _on_area_2d_body_entered(Node2D body)
    {
        GD.Print("ghost collision");
        StoredData.CurrentSoldierNode.MessageLog.Clear();
        Character.ActionsForTurn.ActionsTaken.Clear();
        GhostSprite.Position = AnimatedSprite.Position;
        GhostSprite.Visible = false;
    }
}