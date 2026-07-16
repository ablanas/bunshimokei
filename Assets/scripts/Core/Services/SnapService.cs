using Bunshimokei.Core.Interfaces;
using Bunshimokei.Core.Models;
using Bunshimokei.Core.ValueObjects;
using Bunshimokei.Core.Enums;

namespace Bunshimokei.Core.Services;

/// <summary>
/// スナップ候補の探索を行うサービス。
/// M1では距離のみで判定する。
/// M2以降でVSEPRによる角度判定を追加予定。
/// </summary>
public sealed class SnapService
{
    private readonly IBondValidator _bondValidator;

    public SnapService(
        IBondValidator bondValidator)
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
                    atom,
                    BondOrder.Single))
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

    public VectorPm3D CalculateSnapPosition(
    AtomData draggedAtom,
    AtomData targetAtom,
    VectorPm3D currentPosition)
    {
        float idealDistance =
            draggedAtom.Element.CovalentRadiusPm
            + targetAtom.Element.CovalentRadiusPm;


        VectorPm3D direction =
            currentPosition - targetAtom.Position;


        if (direction.LengthSquared < 1e-6f)
        {
            direction = new VectorPm3D(
                1f,
                0f,
                0f);
        }
        else
        {
            direction = direction.Normalized;
        }


        return targetAtom.Position
            + direction * idealDistance;
    }
}