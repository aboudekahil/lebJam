using Godot;
using LebJam.Enemies.scripts;

namespace LebJam.player.scripts;

public partial class MouseArea2D : Area2D
{
    private bool _isPressed;

    public override void _EnterTree()
    {
        AreaEntered += HandleAttack;
    }

    public override void _ExitTree()
    {
        AreaEntered -= HandleAttack;
    }

    public override void _Draw()
    {
        DrawCircle(GlobalPosition, 10, Colors.Aqua);
    }

    private void HandleAttack(Area2D area)
    {
        var enemyHealthManager =
            area.GetNodeOrNull<EnemyHealthManager>("%EnemyHealthManager");
        GD.Print("H");
        if (enemyHealthManager is
                null || !_isPressed)
        {
            return;
        }

        enemyHealthManager.TakeDamage(10);
    }

    public override void _Input(InputEvent @event)
    {
        _isPressed = @event switch
        {
            InputEventMouseButton mouseButton => mouseButton.Pressed,
            _ => _isPressed
        };

        if (@event is InputEventMouseMotion mouseMotion)
        {
            GlobalPosition = mouseMotion.GlobalPosition;
        }
    }


    public override void _Process(double delta)
    {
        QueueRedraw();
    }
}