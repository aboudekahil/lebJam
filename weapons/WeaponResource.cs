using Godot;

namespace LebJam.scripts;

[GlobalClass]
public partial class WeaponResource : Resource
{
    [Export] private string _weaponName;
    [Export] private PackedScene _weaponScene;
}