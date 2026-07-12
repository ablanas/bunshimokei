using System;
using Bunshimokei.Core.ValueObjects;

namespace Bunshimokei.Core.ValueObjects;

public readonly struct AtomId
{
    public int Value { get; }

    public AtomId(int value)
    {
        if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }

    public override string ToString() => Value.ToString();
}
