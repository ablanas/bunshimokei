using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Bunshimokei.Core.Definitions;
using Bunshimokei.Core.Services;
using Bunshimokei.Unity.Controllers;

namespace Bunshimokei.Unity.Presenters
{

    public sealed class ElementPalettePresenter : MonoBehaviour
    {
        [SerializeField]
        private Button buttonPrefab = null!;

        [SerializeField]
        private Transform content = null!;


        public void Initialize(
            ElementDatabase database,
            ElementPaletteController controller)
        {
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }

            foreach (ElementDefinition element in database.Elements)
            {
                CreateButton(
                    element,
                    controller);
            }
        }


        private void CreateButton(
            ElementDefinition element,
            ElementPaletteController controller)
        {
            Button button =
                Instantiate(
                    buttonPrefab,
                    content);


            TMP_Text text =
                button.GetComponentInChildren<TMP_Text>();

            if (text != null)
            {
                text.text =
                    element.Symbol.Value;
            }


            string symbol =
                element.Symbol.Value;


            button.onClick.AddListener(
                () => controller.SelectElement(symbol));
        }
    }
}