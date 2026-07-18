using Bunshimokei.Core.Definitions;
using System;

namespace Bunshimokei.Unity.Services
{
    public sealed class SelectedElementService
    {
        public ElementDefinition? SelectedElement
        {
            get;
            private set;
        }


        public event Action<ElementDefinition?>?
            SelectedElementChanged;


        public void Select(
            ElementDefinition element)
        {
            SelectedElement = element;

            SelectedElementChanged?
                .Invoke(element);
        }


        public void Clear()
        {
            SelectedElement = null;

            SelectedElementChanged?
                .Invoke(null);
        }
    }
}