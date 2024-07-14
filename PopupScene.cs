using Godot;

public partial class PopupScene : Panel
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void _on_popup_button_pressed()
    {
        //Destroys the popup
        this.QueueFree();
    }
}