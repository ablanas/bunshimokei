using Bunshimokei.Core.Chemistry;
using Bunshimokei.Core.Interfaces;
using Bunshimokei.Core.Models;
using Bunshimokei.Core.Services;
using Bunshimokei.Infrastructure.Modding;
using Bunshimokei.Unity.Controllers;
using Bunshimokei.Unity.Presenters;
using System.IO;
using UnityEngine;

namespace Bunshimokei.Unity;

public sealed class GameController : MonoBehaviour
{
    [SerializeField]
    private MoleculePresenter moleculePresenter = null!;

    [SerializeField]
    private MoleculeInputController moleculeInputController = null!;


    public ElementDatabase Database { get; private set; } = null!;

    public MoleculeData Molecule { get; private set; } = null!;


    private void Awake()
    {
        // ---------- Mod ----------
        Database = new ElementDatabase();

        ModManager modManager =
            new ModManager();

        modManager.LoadMods(
            new[]
            {
            Path.Combine(
                Application.streamingAssetsPath,
                "Mods",
                "Vanilla")
            },
            Database);

        // ---------- Core ----------
        IBondValidator validator =
            new BondValidator();

        SnapService snapService =
            new SnapService(validator);

        Molecule =
            new MoleculeData(validator);

        // ---------- Unity ----------
        moleculePresenter.Initialize(
            Molecule);

        moleculeInputController.Initialize(
            Molecule,
            snapService);

        // ---------- Event ----------
        moleculeInputController.SnapTargetChanged +=
            moleculePresenter.SetHighlight;
    }


    private void OnDestroy()
    {
        moleculeInputController.SnapTargetChanged -=
            moleculePresenter.SetHighlight;
    }

}