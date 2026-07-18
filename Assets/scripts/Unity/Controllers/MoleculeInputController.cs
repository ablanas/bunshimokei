using System;
using UnityEngine;

using Bunshimokei.Core.Enums;
using Bunshimokei.Core.Models;
using Bunshimokei.Core.Services;
using Bunshimokei.Core.ValueObjects;
using Bunshimokei.Unity.Settings;

namespace Bunshimokei.Unity.Controllers
{

    public sealed class MoleculeInputController : MonoBehaviour
    {
        [SerializeField]
        private float snapDistancePm = 200f;


        private MoleculeData _molecule = null!;

        private SnapService _snapService = null!;


        private AtomData? _draggingAtom;

        private AtomData? _snapTarget;


        public event Action<AtomId?>? SnapTargetChanged;

        private AtomPlacementController _atomPlacementController = null!;

        private  MoleculeDisplaySettings _displaySettings = null!;


        public void Initialize(
            MoleculeData molecule,
            SnapService snapService,
            AtomPlacementController atomPlacementController,
            MoleculeDisplaySettings displaySettings)
        {
            _molecule = molecule;
            _snapService = snapService;
            _atomPlacementController = atomPlacementController;
            _displaySettings = displaySettings;
        }


        public void BeginDrag(
            AtomId atomId)
        {
            ClearHighlight();

            if (_molecule.Atoms.TryGetValue(
                    atomId,
                    out AtomData? atom))
            {
                _draggingAtom = atom;
            }
        }


        public void UpdateDrag(
            Vector3 unityPosition)
        {
            if (_draggingAtom == null)
                return;


            VectorPm3D position =
                ToPmPosition(unityPosition);


            _molecule.MoveAtom(
                _draggingAtom.Id,
                position);


            AtomData? target =
                _snapService.FindSnapTarget(
                    _molecule,
                    _draggingAtom,
                    position,
                    snapDistancePm);

            Debug.Log(
                $"MoleculeInput Position: {unityPosition}");


            SetSnapTarget(target);
        }


        public void EndDrag()
        {
            if (_draggingAtom == null)
                return;


            if (_snapTarget != null)
            {
                VectorPm3D snapped =
                    _snapService.CalculateSnapPosition(
                        _draggingAtom,
                        _snapTarget,
                        _draggingAtom.Position);


                _molecule.MoveAtom(
                    _draggingAtom.Id,
                    snapped);


                _molecule.AddBond(
                    _draggingAtom.Id,
                    _snapTarget.Id,
                    BondOrder.Single);
            }


            ClearHighlight();

            _draggingAtom = null;
        }


        private void SetSnapTarget(
            AtomData? target)
        {
            if (_snapTarget?.Id == target?.Id)
                return;


            _snapTarget = target;

            SnapTargetChanged?.Invoke(
                target?.Id);
        }


        private void ClearHighlight()
        {
            SetSnapTarget(null);
        }

        public void PlaceAt(
    Vector3 worldPosition)
        {
            _atomPlacementController.PlaceAtom(
                worldPosition);
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