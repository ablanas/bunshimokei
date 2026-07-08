using UnityEngine;

/// <summary>
/// 結合1本分のデータ（Unity非依存のPure C#モデル）。
/// M1では常にorder=1（単結合）のみを扱う。
/// 二重結合・三重結合はM2以降で対応。
/// </summary>
[System.Serializable]
public class BondData
{
    [SerializeField] private int atomAId;
    [SerializeField] private int atomBId;
    [SerializeField] private int order = 1;

    public int AtomAId => atomAId;
    public int AtomBId => atomBId;
    public int Order => order;

    public BondData(int atomAId, int atomBId)
    {
        if (atomAId < atomBId)
        {
            this.atomAId = atomAId;
            this.atomBId = atomBId;
        }
        else
        {
            this.atomAId = atomBId;
            this.atomBId = atomAId;
        }
    }
}