using Godot;
using LebJam.Enemies.Griffin.scripts;
using LebJam.FSM.scripts;

namespace LebJam.Enemies.Griffin.scenes;

public partial class GriffinEnterScene : State
{
    private Vector2 _originalLocation;
    [Export] private CharacterBody2D _griffin;
    [Export] private float _flyingSpeed = 10.0f;
    private Vector2 _direction;
    private Vector2 _velocity;
    
    public override void PrepareState()
    {
        _originalLocation = GlobalPosition;
        GlobalPosition = GetTree().Root.Position - new Vector2(5, 5);
        _direction = (_originalLocation - GlobalPosition).Normalized() *
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
        _velocity = GlobalPosition.Lerp(_originalLocation, (float) delta);
        GD.Print(GlobalPosition);
    }
}