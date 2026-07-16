using Bunshimokei.Core.Enums;
using Bunshimokei.Core.ValueObjects;
using System;

namespace Bunshimokei.Core.Models;

    /// <summary>
    /// 結合1本分のデータ（Unity非依存のPure C#モデル）。
    /// M1では常にorder=1（単結合）のみを扱う。
    /// 二重結合・三重結合はM2以降で対応。
    /// </summary>
public sealed class BondData
{
    public BondId Id { get; }
    public AtomId AtomAId { get; }
    public AtomId AtomBId { get; }
    public BondOrder BondOrder { get; }

    public BondData(
        BondId id,
        AtomId atomAId, 
        AtomId atomBId, 
        BondOrder bondOrder = BondOrder.Single)
    {
        Id = id;

        if (atomAId == atomBId) throw new ArgumentException("A bond must connect two different atoms.");

        if (atomAId < atomBId)
        {
            AtomAId = atomAId;
            AtomBId = atomBId;
        }
        else
        {
            AtomAId = atomBId;
            AtomBId = atomAId;
        }

        BondOrder = bondOrder;
    }

    public bool Contains(AtomId atomId)
    {
        return AtomAId == atomId || AtomBId == atomId;
    }
}