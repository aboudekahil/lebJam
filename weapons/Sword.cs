using Godot;

namespace LebJam.scripts;

public partial class Sword : Weapon
{
    public override void Use()
    {
        GD.Print("I AM USING A SWORD");
    }
}