using UnityEngine;

using Bunshimokei.Core.Chemistry;
using Bunshimokei.Core.Interfaces;
using Bunshimokei.Core.Models;
using Bunshimokei.Core.Services;

using Bunshimokei.Unity.Controllers;
using Bunshimokei.Unity.Presenters;

namespace Bunshimokei.Unity;

public sealed class GameController : MonoBehaviour
{
    [SerializeField]
    private MoleculePresenter moleculePresenter = null!;

    [SerializeField]
    private MoleculeInputController moleculeInputController = null!;


    private MoleculeData _molecule = null!;


    private void Awake()
    {
        // Core生成
        IBondValidator bondValidator =
            new BondValidator();

        SnapService snapService =
            new SnapService(bondValidator);

        _molecule =
            new MoleculeData(bondValidator);


        // Unity層へ注入
        moleculePresenter.Initialize(
            _molecule);

        moleculeInputController.Initialize(
            _molecule,
            snapService);


        // Controller → Presenter
        moleculeInputController.SnapTargetChanged +=
            moleculePresenter.SetHighlight;
    }


    private void OnDestroy()
    {
        moleculeInputController.SnapTargetChanged -=
            moleculePresenter.SetHighlight;
    }

}