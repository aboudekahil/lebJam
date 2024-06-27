using Godot;

namespace LebJam.player.scripts;

public partial class PlayerCam : Camera2D
{
    [Export] private const float MaxDistance = 100;
    private Vector2 _centerPos;
    private Vector2 _maxDistanceVec;
    private float _targetDistance;

    public override void _Ready()
    {
        _centerPos = Position;
        _maxDistanceVec = new Vector2(MaxDistance, MaxDistance);
    }

    public override void _Process(double delta)
    {
        var direction = _centerPos.DirectionTo(GetLocalMousePosition());
        var targetPos = _centerPos + direction * _targetDistance;

        targetPos = targetPos.Clamp(_centerPos - _maxDistanceVec,
            _centerPos + _maxDistanceVec);

        Position = targetPos;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            _targetDistance =
                _centerPos.DistanceTo(GetLocalMousePosition()) / 2;
        }
    }
}