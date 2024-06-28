using Godot;

namespace LebJam.Enemies.scripts;

public partial class EnemyHealthManager : Node
{

	[Export] private CharacterBody2D _enemy;
	[Export] private Area2D _hitBox;
	[Export] private uint _health = 100;

	[Signal]
	public delegate void EnemyHitEventHandler();

	[Signal]
	public delegate void EnemyDeathEventHandler();
	
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