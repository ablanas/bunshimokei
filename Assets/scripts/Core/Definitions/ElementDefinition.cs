using System.Text.Json.Serialization;
using System;

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