using Bunshimokei.Core.Services;
using System;
using System.Collections.Generic;

namespace Bunshimokei.Infrastructure.Modding;

public sealed class ModManager
{
    private readonly ModLoader _loader = new();

    public void LoadMods(
        IEnumerable<string> modDirectories,
        ElementDatabase database)
    {
        if (modDirectories == null)
            throw new ArgumentNullException(nameof(modDirectories));

        if (database == null)
            throw new ArgumentNullException(nameof(database));

        foreach (string modDirectory in modDirectories)
        {
            var elements =
                _loader.LoadElements(modDirectory);

            database.AddRange(elements);
        }
    }
}