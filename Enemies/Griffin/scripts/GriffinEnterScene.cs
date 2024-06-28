using System;
using System.Diagnostics;
using Godot;
using LebJam.Enemies.Griffin.scripts;
using LebJam.FSM.scripts;

namespace LebJam.Enemies.Griffin.scenes;

public partial class GriffinEnterScene : State
{
    private Camera2D _camera;
    private Vector2 _direction;
    [Export] private float _flyingSpeed = 100.0f;
    [Export] private GriffinEnemy _griffinEnemy;
    private Vector2 _originalLocation;
    private Vector2 _velocity;

    public override void PrepareState()
    {
        _griffinEnemy.SetGriffinFlyingState(GriffinFlyingStates.Flying);
        _camera = (Camera2D) GetTree().GetFirstNodeInGroup("camera");
        _originalLocation = _griffinEnemy.GlobalPosition;

        _griffinEnemy.GlobalPosition =
            _camera.GlobalPosition +
            (GetViewportRect() * GetCanvasTransform()).Position *
            (1 / _camera.Zoom.X) * 0.5f + new Vector2(5, 5);

        _direction =
            (_originalLocation - _griffinEnemy.GlobalPosition).Normalized() *
            _flyingSpeed;
    }

    public override void ProcessState(double delta)
    {
        _direction =
            (_originalLocation - _griffinEnemy.GlobalPosition).Normalized() *
            _flyingSpeed;
        _griffinEnemy.Velocity = _griffinEnemy.Velocity.Lerp(_direction, (float)delta);

        _griffinEnemy.MoveAndSlide();

        var distanceFromOrgin =
            (_griffinEnemy.GlobalPosition - _originalLocation).Length();
        if (distanceFromOrgin < 1)
        {
            GetParent<FSM.scripts.FSM>().ChangeStates<Idle>();
        }

        if(distanceFromOrgin < 100)
        {
            _flyingSpeed = (float)Mathf.Lerp(_flyingSpeed, 0, delta* 10);
        }
    }

    public override void ResetState()
    {
        _griffinEnemy.SetGriffinFlyingState(GriffinFlyingStates.Walking);
    }
}