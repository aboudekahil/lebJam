using System;
using System.Collections.Generic;
using Godot;

namespace LebJam.FSM.scripts;

[GlobalClass]
public partial class FSM : Node
{
    private State _currentState;
    [Export] private State _defaultState;
    private Dictionary<Type, State> _states;

    public override void _Ready()
    {
        _currentState = _defaultState;
        _states = new Dictionary<Type, State>();
        var children = GetChildren();


        foreach (var child in children)
        {
            if (child is not State s)
            {
                throw new ArgumentException("Illegal Child in FSM");
            }

            _states.Add(s.GetType(), s);
            s.ResetState();
            s.PrepareState();
        }
    }

    public void ChangeStates<T>()
        where T : State
    {
        if (!_states.TryGetValue(typeof(T), out var state))
        {
            return;
        }

        if (state is null)
        {
            return;
        }

        _currentState.ResetState();
        _currentState = state;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        _currentState.HandleStateInput(@event);
    }

    public override void _Process(double delta)
    {
        _currentState.ProcessState(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentState.PhysicsProcessState(delta);
    }
}