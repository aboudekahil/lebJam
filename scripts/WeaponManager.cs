using System.Collections.Generic;
using Godot;

namespace LebJam.scripts;

public partial class WeaponManager : Weapon
{
    private readonly List<Weapon> _weapons = new();
    private int _primaryWeapon = -1;

    public void AddWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
        ToggleWeapon();
    }

    private void ToggleWeapon()
    {
        _primaryWeapon = (_primaryWeapon + 1) % _weapons.Count;
        foreach (var child in GetChildren())
        {
            RemoveChild(child);
        }
        CallDeferred(Node.MethodName.AddChild, _weapons[_primaryWeapon]);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("toggle_weapon"))
        {
            ToggleWeapon();
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }
}