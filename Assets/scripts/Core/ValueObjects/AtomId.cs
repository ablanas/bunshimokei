using System;

namespace Bunshimokei.Core.ValueObjects;

public readonly struct AtomId : IEquatable<AtomId>, IComparable<AtomId>
{
    public int Value { get; }

    public AtomId(int value)
    {
        if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }

    public readonly override string ToString() => Value.ToString();

    public readonly bool Equals(AtomId other) => Value == other.Value;
    public readonly override bool Equals(object? obj) => obj is AtomId other && Equals(other);
    public readonly override int GetHashCode() => Value.GetHashCode();
    public readonly int CompareTo(AtomId other) => Value.CompareTo(other.Value);

    public static bool operator ==(AtomId left, AtomId right) => left.Equals(right);
    public static bool operator !=(AtomId left, AtomId right) => !left.Equals(right);
    public static bool operator <(AtomId left, AtomId right) => left.CompareTo(right) < 0;
    public static bool operator >(AtomId left, AtomId right) => left.CompareTo(right) > 0;
    public static bool operator <=(AtomId left, AtomId right) => left.CompareTo(right) <= 0;
    public static bool operator >=(AtomId left, AtomId right) => left.CompareTo(right) >= 0;
}
