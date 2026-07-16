using Bunshimokei.Core.ValueObjects;
using System;

namespace Bunshimokei.Core.Events;

public sealed class AtomMovedEventArgs : EventArgs
{
    public AtomId AtomId { get; }

    public VectorPm3D Position { get; }


    public AtomMovedEventArgs(
        AtomId atomId,
        VectorPm3D position)
    {
        AtomId = atomId;
        Position = position;
    }
}