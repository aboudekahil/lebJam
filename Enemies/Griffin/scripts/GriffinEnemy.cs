using System;
using Godot;

namespace LebJam.Enemies.Griffin.scripts;

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

public enum GriffinFlyingStates
{
    Flying,
    Walking
}

