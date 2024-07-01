using System;
using Godot;

namespace LebJam.Enemies.Griffin.scripts;

public partial class GriffinEnemy : CharacterBody2D
{
    private Area2D _hitBox;

    public override void _EnterTree()
    {
        _hitBox = GetNode<Area2D>("%Hitbox");
    }

    public void SetGriffinFlyingState(GriffinFlyingStates state)
    {
        switch (state)
        {
            case GriffinFlyingStates.Flying:
                CollisionMask = (uint)CollisionLayers.PassThrough;
                _hitBox.CollisionMask = (uint)CollisionLayers.PassThrough;
                break;

            case GriffinFlyingStates.Walking:
                CollisionMask = (uint)CollisionLayers.Player;
                _hitBox.CollisionMask = (uint)CollisionLayers.Player;
                break;
        }

    }
}

public enum GriffinFlyingStates
{
    Flying,
    Walking
}