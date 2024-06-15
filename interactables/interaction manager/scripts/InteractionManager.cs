using System.Collections.Generic;
using Godot;
using LebJam.interactables.interaction_area.scripts;
using Player = LebJam.player.scripts.Player;

namespace LebJam.interactables.interaction_manager.scripts;

public partial class InteractionManager : Node2D
{
    private const string BaseText = "[E] to ";
    private readonly List<InteractionArea> _activeAreas = new();
    private bool _canInteract = true;
    private Label _label;
    private Player _player;

    public override void _Ready()
    {
        _player = (Player)GetTree().GetFirstNodeInGroup("player");
        _label = GetNode<Label>("%InteractionLabel");
    }

    public void RegisterArea(InteractionArea area)
    {
        _activeAreas.Add(area);
    }

    public void UnregisterArea(InteractionArea area)
    {
        _activeAreas.Remove(area);
    }

    public override void _Process(double delta)
    {
        if (_label is null)
        {
            return;
        }

        if (_activeAreas.Count <= 0 || !_canInteract)
        {
            _label.Hide();
            return;
        }

        _activeAreas.Sort(SortByPlayerDistance);
        _label.Text = BaseText + _activeAreas[0].ActionName;
        _label.GlobalPosition = _activeAreas[0].GlobalPosition;
        _label.GlobalPosition = _label.GlobalPosition with
        {
            Y = _label.GlobalPosition.Y - 25
        };
        _label.GlobalPosition = _label.GlobalPosition with
        {
            X = _label.GlobalPosition.X -
                _label.Size.X / 2
        };

        _label.Show();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("interact") && _canInteract)
        {
            if (_activeAreas.Count <= 0)
            {
                return;
            }

            _canInteract = false;
            _label.Hide();

            _activeAreas[0].Interact.Call();

            _canInteract = true;
        }
    }

    private int SortByPlayerDistance(InteractionArea area1,
        InteractionArea area2)
    {
        var area1ToPlayer =
            _player.GlobalPosition.DistanceTo(area1.GlobalPosition);
        var area2ToPlayer =
            _player.GlobalPosition.DistanceTo(area2.GlobalPosition);

        return area1ToPlayer < area2ToPlayer ? -1 : 1;
    }
}