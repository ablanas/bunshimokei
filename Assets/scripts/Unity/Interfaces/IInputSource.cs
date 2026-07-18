using System;
using Bunshimokei.Unity.Inputs;

namespace Bunshimokei.Unity.Interfaces
{

    public interface IInputSource
    {
        event Action<PointerInputData> PointerDown;

        event Action<PointerInputData> PointerMove;

        event Action<PointerInputData> PointerUp;
    }
}