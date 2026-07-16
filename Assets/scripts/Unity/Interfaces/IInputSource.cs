using System;
using UnityEngine.EventSystems;

namespace Bunshimokei.Unity.Interfaces;

public interface IInputSource
{
    event Action<PointerEventData> PointerDown;

    event Action<PointerEventData> PointerMove;

    event Action<PointerEventData> PointerUp;
}