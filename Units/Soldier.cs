using FireFight.CharacterObjects;
using FireFight.Classes;
using FireFightGodot;
using Godot;
using System;

public partial class Soldier : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public Character Character { get; set; }

	private AnimatedSprite2D _animatedSprite;

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

		_animatedSprite = GetNode<AnimatedSprite2D>("SoldierspriteAnimated");

		//_animated_sprite.play("run");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Running animation for Idle
		_animatedSprite.Play("Idle");
	}
}
