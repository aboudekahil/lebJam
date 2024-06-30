using Godot;

namespace LebJam.Enemies.scripts;

public partial class EnemyHealthManager : Node
{
    [Signal]
    public delegate void EnemyDeathEventHandler();

    [Signal]
    public delegate void EnemyHitEventHandler();

    [Export] private CharacterBody2D _enemy;
    [Export] private uint _health = 100;
    [Export] private Area2D _hitBox;

    public override void _Process(double delta)
    {
        if (_health > 0)
        {
            return;
        }

        _enemy.QueueFree();
        EmitSignal(SignalName.EnemyDeath);
    }

    public void TakeDamage(uint damage)
    {
        _health -= damage;
        EmitSignal(SignalName.EnemyHit);
    }
}