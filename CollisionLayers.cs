namespace LebJam;

public enum CollisionLayers : uint
{
    Ground = 1,
    Player = 2,
    Enemy = 4,
    PlayerAttacks = 8,
    PassThrough = 32
}