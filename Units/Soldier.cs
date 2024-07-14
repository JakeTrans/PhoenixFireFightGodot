using FireFight.CharacterObjects;
using FireFight.Classes;
using Godot;
using System;

public partial class Soldier : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public Character Character { get; set; }

    private AnimatedSprite2D _animatedSprite;

    private Animation ActiveAnimation;

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

        _animatedSprite = GetNode<AnimatedSprite2D>("SoldierspriteAnimated");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        //Running animation for Idle
        //_animatedSprite.Play("Idle");
        switch (ActiveAnimation)
        {
            case Animation.Idle:
                _animatedSprite.Play("Idle");
                break;

            case Animation.Melee:
                _animatedSprite.Play("Melee");
                break;

            case Animation.Move:
                _animatedSprite.Play("Move");
                break;

            case Animation.reload:
                _animatedSprite.Play("reload");
                break;

            case Animation.Shoot:
                _animatedSprite.Play("Shoot");
                break;
        }
    }
}