using Godot;

namespace LebJam.FSM.scripts;

[GlobalClass]
public partial class State : Node2D
{
    public virtual void PrepareState()
    {
    }

    public virtual void ResetState()
    {
    }

    public virtual void ProcessState(double delta)
    {
    }

    public virtual void PhysicsProcessState(double delta)
    {
    }

    public virtual void HandleStateInput(InputEvent @event)
    {
    }
}