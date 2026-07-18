using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Bunshimokei.Core.Events;
using Bunshimokei.Core.Models;
using Bunshimokei.Core.ValueObjects;

using Bunshimokei.Unity.Settings;
using Bunshimokei.Unity.Views;


namespace Bunshimokei.Unity.Presenters
{


    public sealed class MoleculePresenter : MonoBehaviour
    {
        [SerializeField]
        private AtomView atomPrefab = null!;


        [SerializeField]
        private BondView bondPrefab = null!;


        [SerializeField]
        private Transform atomParent = null!;


        [SerializeField]
        private Transform bondParent = null!;


        [SerializeField]
        private MoleculeDisplaySettings displaySettings = null!;



        private MoleculeData? _molecule;


        private readonly Dictionary<AtomId, AtomView> _atomViews = new();

        private readonly Dictionary<BondId, BondView> _bondViews = new();

        private readonly List<BondData> _pendingBonds = new();

        private AtomId? _highlightedAtom;


        private bool _initialized;



        public void Initialize(
            MoleculeData molecule)
        {
            if (_initialized)
                return;


            _molecule = molecule;


            _molecule.AtomAdded += OnAtomAdded;
            _molecule.BondAdded += OnBondAdded;
            _molecule.AtomMoved += OnAtomMoved;
            _molecule.AtomRemoved += OnAtomRemoved;
            _molecule.BondRemoved += OnBondRemoved;



            // 初期同期
            foreach (AtomData atom in _molecule.Atoms.Values)
            {
                CreateAtomView(atom);
            }


            foreach (BondData bond in _molecule.Bonds.Values)
            {
                CreateBondView(bond);
            }


            _initialized = true;
        }



        private void OnDestroy()
        {
            if (_molecule == null)
                return;


            _molecule.AtomAdded -= OnAtomAdded;
            _molecule.BondAdded -= OnBondAdded;
            _molecule.AtomMoved -= OnAtomMoved;
            _molecule.AtomRemoved -= OnAtomRemoved;
            _molecule.BondRemoved -= OnBondRemoved;
        }



        private void OnAtomAdded(
            object? sender,
            AtomAddedEventArgs e)
        {
            CreateAtomView(e.Atom);
        }



        private void OnBondAdded(
            object? sender,
            BondAddedEventArgs e)
        {
            CreateBondView(e.Bond);
        }



        private void OnAtomMoved(
            object? sender,
            AtomMovedEventArgs e)
        {
            if (!_atomViews.TryGetValue(
                    e.AtomId,
                    out AtomView? view))
            {
                return;
            }


            view.SetPosition(
                ToUnityPosition(e.Position));
        }



        private void OnAtomRemoved(
            object? sender,
            AtomRemovedEventArgs e)
        {
            if (_highlightedAtom == e.AtomId)
            {
                _highlightedAtom = null;
            }

            if (!_atomViews.TryGetValue(
                    e.AtomId,
                    out AtomView? view))
            {
                return;
            }


            Destroy(view.gameObject);

            _atomViews.Remove(e.AtomId);
        }



        private void OnBondRemoved(
            object? sender,
            BondRemovedEventArgs e)
        {
            // 作成待ち結合も削除
            _pendingBonds.RemoveAll(
                b => b.Id == e.BondId);


            if (!_bondViews.TryGetValue(
                    e.BondId,
                    out BondView? view))
            {
                return;
            }


            Destroy(view.gameObject);

            _bondViews.Remove(e.BondId);
        }



        private void CreateAtomView(
            AtomData atom)
        {
            Debug.Log(
                $"Atom Position(pm): {atom.Position.X}, {atom.Position.Y}, {atom.Position.Z}");

            Vector3 unityPosition =
                ToUnityPosition(atom.Position);

            Debug.Log(
                $"Unity Position: {unityPosition}");

            AtomView view =
                Instantiate(
                    atomPrefab,
                    unityPosition,
                    Quaternion.identity,
                    atomParent);


            view.Initialize(
                atom,
                displaySettings);


            _atomViews.Add(
                atom.Id,
                view);


            TryCreatePendingBonds();
        }



        private bool CreateBondView(
            BondData bond)
        {
            if (_bondViews.ContainsKey(bond.Id))
                return true;


            if (!_atomViews.TryGetValue(
                    bond.AtomAId,
                    out AtomView? atomA))
            {
                AddPendingBond(bond);
                return false;
            }


            if (!_atomViews.TryGetValue(
                    bond.AtomBId,
                    out AtomView? atomB))
            {
                AddPendingBond(bond);
                return false;
            }



            BondView view =
                Instantiate(
                    bondPrefab,
                    bondParent);



            view.Initialize(
                atomA.transform,
                atomB.transform,
                bond.BondOrder);



            _bondViews.Add(
                bond.Id,
                view);


            return true;
        }



        private void AddPendingBond(
            BondData bond)
        {
            if (_pendingBonds.Any(
                    b => b.Id == bond.Id))
            {
                return;
            }


            _pendingBonds.Add(bond);
        }



        private void TryCreatePendingBonds()
        {
            foreach (BondData bond in _pendingBonds.ToArray())
            {
                if (CreateBondView(bond))
                {
                    _pendingBonds.Remove(bond);
                }
            }
        }


        public void SetHighlight(
            AtomId? atomId)
        {
            if (_highlightedAtom != null &&
                _atomViews.TryGetValue(
                    _highlightedAtom.Value,
                    out AtomView? previous))
            {
                previous.SetHighlight(false);
            }

            if (_highlightedAtom == atomId)
            {
                return;
            }

            _highlightedAtom = atomId;

            if (atomId != null &&
                _atomViews.TryGetValue(
                    atomId.Value,
                    out AtomView? current))
            {
                current.SetHighlight(true);
            }
        }
        private Vector3 ToUnityPosition(
            VectorPm3D position)
        {
            return new Vector3(
                displaySettings.ConvertPmToUnity(position.X),
                displaySettings.ConvertPmToUnity(position.Y),
                displaySettings.ConvertPmToUnity(position.Z));
        }
    }
}