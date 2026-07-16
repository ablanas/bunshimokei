using System;

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

    public static float Distance(
        VectorPm3D a,
        VectorPm3D b)
    {
        return (a - b).Length;
    }

    public static float DistanceSquared(
        VectorPm3D a,
        VectorPm3D b)
    {
        return (a - b).LengthSquared;
    }

    public readonly float LengthSquared =>
        X * X + Y * Y + Z * Z;

    public readonly float Length =>
        MathF.Sqrt(LengthSquared);

    public readonly VectorPm3D Normalized
    {
        get
        {
            if (LengthSquared == 0f) return Zero;

            return this / Length;
        }
    }

    public static VectorPm3D Zero { get; } = new(0f, 0f, 0f);

    public static float Dot(VectorPm3D a, VectorPm3D b)
    {
        return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    }

    public static VectorPm3D operator +(VectorPm3D a, VectorPm3D b)
    {
        return new VectorPm3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    public static VectorPm3D operator -(VectorPm3D a, VectorPm3D b)
    {
        return new VectorPm3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    public static VectorPm3D operator *(VectorPm3D a, float scalar)
    {
        return new VectorPm3D(a.X * scalar, a.Y * scalar, a.Z * scalar);
    }

    public static VectorPm3D operator *(float scalar, VectorPm3D a)
    {
        return a * scalar;
    }

    public static VectorPm3D operator /(VectorPm3D a, float scalar)
    {
        if (scalar == 0f)
            throw new DivideByZeroException();
        return new VectorPm3D(a.X / scalar, a.Y / scalar, a.Z / scalar);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}