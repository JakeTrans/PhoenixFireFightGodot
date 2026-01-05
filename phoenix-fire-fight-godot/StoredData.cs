using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixFireFightGodot
{
    public static class StoredData
    {
        public static List<Soldier> Soldiers = new List<Soldier>();
        public static uint NumberOfSoldiers = 2;
        public static Soldier CurrentSoldierNode;
    }
}