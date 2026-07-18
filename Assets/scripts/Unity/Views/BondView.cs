using UnityEngine;

using Bunshimokei.Core.Enums;

namespace Bunshimokei.Unity.Views
{

    public sealed class BondView : MonoBehaviour
    {
        private Transform _atomA = null!;
        private Transform _atomB = null!;

        private float _bondRadius;


        [SerializeField]
        private Transform stick = null!;


        private BondOrder _order;


        public void Initialize(
            Transform atomA,
            Transform atomB,
            BondOrder order,
            float bondRadius)
        {
            _atomA = atomA;
            _atomB = atomB;
            _order = order;
            _bondRadius = bondRadius;

            UpdateTransform();
        }


        private void Update()
        {
            UpdateTransform();
        }


        private void UpdateTransform()
        {
            if (_atomA == null || _atomB == null)
                return;


            Vector3 direction =
                _atomB.position - _atomA.position;


            float distance =
                direction.magnitude;


            if (distance == 0f)
                return;


            transform.position =
                (_atomA.position + _atomB.position) * 0.5f;


            transform.rotation =
                Quaternion.FromToRotation(
                    Vector3.up,
                    direction);


            stick.localScale =
                new Vector3(
                    _bondRadius,
                    distance / 2f,
                    _bondRadius);
        }
    }
}