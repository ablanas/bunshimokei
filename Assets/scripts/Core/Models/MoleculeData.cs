using Bunshimokei.Core.Chemistry;
using Bunshimokei.Core.Definitions;
using Bunshimokei.Core.Enums;
using Bunshimokei.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace Bunshimokei.Core.Models;

public sealed class MoleculeData
{
    private readonly Dictionary<AtomId, AtomData> _atoms = new();

    private readonly List<BondData> _bonds = new();

    private int _nextAtomId;

    private readonly BondValidator _validator;


    public IReadOnlyDictionary<AtomId, AtomData> Atoms => _atoms;

    public IReadOnlyList<BondData> Bonds => _bonds;

    public MoleculeData (BondValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }


    public AtomData AddAtom(
        ElementDefinition element,
        VectorPm3D position)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element));

        var atom = new AtomData(
            new AtomId(_nextAtomId++),
            element,
            position);

        _atoms.Add(atom.Id, atom);

        return atom;
    }


    public AtomData? GetAtom(AtomId id)
    {
        _atoms.TryGetValue(id, out var atom);

        return atom;
    }


    public BondData AddBond(
        AtomId atomAId,
        AtomId atomBId,
        BondOrder order)
    {
        AtomData atomA = GetAtom(atomAId)
            ?? throw new ArgumentException($"Atom {atomAId} does not exist.");

        AtomData atomB = GetAtom(atomBId)
            ?? throw new ArgumentException($"Atom {atomBId} does not exist.");


        if (!_validator.CanCreate(
                this,
                atomA,
                atomB,
                order))
        {
            throw new InvalidOperationException(
                $"Cannot create bond between {atomAId} and {atomBId}.");
        }


        var bond = new BondData(
            atomAId,
            atomBId,
            order);

        _bonds.Add(bond);

        return bond;
    }


    public void RemoveAtom(AtomId id)
    {
        if (!_atoms.ContainsKey(id))
            return;

        _bonds.RemoveAll(
            b => b.Contains(id));

        _atoms.Remove(id);
    }
}