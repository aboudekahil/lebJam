using System;
using Godot;

namespace LebJam.Enemies.Griffin.scripts;

public enum GriffinFlyingStates
{
    Flying,
    Walking
}

public partial class GriffinEnemy : CharacterBody2D
{
    public void SetGriffinFlyingState(GriffinFlyingStates state)
    {
        CollisionMask = state switch
        {
            GriffinFlyingStates.Flying => 4,
            GriffinFlyingStates.Walking => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state,
                null)
        };
    }
}