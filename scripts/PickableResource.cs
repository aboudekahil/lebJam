using Godot;

namespace LebJam.scripts;

public enum PickableType
{
    Weapon
};

[GlobalClass]
public partial class PickableResource : Resource
{
    [Export] public PickableType PickableType { get; set; }
    [Export] public PackedScene Pickable { get; set; }
}