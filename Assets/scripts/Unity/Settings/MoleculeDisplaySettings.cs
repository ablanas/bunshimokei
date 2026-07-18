using UnityEngine;

namespace Bunshimokei.Unity.Settings;

[CreateAssetMenu(
    fileName = "MoleculeDisplaySettings",
    menuName = "Bunshimokei/Molecule Display Settings")]
public sealed class MoleculeDisplaySettings : ScriptableObject
{
    [Header("Scale")]
    [Min(0.000001f)]
    public float PmToUnityScale = 0.001f;

    [Min(0.01f)]
    public float AtomRadiusScale = 1.0f;

    [Header("Bond")]
    [Min(0.001f)]
    public float BondRadiusPm = 20f;

    public float ConvertPmToUnity(float pm)
    {
        return pm * PmToUnityScale;
    }

    public float ConvertUnityToPm(float unity)
    {
        return unity / PmToUnityScale;
    }

    public float GetBondRadiusUnity()
    {
        return ConvertPmToUnity(BondRadiusPm);
    }
}