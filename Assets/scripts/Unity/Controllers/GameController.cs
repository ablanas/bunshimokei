using Bunshimokei.Core.Chemistry;
using Bunshimokei.Core.Interfaces;
using Bunshimokei.Core.Models;
using Bunshimokei.Core.Services;
using Bunshimokei.Core.ValueObjects;
using Bunshimokei.Infrastructure.Modding;
using Bunshimokei.Unity.Controllers;
using Bunshimokei.Unity.Presenters;
using Bunshimokei.Unity.Services;
using Bunshimokei.Unity.Settings;
using System.IO;
using UnityEngine;

namespace Bunshimokei.Unity
{

    public sealed class GameController : MonoBehaviour
    {
        [SerializeField]
        private MoleculePresenter moleculePresenter = null!;

        [SerializeField]
        private MoleculeInputController moleculeInputController = null!;

        [SerializeField]
        private ElementPaletteController elementPaletteController = null!;

        [SerializeField]
        private AtomPlacementController atomPlacementController = null!;

        [SerializeField]
        private ElementPalettePresenter elementPalettePresenter = null!;

        [SerializeField]
        private MoleculeDisplaySettings displaySettings = null!;

        public ElementDatabase Database { get; private set; } = null!;

        public MoleculeData Molecule { get; private set; } = null!;

        private SelectedElementService _selectedElementService = null!;


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
                snapService,
                atomPlacementController);

            _selectedElementService = new SelectedElementService();


            atomPlacementController.Initialize(
                Molecule,
                _selectedElementService,
                displaySettings);


            elementPaletteController.Initialize(
                _selectedElementService,
                Database);

            elementPalettePresenter.Initialize(
                Database,
                elementPaletteController);

            // ---------- Event ----------
            moleculeInputController.SnapTargetChanged +=
                moleculePresenter.SetHighlight;

            // ----------Test----------
            //Molecule.AddAtom(
            //    Database.Get(new("H")),
            //    VectorPm3D.Zero);
        }


        private void OnDestroy()
        {
            moleculeInputController.SnapTargetChanged -=
                moleculePresenter.SetHighlight;
        }

    }
}