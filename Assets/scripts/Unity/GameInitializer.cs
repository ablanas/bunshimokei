//using UnityEngine;
//using Bunshimokei.Core.Services;
//using Bunshimokei.Infrastructure.Modding;
//using System.IO;
//using System.Collections.Generic;

//public sealed class GameInitializer : MonoBehaviour
//{
//    public ElementDatabase Database { get; private set; } = null!;

//    private readonly ModManager _modManager = new();

//    private void Awake()
//    {
//        Database = new ElementDatabase();

//        var mods = new List<string>
//        {
//            Path.Combine(
//                Application.streamingAssetsPath,
//                "Mods",
//                "Vanilla")
//        };

//        _modManager.LoadMods(mods, Database);

//        Debug.Log(
//            $"Loaded elements: {Database.Count}");
//    }
//}