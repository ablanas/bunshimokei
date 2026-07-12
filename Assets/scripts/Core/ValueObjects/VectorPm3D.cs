namespace Bunshimokei.Core.ValueObjects;

public readonly struct VectorPm3D
{
    public float X { get; }
    public float Y { get; }
    public float Z { get; }

    public VectorPm3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}