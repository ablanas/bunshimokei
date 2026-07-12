/// <summary>
/// 元素1種類分のデータ定義。
/// </summary>
public sealed class ElementDefinition
{
    public string Symbol { get; }
    public string DisplayName { get; }
    public int AtomicNumber { get; }
    public int Valence { get; }
    public ColorRGBA Color { get; }
    public float CovalentRadiusPm { get; }
    public float VanDerWaalsRadiusPm { get; }
    public float StickDisplayScale { get; }

    [JsonConstructor]
    public ElementDefinition(
        string symbol,
        string displayName,
        int atomicNumber,
        int valence,
        ColorRGBA color,
        float covalentRadiusPm,
        float vanDerWaalsRadiusPm,
        float stickDisplayScale = 0.25f)
    {
        if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException("Symbol is required.", nameof(symbol));
        if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Display name is required.", nameof(displayName));
        if (atomicNumber <= 0) throw new ArgumentOutOfRangeException(nameof(atomicNumber));
        if (valence < 0) throw new ArgumentOutOfRangeException(nameof(valence));
        if (covalentRadiusPm < 0) throw new ArgumentOutOfRangeException(nameof(covalentRadiusPm));
        if (vanDerWaalsRadiusPm < 0) throw new ArgumentOutOfRangeException(nameof(vanDerWaalsRadiusPm));
        if (stickDisplayScale < 0) throw new ArgumentOutOfRangeException(nameof(stickDisplayScale));

        Symbol = symbol;
        DisplayName = displayName;
        AtomicNumber = atomicNumber;
        Valence = valence;
        Color = color;
        CovalentRadiusPm = covalentRadiusPm;
        VanDerWaalsRadiusPm = vanDerWaalsRadiusPm;
        StickDisplayScale = stickDisplayScale;
    }
}

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