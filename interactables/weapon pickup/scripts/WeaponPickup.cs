using Godot;
using LebJam.interactables.interaction_area.scripts;
using Player = LebJam.player.scripts.Player;

namespace LebJam.interactables.weapon_pickup.scripts;

public partial class WeaponPickup : Node2D
{
    private Sprite2D _pickableSprite;
    private Player _player;
    [Export] private PackedScene _weapon;

    public override void _Ready()
    {
        _pickableSprite = GetNode<Sprite2D>("%WeaponSprite");
        var pickableRootNode = _weapon.Instantiate();
        var itemSprite = pickableRootNode.GetNode<Sprite2D>("Sprite2D");
        var interactionArea = GetNode<InteractionArea>("%InteractionArea");
        _player = (Player)GetTree().GetFirstNodeInGroup("player");

        interactionArea.Interact =
            new Callable(this, nameof(HandlePlayerPickup));
        interactionArea.ActionName = "pickup";

        _pickableSprite.Texture = itemSprite.Texture;
    }

    private void HandlePlayerPickup()
    {
        _player.AddWeapon(_weapon);

        QueueFree();
    }
}