using System;
using Godot;

namespace LebJam.Enemies.Griffin.scripts;

public partial class GriffinEnemy : CharacterBody2D
{
    public void SetGriffinFlyingState(GriffinFlyingStates state)
    {
        CollisionMask = state switch
        {
            GriffinFlyingStates.Flying => (uint) CollisionLayers.PassThrough,
            GriffinFlyingStates.Walking => (uint) CollisionLayers.Ground,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state,
                null)
        };
    }
}

public enum GriffinFlyingStates
{
    Flying,
    Walking
}