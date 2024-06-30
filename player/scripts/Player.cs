using System;
using System.Collections.Generic;
using Godot;
using LebJam.scripts;
using WeaponManager = LebJam.weapons.WeaponManager;

namespace LebJam.player.scripts;

public partial class Player : CharacterBody2D
{
    private const float NormalSpeed = 85.0f;
    private const float RunningSpeedBoost = 100.0f;


    private float _baseRotation;
    private bool _isHoldingTeleport;
    private float _rotation;
    private Timer _teleportationTimer;
    [Export] private float _teleportRange = 40.0f;
    private Sprite2D _tpGhost;
    private WeaponManager _weaponManager;

    private bool IsTeleportationPointValid(Vector2 targetPosition)
    {
        var spaceState = GetWorld2D().DirectSpaceState;
        var queryParameters = new PhysicsPointQueryParameters2D
        {
            Position = targetPosition,
            CollisionMask = (uint)CollisionLayers.Ground,
            CollideWithBodies = true,
            CollideWithAreas = true
        };
        var result = spaceState.IntersectPoint(queryParameters, 1);
        return result.Count == 0;
    }

    public override void _Ready()
    {
        _weaponManager = GetNode<WeaponManager>("WeaponManager");
        _teleportationTimer = GetNode<Timer>("%TeleportationTimer");

        _baseRotation = RotationDegrees;
        _rotation = _baseRotation;
        _tpGhost = GetNode<Sprite2D>("%TeleportGhost");
    }

    public override void _PhysicsProcess(double delta)
    {
        MovePlayer(delta);
        RotateTowardsMouse(delta);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("teleport"))
        {
            _isHoldingTeleport = true;
            _tpGhost.Visible = true;
            _tpGhost.GlobalPosition = GlobalPosition + TeleportRange();
        } 
        else if (@event.IsActionReleased("teleport"))
        {
            _tpGhost.Visible = false;
            TeleportSpecial(TeleportRange());
            _isHoldingTeleport = false;
        } 
        else if (@event is InputEventMouseButton mouseButton)
        {
            var a = GetWorld2D().DirectSpaceState.IntersectPoint(
                new PhysicsPointQueryParameters2D
                {
                    Position = mouseButton.GlobalPosition,
                    CollisionMask = (uint)CollisionLayers.Enemy,
                    CollideWithAreas = true,
                    CollideWithBodies = true
                }, 1);

            if(a.Count > 0)
            {
                GD.Print(a[0].GetValueOrDefault("collider"));
            }
        }
        else if (@event is not InputEventMouseMotion || !_isHoldingTeleport)
        {
            _tpGhost.GlobalPosition = GlobalPosition + TeleportRange();
        }

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
        } else
        {
            newRotation = -Mathf.Atan2(mousePosition.X - position.X,
                mousePosition.Y - position.Y) - Mathf.Pi / 2;
        }

        newRotation = (float)Mathf.Clamp(newRotation, -0.55, 0.55);

        Rotation = Mathf.RotateToward(Rotation, newRotation, (float)delta * 10);
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

    private void TeleportSpecial(Vector2 teleportVector)
    {
        if (_teleportationTimer.TimeLeft > 0 ||
            !IsTeleportationPointValid(GlobalPosition + teleportVector))
        {
            return;
        }

        GlobalPosition += teleportVector;

        _teleportationTimer.Start();
    }

    private Vector2 TeleportRange()
    {
        var mousePosition = GetGlobalMousePosition();

        var teleportVector = mousePosition - GlobalPosition;

        if (teleportVector.Length() > _teleportRange)
        {
            teleportVector = teleportVector.Normalized() * _teleportRange;
        }

        return teleportVector;
    }
}