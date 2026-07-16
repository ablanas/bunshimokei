using Bunshimokei.Unity.Controllers;
using UnityEngine;

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