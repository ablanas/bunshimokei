using Bunshimokei.Unity.Controllers;
using UnityEngine;

namespace Bunshimokei.Unity.UI;

public sealed class ElementButton : MonoBehaviour
{
    [SerializeField]
    private ElementPaletteController paletteController = null!;

    [SerializeField]
    private string elementSymbol = "H";


    public void OnClick()
    {
        paletteController.SelectElement(
            elementSymbol);
    }
}