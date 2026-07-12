using Bunshimokei.Core.Definitions;
using Bunshimokei.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace Bunshimokei.Core.Services;

public sealed class ElementDatabase
{
    private readonly Dictionary<ElementSymbol, ElementDefinition> _elements = new();

    public void Add(ElementDefinition definition)
    {
        if (definition == null) throw new ArgumentNullException(nameof(definition));

        if (!_elements.TryAdd(definition.Symbol, definition)) throw new InvalidOperationException($"Duplicate element '{definition.Symbol}'.");
    }

    public void AddRange(IEnumerable<ElementDefinition> definitions)
    {
        if (definitions == null) throw new ArgumentNullException(nameof(definitions));

        foreach (ElementDefinition element in definitions)
        {
            Add(element);
        }
    }

    public ElementDefinition Get(ElementSymbol symbol)
    {
        if (!_elements.TryGetValue(symbol, out ElementDefinition definition))
        {
            throw new KeyNotFoundException(
                $"Element '{symbol}' was not found.");
        }

        return definition;
    }

    public bool TryGet(ElementSymbol symbol, out ElementDefinition definition) => _elements.TryGetValue(symbol, out definition);

    public IReadOnlyCollection<ElementDefinition> Definitions => _elements.Values;

    public int Count => _elements.Count;
}