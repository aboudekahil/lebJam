using Godot;
using LebJam.FSM.scripts;
using LebJam.player.scripts;

namespace LebJam.Enemies.scenes;

public partial class EnemyIdle : State
{
    [Export] private CharacterBody2D _enemy;
    [Export] private NavigationAgent2D _nav;
    private Player _player;
    [Export] private float _speed = 20.0f;

    public override void PrepareState()
    {
        _player = GetTree().GetNodesInGroup("player")[0] as Player;
    }

    public override void ProcessState(double delta)
    {
        _nav.TargetPosition = _player.GlobalPosition;

        var direction = _nav.GetNextPathPosition() - _enemy.GlobalPosition;
        direction = direction.Normalized();

        _enemy.Velocity = _enemy.Velocity.Lerp(direction * _speed, (float)
            (7 * delta));


        _enemy.MoveAndSlide();
    }
}