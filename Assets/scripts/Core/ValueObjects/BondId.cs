using System;

namespace Bunshimokei.Core.ValueObjects;

public readonly struct BondId : IEquatable<BondId>, IComparable<BondId>
{
    public int Value { get; }

    public BondId(int value)
    {
        if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }

    public readonly override string ToString() => Value.ToString();

    public readonly bool Equals(BondId other) => Value == other.Value;
    public readonly override bool Equals(object? obj) => obj is BondId other && Equals(other);
    public readonly override int GetHashCode() => Value.GetHashCode();
    public readonly int CompareTo(BondId other) => Value.CompareTo(other.Value);

    public static bool operator ==(BondId left, BondId right) => left.Equals(right);
    public static bool operator !=(BondId left, BondId right) => !left.Equals(right);
    public static bool operator <(BondId left, BondId right) => left.CompareTo(right) < 0;
    public static bool operator >(BondId left, BondId right) => left.CompareTo(right) > 0;
    public static bool operator <=(BondId left, BondId right) => left.CompareTo(right) <= 0;
    public static bool operator >=(BondId left, BondId right) => left.CompareTo(right) >= 0;
}
