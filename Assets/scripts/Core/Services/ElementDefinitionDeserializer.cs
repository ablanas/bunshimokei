using Bunshimokei.Core.Definitions;
using Bunshimokei.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Bunshimokei.Core.Serialization;

public sealed class ElementDefinitionDeserializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public IReadOnlyList<ElementDefinition> Deserialize(string json)
    {
        if (json == null)
            throw new ArgumentNullException(nameof(json));

        var rawElements = JsonSerializer.Deserialize<List<ElementDefinitionJson>>(json, _options);

        if (rawElements == null)
        {
            throw new InvalidOperationException(
                "Failed to deserialize element definitions.");
        }

        var elements = new List<ElementDefinition>();

        foreach (var raw in rawElements)
        {
            elements.Add(CreateDefinition(raw));
        }

        return elements;
    }

    private static ElementDefinition CreateDefinition(ElementDefinitionJson raw)
    {
        return new ElementDefinition(
            new ElementSymbol(raw.Symbol),
            raw.DisplayName,
            raw.AtomicNumber,
            raw.Valence,
            new ColorRGBA(
                raw.Color?.R ?? 1,
                raw.Color?.G ?? 1,
                raw.Color?.B ?? 1,
                raw.Color?.A ?? 1),
            raw.CovalentRadiusPm,
            raw.VanDerWaalsRadiusPm,
            raw.StickDisplayScale);
    }


    private sealed class ElementDefinitionJson
    {
        public string Symbol { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public int AtomicNumber { get; set; }
        public int Valence { get; set; }
        public ColorData Color { get; set; } = new ColorData();
        public float CovalentRadiusPm { get; set; }
        public float VanDerWaalsRadiusPm { get; set; }
        public float StickDisplayScale { get; set; } = 0.25f;
    }

    private sealed class ColorData
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }
    }
}