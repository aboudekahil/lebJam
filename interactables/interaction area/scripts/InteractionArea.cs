using Godot;
using InteractionManager =
    LebJam.interactables.interaction_manager.scripts.InteractionManager;

namespace LebJam.interactables.interaction_area.scripts;

[GlobalClass]
public partial class InteractionArea : Area2D
{
    private Callable _interact;
    private InteractionManager _interactionManager;
    [Export] public string ActionName = "Interact";

    public Callable Interact
    {
        get => _interact;
        set => _interact = value;
    }

    public override void _Ready()
    {
        _interactionManager =
            GetNode<InteractionManager>("/root/InteractionManager");
    }

    public override void _EnterTree()
    {
        AreaEntered += OnPlayerEnter;
        AreaExited += OnPlayerExit;
    }

    public override void _ExitTree()
    {
        AreaEntered -= OnPlayerEnter;
        AreaExited -= OnPlayerExit;
        _interactionManager.UnregisterArea(this);
    }

    private void OnPlayerEnter(Area2D playerArea)
    {
        _interactionManager.RegisterArea(this);
    }

    private void OnPlayerExit(Area2D playerArea)
    {
        _interactionManager.UnregisterArea(this);
    }
}