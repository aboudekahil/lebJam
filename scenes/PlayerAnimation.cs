using Godot;

namespace LebJam.scenes;

public partial class PlayerAnimation : AnimationPlayer
{
	private CharacterBody2D _body2D; 
	
	public override void _Ready()
	{
		_body2D = GetParent() as CharacterBody2D;
	}

	public override void _Process(double delta)
	{
		if (_body2D.Velocity != Vector2.Zero)
		{
			Play("walk");
		} else
		{
			Pause();
		}
	}

}