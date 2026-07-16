using Bunshimokei.Core.Enums;
using Bunshimokei.Core.Models;

namespace Bunshimokei.Core.Interfaces;

public interface IBondValidator
{
    bool CanCreate(
        MoleculeData molecule,
        AtomData atomA,
        AtomData atomB,
        BondOrder order);
}