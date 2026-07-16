using System;
using System.Collections.Generic;

using Bunshimokei.Core.Definitions;
using Bunshimokei.Core.Enums;
using Bunshimokei.Core.Events;
using Bunshimokei.Core.Interfaces;
using Bunshimokei.Core.ValueObjects;


namespace Bunshimokei.Core.Models;


public sealed class MoleculeData
{
    private readonly Dictionary<AtomId, AtomData> _atoms = new();

    private readonly List<BondData> _bonds = new();


    private int _nextAtomId;

    private int _nextBondId;


    private readonly IBondValidator _bondValidator;



    public IReadOnlyDictionary<AtomId, AtomData> Atoms
        => _atoms;


    public IReadOnlyList<BondData> Bonds
        => _bonds;



    public event EventHandler<AtomAddedEventArgs>? AtomAdded;

    public event EventHandler<BondAddedEventArgs>? BondAdded;

    public event EventHandler<AtomMovedEventArgs>? AtomMoved;



    public MoleculeData(
        IBondValidator bondValidator)
    {
        _bondValidator =
            bondValidator
            ?? throw new ArgumentNullException(
                nameof(bondValidator));
    }



    public AtomData AddAtom(
        ElementDefinition element,
        VectorPm3D position)
    {
        if (element == null)
            throw new ArgumentNullException(
                nameof(element));


        AtomData atom =
            new AtomData(
                new AtomId(_nextAtomId++),
                element,
                position);



        _atoms.Add(
            atom.Id,
            atom);



        AtomAdded?.Invoke(
            this,
            new AtomAddedEventArgs(atom));



        return atom;
    }



    public AtomData? GetAtom(
        AtomId id)
    {
        _atoms.TryGetValue(
            id,
            out AtomData? atom);


        return atom;
    }



    public BondData AddBond(
        AtomId atomAId,
        AtomId atomBId,
        BondOrder order)
    {
        AtomData atomA =
            GetAtom(atomAId)
            ?? throw new ArgumentException(
                $"Atom {atomAId} does not exist.");


        AtomData atomB =
            GetAtom(atomBId)
            ?? throw new ArgumentException(
                $"Atom {atomBId} does not exist.");



        if (!_bondValidator.CanCreate(
                this,
                atomA,
                atomB,
                order))
        {
            throw new InvalidOperationException(
                $"Cannot create bond between {atomAId} and {atomBId}.");
        }



        BondData bond =
            new BondData(
                new BondId(_nextBondId++),
                atomAId,
                atomBId,
                order);



        _bonds.Add(bond);



        BondAdded?.Invoke(
            this,
            new BondAddedEventArgs(bond));



        return bond;
    }



    public void RemoveAtom(
        AtomId id)
    {
        if (!_atoms.ContainsKey(id))
            return;



        _bonds.RemoveAll(
            b => b.Contains(id));



        _atoms.Remove(id);
    }



    public bool MoveAtom(
        AtomId atomId,
        VectorPm3D position)
    {
        if (!_atoms.TryGetValue(
                atomId,
                out AtomData? atom))
        {
            return false;
        }



        atom.Position = position;



        AtomMoved?.Invoke(
            this,
            new AtomMovedEventArgs(
                atomId,
                position));



        return true;
    }
}