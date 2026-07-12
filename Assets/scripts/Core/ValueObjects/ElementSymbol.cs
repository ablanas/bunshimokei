using System;

namespace Bunshimokei.Core.ValueObjects;

public readonly struct ElementSymbol
{
    public string Value { get; }
    public ElementSymbol(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Element symbol cannot be null or whitespace.", nameof(value));
        
        Value = value;
    }
    public override string ToString()
    {
        return Value;
    }
}