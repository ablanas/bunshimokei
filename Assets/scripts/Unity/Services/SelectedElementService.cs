using Bunshimokei.Core.Definitions;

namespace Bunshimokei.Unity.Services;

public sealed class SelectedElementService
{
    public ElementDefinition? SelectedElement
    {
        get;
        private set;
    }

    public void Select(
        ElementDefinition element)
    {
        SelectedElement = element;
    }

    public void Clear()
    {
        SelectedElement = null;
    }
}