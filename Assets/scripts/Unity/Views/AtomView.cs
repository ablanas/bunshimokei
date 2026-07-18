using UnityEngine;
using Bunshimokei.Core.Models;
using Bunshimokei.Core.ValueObjects;
using Bunshimokei.Unity.Settings;

namespace Bunshimokei.Unity.Views
{

    [RequireComponent(typeof(SphereCollider))]
    public sealed class AtomView : MonoBehaviour
    {
        public AtomData Data { get; private set; } = null!;


        private Renderer _renderer = null!;
        private MaterialPropertyBlock _propertyBlock = null!;

        private Color _baseColor;


        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();

            if (_renderer == null)
            {
                throw new MissingComponentException(
                    $"{nameof(AtomView)} requires a child Renderer.");
            }

            _propertyBlock = new MaterialPropertyBlock();
        }


        public void Initialize(
            AtomData atomData,
            MoleculeDisplaySettings settings)
        {
            Data = atomData;



            RefreshAppearance(settings);
        }

        public void RefreshAppearance(
            MoleculeDisplaySettings settings)
        {
            _baseColor = ToUnityColor(Data.Element.Color);

            SetColor(_baseColor);

            float radius =
                settings.ConvertPmToUnity(
                    Data.Element.VanDerWaalsRadiusPm)
                * settings.AtomRadiusScale;

            transform.localScale =
                2f * radius * Vector3.one;
        }


        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }


        public void SetHighlight(bool enabled)
        {
            SetColor(
                enabled
                    ? Color.yellow
                    : _baseColor);
        }


        private void SetColor(Color color)
        {
            _renderer.GetPropertyBlock(
                _propertyBlock);

            _propertyBlock.SetColor(
                "_Color",
                color);

            _renderer.SetPropertyBlock(
                _propertyBlock);
        }


        private static Color ToUnityColor(
            ColorRGBA color)
        {
            return new Color(
                color.R,
                color.G,
                color.B,
                color.A);
        }
    }
}