using Bunshimokei.Unity.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bunshimokei.Unity.Inputs
{

    public sealed class MouseInputSource :
        MonoBehaviour,
        IInputSource
    {
        [SerializeField]
        private Camera targetCamera = null!;


        public event System.Action<PointerInputData>? PointerDown;

        public event System.Action<PointerInputData>? PointerMove;

        public event System.Action<PointerInputData>? PointerUp;

        private const float DefaultDistance = 5f;


        private void Awake()
        {
            targetCamera ??= Camera.main;

            if (targetCamera == null)
            {
                throw new MissingReferenceException(
                    "Target Camera is not assigned.");
            }
        }


        private void Update()
        {
            Mouse mouse = Mouse.current;

            if (mouse == null)
                return;


            if (mouse.leftButton.wasPressedThisFrame)
            {
                PointerDown?.Invoke(
                    CreatePointerData());
            }


            if (mouse.leftButton.isPressed)
            {
                PointerMove?.Invoke(
                    CreatePointerData());
            }


            if (mouse.leftButton.wasReleasedThisFrame)
            {
                PointerUp?.Invoke(
                    CreatePointerData());
            }
        }

        private PointerInputData CreatePointerData()
        {
            Vector2 mousePosition =
                Mouse.current.position.ReadValue();


            Ray ray =
                targetCamera.ScreenPointToRay(
                    mousePosition);


            if (Physics.Raycast(
                    ray,
                    out RaycastHit hit))
            {
                return new PointerInputData(
                    hit.point,
                    hit.collider.gameObject);
            }


            return new PointerInputData(
                ray.GetPoint(DefaultDistance),
                null);
        }
    }
}