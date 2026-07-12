using System;

public readonly struct ColorRGBA
{
    public float R { get; }
    public float G { get; }
    public float B { get; }
    public float A { get; }

    public ColorRGBA(float r, float g, float b, float a = 1f)
    {
        if (r < 0f || r > 1f) throw new ArgumentOutOfRangeException(nameof(r));
        if (g < 0f || g > 1f) throw new ArgumentOutOfRangeException(nameof(g));
        if (b < 0f || b > 1f) throw new ArgumentOutOfRangeException(nameof(b));
        if (a < 0f || a > 1f) throw new ArgumentOutOfRangeException(nameof(a));

        R = r;
        G = g;
        B = b;
        A = a;
    }

    public override string ToString()
    {
        return $"({R}, {G}, {B}, {A})";
    }
}