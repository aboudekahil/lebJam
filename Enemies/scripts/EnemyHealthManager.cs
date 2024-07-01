using Godot;
using LebJam.player.scripts;

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

    public override void _EnterTree()
    {
        _hitBox.AreaEntered += HandlePlayerEnter;
    }

    public override void _ExitTree()
    {
        _hitBox.AreaEntered -= HandlePlayerEnter;
    }

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

    private void HandlePlayerEnter(Area2D area)
    {
        if (area.GetParent() is not Player player)
        {
            return;
        }

        TakeDamage(player.Damage);
    }
}