using Godot;

namespace LebJam;

public partial class PlayerMovement : CharacterBody2D
{
    [Export] private const float NormalSpeed = 85.0f;

    private const float RunningSpeedBoost = 100.0f;
    private float _baseRotation;
    private float _rotation;

    public override void _Ready()
    {
        _baseRotation = RotationDegrees;
        _rotation = _baseRotation;
    }

    public override void _PhysicsProcess(double delta)
    {
        var isRunning = Input.IsActionPressed("running");
        var speed = NormalSpeed + (isRunning ? RunningSpeedBoost : 0.0f);

        var velocity = Velocity;

        var direction =
            Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            velocity = direction.Normalized() * speed;
        } else
        {
            velocity = velocity.MoveToward(Vector2.Zero,
                (float)delta * speed * 12.5f);
        }


        Velocity = velocity;
        MoveAndSlide();
    }
}