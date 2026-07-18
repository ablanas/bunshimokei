using UnityEngine;

using Bunshimokei.Core.Definitions;
using Bunshimokei.Core.Models;
using Bunshimokei.Core.ValueObjects;
using Bunshimokei.Unity.Services;
using Bunshimokei.Unity.Settings;

namespace Bunshimokei.Unity.Controllers
{

    public sealed class AtomPlacementController : MonoBehaviour
    {
        private MoleculeData _molecule = null!;

        private SelectedElementService _selectedElementService = null!;

        private MoleculeDisplaySettings _displaySettings = null!;


        public void Initialize(
            MoleculeData molecule,
            SelectedElementService selectedElementService,
            MoleculeDisplaySettings displaySettings)
        {
            _molecule = molecule;
            _selectedElementService = selectedElementService;
            _displaySettings = displaySettings;
        }


        public bool PlaceAtom(
            Vector3 worldPosition)
        {
            Debug.Log(
                $"PlaceAtom Position : {worldPosition}");

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


        private VectorPm3D ToPmPosition(
            Vector3 position)
        {
            return new VectorPm3D(
                _displaySettings.ConvertUnityToPm(position.x),
                _displaySettings.ConvertUnityToPm(position.y),
                _displaySettings.ConvertUnityToPm(position.z));
        }
    }
}