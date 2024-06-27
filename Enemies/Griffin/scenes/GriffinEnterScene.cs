using System;
using Godot;
using LebJam.Enemies.Griffin.scripts;
using LebJam.FSM.scripts;

namespace LebJam.Enemies.Griffin.scenes;

public partial class GriffinEnterScene : State
{
    private Camera2D _camera;
    private Vector2 _direction;
    [Export] private float _flyingSpeed = 10.0f;
    [Export] private CharacterBody2D _griffin;
    private Vector2 _originalLocation;
    private Vector2 _velocity;
    
    public Vector2 GetCameraTopLeft()
    {
        var viewport = GetViewportRect();
        var viewportSize = viewport.Size;
        var cameraTransform = _camera.GlobalTransform;

        var offset = (viewportSize / 2) * -_camera.Offset;
        var topLeft = _camera.GlobalPosition + offset;

        return topLeft;
    }

    public override void PrepareState()
    {
        _camera = GetTree().GetFirstNodeInGroup("camera") as Camera2D;
        _originalLocation = _griffin.GlobalPosition;
        if (_camera == null)
        {
            throw new Exception("AYREEEEE");
            return;
        }

        Rect2 a = GetViewportRect() * GetCanvasTransform(); 
        _griffin.GlobalPosition =
            GetCameraTopLeft() + new Vector2(-5, -5);
        
        GD.Print(_griffin.GlobalPosition);

        _direction = (_originalLocation - _griffin.GlobalPosition).Normalized() *
                     _flyingSpeed;
    }

    public override void ProcessState(double delta)
    {
        _griffin.Velocity = _velocity;

        _griffin.MoveAndSlide();

        if (GlobalPosition == _originalLocation)
        {
            GetParent<FSM.scripts.FSM>().ChangeStates<Idle>();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _velocity = GlobalPosition.Lerp(_originalLocation, (float)delta);
    }
}