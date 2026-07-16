using System;

using Bunshimokei.Core.Chemistry;
using Bunshimokei.Core.Enums;
using Bunshimokei.Core.Models;

namespace Bunshimokei.Core.Services;

/// <summary>
/// 結合の生成・管理を行うサービス。
/// 化学ルール判定はBondValidatorに委譲する。
/// </summary>
public sealed class BondService
{
    private readonly BondValidator _validator;


    public BondService(
        BondValidator validator)
    {
        _validator = validator
            ?? throw new ArgumentNullException(
                nameof(validator));
    }


    public BondData? CreateBond(
        MoleculeData molecule,
        AtomData atomA,
        AtomData atomB,
        BondOrder order = BondOrder.Single)
    {
        if (molecule == null)
            throw new ArgumentNullException(nameof(molecule));

        if (atomA == null)
            throw new ArgumentNullException(nameof(atomA));

        if (atomB == null)
            throw new ArgumentNullException(nameof(atomB));

        if (!_validator.CanCreate(
                molecule,
                atomA,
                atomB,
                order))
        {
            return null;
        }


        BondData bond = 
            molecule.AddBond(
                atomA.Id,
                atomB.Id,
                order);


        return bond;
    }


    public float CalculateBondDistance(
        AtomData atomA,
        AtomData atomB)
    {
        if (atomA == null)
            throw new ArgumentNullException(nameof(atomA));

        if (atomB == null)
            throw new ArgumentNullException(nameof(atomB));

        return atomA.Element.CovalentRadiusPm + atomB.Element.CovalentRadiusPm;
    }
}