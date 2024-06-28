using System;
using Godot;
using LebJam.Enemies.Griffin.scripts;
using LebJam.FSM.scripts;

namespace LebJam.Enemies.Griffin.scenes;

public partial class GriffinEnterScene : State
{
    private Camera2D _camera;
    private Vector2 _direction;
    [Export] private float _flyingSpeed = 100.0f;
    [Export] private CharacterBody2D _griffin;
    private Vector2 _originalLocation;
    private Vector2 _velocity;

    public override void PrepareState()
    {
        _griffin.CollisionMask = 4;
        _camera = GetTree().GetFirstNodeInGroup("camera") as Camera2D;
        _originalLocation = _griffin.GlobalPosition;
        if (_camera == null)
        {
            throw new Exception("AYREEEEE");
            return;
        }

        _griffin.GlobalPosition =
            _camera.GlobalPosition +
            (GetViewportRect() * GetCanvasTransform()).Position *
            (1 / _camera.Zoom.X) * 0.5f + new Vector2(5, 5);

        _direction =
            (_originalLocation - _griffin.GlobalPosition).Normalized() *
            _flyingSpeed;
    }

    public override void ProcessState(double delta)
    {
        _direction =
            (_originalLocation - _griffin.GlobalPosition).Normalized() *
            _flyingSpeed;
        _griffin.Velocity = _griffin.Velocity.Lerp(_direction, (float)delta);

        _griffin.MoveAndSlide();

        var distanceFromOrgin =
            (_griffin.GlobalPosition - _originalLocation).Length();
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
        // _griffin.CollisionMask = 1;
    }
}