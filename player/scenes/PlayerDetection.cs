using Godot;

namespace LebJam.player.scenes;

[GlobalClass]
public partial class PlayerDetection : Area2D
{
	public bool IsPlayerInRange = false;
	public override void _EnterTree()
	{
		AreaEntered += HandlePlayerEnter;
		AreaExited += HandlePlayerExit;
	}

	public override void _ExitTree()
	{
		AreaEntered -= HandlePlayerEnter;
		AreaExited -= HandlePlayerExit;
	}

	private void HandlePlayerEnter(Area2D player)
	{
		IsPlayerInRange = true;
	}

	private void HandlePlayerExit(Area2D player)
	{
		IsPlayerInRange = false;
	}
}