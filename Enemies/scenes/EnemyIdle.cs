using Godot;
using LebJam.FSM.scripts;

namespace LebJam.Enemies.scenes;

public partial class EnemyIdle : State
{
    [Export] private NavigationAgent2D _nav;
    [Export] private CharacterBody2D _enemy;

    public override void ProcessState(double delta)
    {
        _nav.TargetPosition = GetGlobalMousePosition();

        var direction = _nav.GetNextPathPosition() - _enemy.GlobalPosition;
        direction = direction.Normalized();

        _enemy.Velocity = _enemy.Velocity.Lerp(direction * 10, (float)
            (7 * delta));

        _enemy.MoveAndSlide();
    }
}