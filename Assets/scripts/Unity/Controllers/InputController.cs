using UnityEngine;

using Bunshimokei.Unity.Inputs;
using Bunshimokei.Unity.Interfaces;
using Bunshimokei.Unity.Views;

namespace Bunshimokei.Unity.Controllers;

public sealed class InputController : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour inputSourceBehaviour = null!;

    [SerializeField]
    private MoleculeInputController moleculeInputController = null!;


    private IInputSource _inputSource = null!;


    private void Awake()
    {
        _inputSource =
            inputSourceBehaviour as IInputSource
            ?? throw new MissingReferenceException(
                $"{nameof(inputSourceBehaviour)} must implement {nameof(IInputSource)}.");
    }


    private void OnEnable()
    {
        _inputSource.PointerDown += OnPointerDown;
        _inputSource.PointerMove += OnPointerMove;
        _inputSource.PointerUp += OnPointerUp;
    }


    private void OnDisable()
    {
        _inputSource.PointerDown -= OnPointerDown;
        _inputSource.PointerMove -= OnPointerMove;
        _inputSource.PointerUp -= OnPointerUp;
    }


    private void OnPointerDown(
        PointerInputData data)
    {
        if (data.Target == null)
            return;

        if (!data.Target.TryGetComponent(
                out AtomView? atomView))
        {
            return;
        }

        moleculeInputController.BeginDrag(
            atomView!.Data.Id);
    }


    private void OnPointerMove(
        PointerInputData data)
    {

        moleculeInputController.UpdateDrag(
            data.WorldPosition);
    }


    private void OnPointerUp(
        PointerInputData data)
    {

        moleculeInputController.EndDrag();

    }
}