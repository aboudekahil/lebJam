using System.Collections.Generic;
using Godot;

namespace LebJam.scripts;

public partial class WeaponManager : Weapon
{
    private readonly List<Weapon> _weapons = new();
    private Marker2D _leftHand;
    private int _primaryWeapon = -1;
    private Marker2D _rightHand;

    public override void _Ready()
    {
        _leftHand = GetNode<Marker2D>("%LeftHand");
        _rightHand = GetNode<Marker2D>("%RightHand");
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
        if (@event.IsActionPressed("toggle_weapon"))
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
        var position = GlobalPosition;
        var mousePosition = GetGlobalMousePosition();
        var isFarEnough = Mathf.Abs(position.X - mousePosition.X) > 15;

        if (position.X > mousePosition.X && isFarEnough)
        {
            Position = _leftHand.Position;
            Rotation = _leftHand.Rotation;
        } else if (position.X < mousePosition.X && isFarEnough)
        {
            Position = _rightHand.Position;
            Rotation = _rightHand.Rotation;
        }
    }
}