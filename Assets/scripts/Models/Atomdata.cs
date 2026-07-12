using System;

/// <summary>
/// 原子1個分のデータ（Unity非依存のPure C#モデル）。
/// 見た目や入力処理は持たず、状態だけを保持する。
/// </summary>
public sealed class AtomData
{
    public AtomId Id { get; }
    public ElementDefinition Element { get; }
    public VectorPm3D Position { get; set; }

    public AtomData(
        AtomId id,
        ElementDefinition element,
        VectorPm3D position)
    {
        Id = id;
        Element = element ?? throw new ArgumentNullException(nameof(element));
        Position = position;
    }
}