using Godot;
using LebJam.FSM.scripts;
using LebJam.player.scripts;

namespace LebJam.Enemies.Griffin.scripts;

public partial class GriffinEnterScene : State
{
    private PlayerCam _camera;
    private Vector2 _direction;
    [Export] private float _flyingSpeed = 100.0f;
    [Export] private GriffinEnemy _griffinEnemy;
    private Vector2 _originalLocation;
    private Vector2 _velocity;

    public override void _Ready()
    {
        _camera = (PlayerCam)GetTree().GetFirstNodeInGroup("camera");
    }

    public override void PrepareState()
    {
        _griffinEnemy.SetGriffinFlyingState(GriffinFlyingStates.Flying);
        _originalLocation = _griffinEnemy.GlobalPosition;

        _griffinEnemy.GlobalPosition =
            _camera.GlobalPosition + _camera.GetTopRightViewCorner()
                                   + new Vector2(5, 5);

        _direction =
            (_originalLocation - _griffinEnemy.GlobalPosition).Normalized() *
            _flyingSpeed;
    }

    public override void ProcessState(double delta)
    {
        _direction =
            (_originalLocation - _griffinEnemy.GlobalPosition).Normalized() *
            _flyingSpeed;
        _griffinEnemy.Velocity =
            _griffinEnemy.Velocity.Lerp(_direction, (float)delta);

        _griffinEnemy.MoveAndSlide();

        var distanceFromOrigin =
            (_griffinEnemy.GlobalPosition - _originalLocation).Length();
        if (distanceFromOrigin < 1)
        {
            GetParent<FSM.scripts.FSM>().ChangeStates<Idle>();
        }

        if (distanceFromOrigin < 100)
        {
            _flyingSpeed = (float)Mathf.Lerp(_flyingSpeed, 0, delta * 10);
        }
    }

    public override void ResetState()
    {
        _griffinEnemy.SetGriffinFlyingState(GriffinFlyingStates.Walking);
        GD.Print("hittable");
    }
}