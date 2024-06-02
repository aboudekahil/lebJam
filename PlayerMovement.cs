using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	private const float Speed = 300.0f;

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;
		
		var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
