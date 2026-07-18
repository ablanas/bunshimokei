using UnityEngine;

using Bunshimokei.Core.Definitions;
using Bunshimokei.Core.Models;
using Bunshimokei.Core.ValueObjects;
using Bunshimokei.Unity.Services;

namespace Bunshimokei.Unity.Controllers
{

    public sealed class AtomPlacementController : MonoBehaviour
    {
        private MoleculeData _molecule = null!;

        private SelectedElementService _selectedElementService = null!;


        public void Initialize(
            MoleculeData molecule,
            SelectedElementService selectedElementService)
        {
            _molecule = molecule;
            _selectedElementService = selectedElementService;
        }


        public bool PlaceAtom(
            Vector3 worldPosition)
        {
            ElementDefinition? element =
                _selectedElementService.SelectedElement;

            if (element == null)
            {
                return false;
            }


            _molecule.AddAtom(
                element,
                ToPmPosition(worldPosition));

            return true;
        }


        private static VectorPm3D ToPmPosition(
            Vector3 position)
        {
            return new VectorPm3D(
                position.x,
                position.y,
                position.z);
        }
    }
}