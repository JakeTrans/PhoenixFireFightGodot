using Godot;
using System;
using FireFight;
using FireFight.CharacterObjects;
using GodotPlugins.Game;
using FireFight.Classes;

public partial class Target : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public Character Character { get; set; }

    public override void _Ready()
    {
        Character = new Character(7, 1);
        Character.Xpos = (uint)Position.X;
        Character.Ypos = (uint)Position.Y;
        Character.CurrentTarget = null;
        Character.MapScale = 100;
        Character.RangedWeapons.Add(new RangedWeapon(1, WeaponType.AssaultRifles));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}