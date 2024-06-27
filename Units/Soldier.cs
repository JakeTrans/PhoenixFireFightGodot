using FireFight.CharacterObjects;
using FireFight.Classes;
using Godot;
using System;

public partial class Soldier : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public Character Character { get; set; }

    public Texture2D DefaultSprite;
    public Texture2D SelectedSprite;
    public Texture2D TargetedSprite;

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

        DefaultSprite = (Texture2D)ResourceLoader.Load("res://Sprites/PrototypeSoldier.png");

        SelectedSprite = (Texture2D)ResourceLoader.Load("res://Sprites/PrototypeSoldierSelected.png");

        TargetedSprite = (Texture2D)ResourceLoader.Load("res://Sprites/PrototypeSoldierTargeted.png");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}