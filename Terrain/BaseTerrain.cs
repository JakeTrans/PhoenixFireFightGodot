using FireFight.Classes;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFightGodot.Terrain
{
    public enum Height
    {
        Low,
        Medium,
        High,
        Blocking
    }

    public enum LineofSight
    {
        NonBlocking,
        Blocking
    }

    internal partial class BaseTerrain : Node2D
    {
        public BaseTerrain(Height height, LineofSight lineOfSight)
        {
            this.height = height;
            this.lineOfSight = lineOfSight;
        }

        public Height height { get; set; }
        public LineofSight lineOfSight { get; set; }
    }
}