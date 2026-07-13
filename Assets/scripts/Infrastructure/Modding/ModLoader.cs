using Bunshimokei.Core.Definitions;
using Bunshimokei.Core.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bunshimokei.Infrastructure.Modding;

public sealed class ModLoader
{
    private readonly ElementDefinitionDeserializer _elementDeserializer = new();

    public IReadOnlyList<ElementDefinition> LoadElements(string modDirectory)
    {
        if (modDirectory == null)
            throw new ArgumentNullException(nameof(modDirectory));

        string elementsDirectory = Path.Combine(
            modDirectory,
            "elements");

        if (!Directory.Exists(elementsDirectory))
            return Array.Empty<ElementDefinition>();

        var elements = new List<ElementDefinition>();

        foreach (string file in Directory.GetFiles(
            elementsDirectory,
            "*.json"))
        {
            string json = File.ReadAllText(file);

            ElementDefinition element = _elementDeserializer.Deserialize(json);

            elements.Add(element);
        }

        return elements;
    }
}