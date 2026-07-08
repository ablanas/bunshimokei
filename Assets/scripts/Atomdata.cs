using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 原子1個分のデータ（Unity非依存のPure C#モデル）。
/// 見た目や入力処理は持たず、状態だけを保持する。
/// </summary>
[System.Serializable]
public class AtomData
{
    [SerializeField] private int id;
    [SerializeField] private ElementDefinition element;
    [SerializeField] private Vector3 position;
    [SerializeField] private List<int> bondedAtomIds = new();

    public int Id => id;
    public ElementDefinition Element => element;

    public Vector3 Position
    {
        get => position;
        set => position = value;
    }

    public IReadOnlyList<int> BondedAtomIds => bondedAtomIds;

    public int UsedValence => bondedAtomIds.Count;

    public bool HasFreeValence => element != null && UsedValence < element.Valence;

    /// <summary>
    /// 結合を追加する。
    /// </summary>
    /// <param name="atomId">つなげる原子のid</param>
    /// <returns>つなげられたか</returns>
    public bool AddBond(int atomId)
    {
        if (!HasFreeValence)
            return false;

        if (bondedAtomIds.Contains(atomId))
            return false;

        bondedAtomIds.Add(atomId);
        return true;
    }

    /// <summary>
    /// 結合を削除する。
    /// </summary>
    /// <param name="atomId">はずす原子のid</param>
    /// <returns>はずせたか</returns>
    public bool RemoveBond(int atomId)
    {
        return bondedAtomIds.Remove(atomId);
    }
}