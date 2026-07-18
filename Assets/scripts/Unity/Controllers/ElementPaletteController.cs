using UnityEngine;

using Bunshimokei.Core.Definitions;
using Bunshimokei.Core.Services;
using Bunshimokei.Core.ValueObjects;
using Bunshimokei.Unity.Services;

namespace Bunshimokei.Unity.Controllers
{

    public sealed class ElementPaletteController : MonoBehaviour
    {
        private SelectedElementService _selectedElementService = null!;

        private ElementDatabase _database = null!;


        public void Initialize(
            SelectedElementService selectedElementService,
            ElementDatabase database)
        {
            _selectedElementService = selectedElementService;
            _database = database;
        }


        public void SelectElement(
            string symbol)
        {
            ElementSymbol elementSymbol =
                new ElementSymbol(symbol);


            ElementDefinition? element =
                _database.Get(elementSymbol);


            if (element == null)
            {
                Debug.LogWarning(
                    $"Element '{symbol}' not found.");
                return;
            }


            _selectedElementService.Select(
                element);
        }


        public void ClearSelection()
        {
            _selectedElementService.Clear();
        }
    }
}