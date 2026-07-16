using Bunshimokei.Core.Chemistry;
using Bunshimokei.Core.Models;
using Bunshimokei.Core.ValueObjects;
using System;

namespace Bunshimokei.Core.Services;

/// <summary>
/// スナップ候補の探索を行うサービス。
/// M1では距離のみで判定する。
/// M2以降でVSEPRによる角度判定を追加予定。
/// </summary>
public sealed class SnapService
{
    private readonly BondValidator _bondValidator;

    public SnapService(
        BondValidator bondValidator)
    {
        _bondValidator = bondValidator;
    }

    public AtomData? FindSnapTarget(
        MoleculeData molecule,
        AtomData draggedAtom,
        VectorPm3D draggedPosition,
        float snapDistancePm)
    {
        AtomData? closest = null;
        float closestDistanceSquared = snapDistancePm * snapDistancePm;

        foreach (AtomData atom in molecule.Atoms.Values)
        {
            if (atom.Id == draggedAtom.Id)
                continue;

            if (!_bondValidator.CanCreate(
                    molecule,
                    draggedAtom,
                    atom))
            {
                continue;
            }

            float distanceSquared =
                VectorPm3D.DistanceSquared(
                    draggedPosition,
                    atom.Position);

            if (distanceSquared < closestDistanceSquared)
            {
                closestDistanceSquared = distanceSquared;
                closest = atom;
            }
        }

        return closest;
    }
}