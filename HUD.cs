using Godot;
using System;

public partial class hud : CanvasLayer
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        System.Diagnostics.Debug.Print("fasffafzsdccddcs");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void _on_n_pressed()
    {
        System.Diagnostics.Debug.Print("n");
    }

    private void _on_nw_pressed()
    {
        System.Diagnostics.Debug.Print("nw");
    }

    private void _on_ne_pressed()
    {
        System.Diagnostics.Debug.Print("ne");
    }

    private void _on_e_pressed()
    {
        System.Diagnostics.Debug.Print("e");
    }

    private void _on_se_pressed()
    {
        System.Diagnostics.Debug.Print("se");
    }

    private void _on_s_pressed()
    {
        System.Diagnostics.Debug.Print("s");
    }

    private void _on_sw_pressed()
    {
        System.Diagnostics.Debug.Print("sw");
    }

    private void _on_w_pressed()
    {
        System.Diagnostics.Debug.Print("w");
    }

    private void _on_fire_pressed()
    {
        System.Diagnostics.Debug.Print("fire");
    }

    private void _on_reload_pressed()
    {
        System.Diagnostics.Debug.Print("reload");
    }

    private void _on_target_pressed()
    {
        System.Diagnostics.Debug.Print("target");
    }

    private void _on_aim_pressed()
    {
        System.Diagnostics.Debug.Print("aim");
    }

    private void _on_end_turn_pressed()
    {
        System.Diagnostics.Debug.Print("end turn");
    }
}