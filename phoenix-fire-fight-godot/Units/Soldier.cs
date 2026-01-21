using FireFight.CharacterObjects;
using FireFight.Classes;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        GhostCollisionShape = GhostSprite.GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>("CollisionShape2DGhost");
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
        PhysicsDirectSpaceState2D spaceState = GetWorld2D().DirectSpaceState;
        // use global coordinates, not local to node
        PhysicsRayQueryParameters2D query = PhysicsRayQueryParameters2D.Create(this.GlobalPosition, Target.GlobalPosition);
        Godot.Collections.Dictionary result = spaceState.IntersectRay(query);

        if (result.Count == 0)
        {
            return true;
        }

        return false;
    }

    private void _on_area_2d_body_entered(Node2D body)
    {
        GD.Print("ghost collision");
    }
}