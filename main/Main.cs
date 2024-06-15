using Godot;

namespace LebJam.main;

public partial class Main : Node2D
{
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_cancel"))
		{
			GetTree().Quit();
		}
	}
}
