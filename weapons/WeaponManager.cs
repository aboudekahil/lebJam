using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using LebJam.scripts;

namespace LebJam.weapons;

public partial class WeaponManager : Weapon
{
    private readonly List<Weapon> _weapons = new();
    [Export] private Array<PackedScene> _defaultWeapons = new();
    private Marker2D _leftHand;
    private int _primaryWeapon = -1;
    private Marker2D _rightHand;
    public uint CurrentDamage => 20;

    public override void _Ready()
    {
        _leftHand = GetNode<Marker2D>("%LeftHand");
        _rightHand = GetNode<Marker2D>("%RightHand");


        if (_defaultWeapons.Count <= 0)
        {
            return;
        }

        _weapons.AddRange(_defaultWeapons.Select(scenes =>
            scenes.Instantiate<Weapon>()));
        _primaryWeapon += _defaultWeapons.Count;
        ToggleWeapon();
    }

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
        if (@event.IsActionPressed("toggle_weapon") && _weapons.Count > 0)
        {
            ToggleWeapon();
        }

        if (@event.IsActionPressed("use_item") && _weapons.Count > 0)
        {
            _weapons[_primaryWeapon].Use();
        }
    }

    public override void _Process(double delta)
    {
    }
}