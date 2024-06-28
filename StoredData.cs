using Godot;
using System.Collections.Generic;

namespace FireFightGodot
{
    public static class StoredData
    {
        public static List<Soldier> Soldiers = new List<Soldier>();
        public static uint NumberOfSoldiers = 2;
        public static Soldier CurrentSoldierNode;

        public static Texture2D DefaultSprite = (Texture2D)ResourceLoader.Load("res://Sprites/PrototypeSoldier.png");
        public static Texture2D SelectedSprite = (Texture2D)ResourceLoader.Load("res://Sprites/PrototypeSoldierSelected.png");
        public static Texture2D TargetedSprite = (Texture2D)ResourceLoader.Load("res://Sprites/PrototypeSoldierTargeted.png");
    }
}