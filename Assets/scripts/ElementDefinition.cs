using UnityEngine;

/// <summary>
/// 元素1種類分のデータ定義。
/// Project上で「Create > MoleculeBuilder > Element Definition」から
/// H, C, N, O, S, P, F, Cl, Br, I などを1つずつアセットとして作成する。
/// </summary>
[CreateAssetMenu(fileName = "NewElement", menuName = "MoleculeBuilder/Element Definition")]
public class ElementDefinition : ScriptableObject
{
    [Header("基本情報")]
    [SerializeField] private string symbol;        // 例: "H", "C", "O"
    [SerializeField] private string displayName;   // 例: "Hydrogen", "水素"
    [SerializeField] private int atomicNumber;

    [Header("化学的性質")]
    [Tooltip("標準的な価数（結合本数の上限）。M1では単結合のみなのでそのまま結合可能数として扱う。")]
    [SerializeField] private int valence;

    [Header("見た目 (CPK配色を目安に)")]
    [SerializeField] private Color color = Color.white;

    [Header("実サイズ（pm、参考値）")]
    [SerializeField] private float covalentRadiusPm;      // 結合距離の計算・Ball-and-Stick表示に使用
    [SerializeField] private float vanDerWaalsRadiusPm;   // Space-Filling表示に使用

    [Header("表示調整")]
    [Tooltip("Ball-and-Stick時、共有結合半径に対してどれくらい縮小して表示するか(0〜1)")]
    [SerializeField, Range(0f, 1f)] private float stickDisplayScale = 0.25f;

    public string Symbol => symbol;
    public string DisplayName => displayName;
    public int AtomicNumber => atomicNumber;
    public int Valence => valence;
    public Color Color => color;
    public float CovalentRadiusPm => covalentRadiusPm;
    public float VanDerWaalsRadiusPm => vanDerWaalsRadiusPm;
    public float StickDisplayScale => stickDisplayScale;
}