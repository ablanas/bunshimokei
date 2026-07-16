using Bunshimokei.Core.Enums;
using Bunshimokei.Core.Interfaces;
using Bunshimokei.Core.Models;
using System.Linq;

namespace Bunshimokei.Core.Chemistry;

public sealed class BondValidator : IBondValidator
{
    public bool CanCreate(
        MoleculeData molecule,
        AtomData atomA,
        AtomData atomB,
        BondOrder order = BondOrder.Single)
    {
        if (molecule == null)
            return false;

        if (atomA == null || atomB == null)
            return false;

        if (atomA.Id == atomB.Id)
            return false;


        // すでに結合しているか
        if (molecule.Bonds.Any(
            b => b.Contains(atomA.Id) && b.Contains(atomB.Id)))
        {
            return false;
        }


        // 価数チェック
        if (!HasFreeValence(molecule, atomA, order))
        {
            return false;
        }

        if (!HasFreeValence(molecule, atomB, order))
        {
            return false;
        }

        return true;
    }


    private static bool HasFreeValence(
        MoleculeData molecule,
        AtomData atom,
        BondOrder newBondOrder)
    {
        int usedValence = molecule.Bonds
            .Where(b => b.Contains(atom.Id))
            .Sum(b => (int)b.BondOrder);


        int requiredValence = (int)newBondOrder;


        return usedValence + requiredValence
            <= atom.Element.Valence;
    }
}