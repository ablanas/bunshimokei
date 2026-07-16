using System.Collections.Generic;
using UnityEngine;

using Bunshimokei.Core.Models;
using Bunshimokei.Core.ValueObjects;

using Bunshimokei.Unity.Views;


namespace Bunshimokei.Unity.Controllers;


public sealed class BondController : MonoBehaviour
{
    [SerializeField]
    private BondView bondPrefab = null!;


    [SerializeField]
    private Transform bondParent = null!;


    private readonly Dictionary<BondId, BondView> _views = new();


    public void CreateBondView(
        BondData bond,
        Transform atomA,
        Transform atomB)
    {
        if (_views.ContainsKey(bond.Id))
            return;


        BondView view =
            Instantiate(
                bondPrefab,
                bondParent);


        view.Initialize(
            atomA,
            atomB,
            bond.BondOrder);


        _views.Add(
            bond.Id,
            view);
    }


    public void RemoveBondView(
        BondId bondId)
    {
        if (!_views.TryGetValue(
                bondId,
                out BondView? view))
        {
            return;
        }


        Destroy(view.gameObject);

        _views.Remove(bondId);
    }
}