using Bunshimokei.Unity.Interfaces;
using UnityEngine;

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
            if (Input.GetMouseButtonDown(0))
            {
                PointerDown?.Invoke(
                    CreatePointerData());
            }

            if (Input.GetMouseButton(0))
            {
                PointerMove?.Invoke(
                    CreatePointerData());
            }

            if (Input.GetMouseButtonUp(0))
            {
                PointerUp?.Invoke(
                    CreatePointerData());
            }
        }


        private PointerInputData CreatePointerData()
        {
            Ray ray =
                targetCamera.ScreenPointToRay(
                    Input.mousePosition);

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