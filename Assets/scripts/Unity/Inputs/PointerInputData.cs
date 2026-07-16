using UnityEngine;

namespace Bunshimokei.Unity.Inputs;

public readonly struct PointerInputData
{
    public Vector3 WorldPosition { get; }

    public GameObject? Target { get; }

    public PointerInputData(
        Vector3 worldPosition,
        GameObject? target)
    {
        WorldPosition = worldPosition;
        Target = target;
    }
}