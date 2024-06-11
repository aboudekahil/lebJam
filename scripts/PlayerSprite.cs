using Godot;

namespace LebJam.scripts;

public partial class PlayerSprite : Sprite2D
{
    private CharacterBody2D _body2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _body2D = GetParent() as CharacterBody2D;
    }

    public override void _Process(double delta)
    {
        var position = GlobalPosition;
        var mousePosition = GetGlobalMousePosition();
        var isFarEnough = Mathf.Abs(position.X - mousePosition.X) > 15;

        FlipH = position.X > mousePosition.X && isFarEnough;
    }
}