using System;
using Godot;

namespace LebJam.scripts;

public partial class Player : CharacterBody2D
{
	private const float NormalSpeed = 85.0f;

	private const float RunningSpeedBoost = 100.0f;
	private float _baseRotation;
	private float _rotation;

	private WeaponManager _weaponManager;
	
	public override void _Ready()
	{
		_weaponManager = GetNode<WeaponManager>("WeaponManager");
		
		_baseRotation = RotationDegrees;
		_rotation = _baseRotation;
	}

	public override void _PhysicsProcess(double delta)
	{
		MovePlayer(delta);
		RotateTowardsMouse(delta);
	}

	private void MovePlayer(double delta)
	{
		var isRunning = Input.IsActionPressed("running");
		var speed = NormalSpeed + (isRunning ? RunningSpeedBoost : 0.0f);

		var velocity = Velocity;

		var direction =
			Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction.Normalized() * speed;
		} else
		{
			velocity = velocity.MoveToward(Vector2.Zero,
				(float)delta * speed * 11f);
		}


		Velocity = velocity;
		MoveAndSlide();
		
	}

	private void RotateTowardsMouse(double delta)
	{
		var mousePosition = GetGlobalMousePosition();
		var position = GlobalPosition;

		float newRotation;
		
		if (position.X < mousePosition.X)
		{
			newRotation = -Mathf.Atan2(position.X - mousePosition.X,
				position.Y - mousePosition.Y) - Mathf.Pi / 2;
		}
		else
		{
			newRotation = -Mathf.Atan2(mousePosition.X - position.X,
				mousePosition.Y - position.Y) - Mathf.Pi / 2;
		}

		newRotation = (float) Mathf.Clamp(newRotation, -0.55, 0.55);

		Rotation = Mathf.RotateToward(Rotation, newRotation, (float) delta * 10);
	}
	
	public void AddWeapon(PackedScene pickableWeapon)
	{
		var newChildWeapon = pickableWeapon.Instantiate();

		if (newChildWeapon is not Weapon weapon)
		{
			throw new ArgumentException("Scene passed is not weapon");
		}
		_weaponManager.AddWeapon(weapon);
	}
}