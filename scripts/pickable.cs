using System;
using Godot;

namespace LebJam.scripts;



public partial class Pickable : Area2D
{
	[Export] private PickableResource _pickableResource;
	private Sprite2D _pickableSprite;
	
	public override void _EnterTree()
	{
		AreaEntered += HandlePlayerPickup;
	}

	public override void _ExitTree()
	{
		AreaEntered -= HandlePlayerPickup;
	}

	public override void _Ready()
	{
		_pickableSprite = GetNode<Sprite2D>("PickableSprite2D");
		var pickableRootNode = _pickableResource.Pickable.Instantiate();
		var itemSprite = pickableRootNode.GetNode<Sprite2D>("Sprite2D");

		_pickableSprite.Texture = itemSprite.Texture;
	}
	
	private void HandlePlayerPickup(Area2D playerArea2D)
	{
		var player = (Player) playerArea2D.GetParent();

		switch (_pickableResource.PickableType)
		{
			case PickableType.Weapon: 
				player.AddWeapon(_pickableResource.Pickable);
				QueueFree();
				break;
			default: throw new ArgumentOutOfRangeException
			{
				HelpLink = "Pickable.cs",
				HResult = 0,
				Source = "Pickable.cs"
			};
		}
	}
}