using System.Collections.Generic;
using UnityEngine;

using Bunshimokei.Core.Models;
using Bunshimokei.Core.Services;
using Bunshimokei.Core.Definitions;
using Bunshimokei.Core.ValueObjects;
using Bunshimokei.Core.Chemistry;

using Bunshimokei.Unity.Views;
using Bunshimokei.Unity.Settings;

namespace Bunshimokei.Unity.Controllers;

public sealed class MoleculeController : MonoBehaviour
{
    [SerializeField]
    private AtomView atomPrefab = null!;

    [SerializeField]
    private Transform atomParent = null!;

    [SerializeField]
    private MoleculeDisplaySettings displaySettings = null!;

    [SerializeField]
    private float snapDistancePm = 200f;


    private MoleculeData _molecule = null!;

    private SnapService _snapService = null!;


    private readonly Dictionary<AtomId, AtomView> _views = new();


    private AtomData? _draggingAtom;
    private AtomData? _snapTarget;


    private void Awake()
    {
        var bondValidator = new BondValidator();
        _molecule = new MoleculeData(bondValidator);

        _snapService = new SnapService(bondValidator);
    }


    public void AddAtom(
        ElementDefinition element,
        VectorPm3D position)
    {
        AtomData atom =
            _molecule.AddAtom(
                element,
                position);


        AtomView view =
            Instantiate(
                atomPrefab,
                ToUnityPosition(position),
                Quaternion.identity,
                atomParent);


        view.Initialize(
            atom,
            displaySettings);


        _views.Add(
            atom.Id,
            view);
    }


    public void BeginDrag(AtomId atomId)
    {
        ClearHighlight();

        _snapTarget = null;

        if (_molecule.Atoms.TryGetValue(
            atomId,
            out AtomData? atom))
        {
            _draggingAtom = atom;
        }
    }


    public void UpdateAtomPosition(
        AtomId atomId,
        Vector3 unityPosition)
    {
        if (_draggingAtom == null)
            return;


        VectorPm3D position =
            ToPmPosition(
                unityPosition);


        _molecule.MoveAtom(
            atomId,
            position);

        _views[atomId].SetPosition(
            unityPosition);


        AtomData? target =
            _snapService.FindSnapTarget(
                _molecule,
                _draggingAtom,
                position,
                snapDistancePm);


        UpdateHighlight(target);
    }


    public void EndDrag(
        AtomId atomId)
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
                atomId,
                snapped);


            _views[atomId]
                .SetPosition(
                    ToUnityPosition(snapped));


            // TODO:
            // Bond生成処理
        }


        ClearHighlight();

        _draggingAtom = null;
        _snapTarget = null;
    }


    private void UpdateHighlight(
        AtomData? target)
    {
        ClearHighlight();

        _snapTarget = target;

        if (target == null)
            return;


        _views[target.Id]
            .SetHighlight(true);
    }


    private void ClearHighlight()
    {
        foreach (AtomView view in _views.Values)
        {
            view.SetHighlight(false);
        }
    }


    private static Vector3 ToUnityPosition(
        VectorPm3D position)
    {
        return new Vector3(
            position.X,
            position.Y,
            position.Z);
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