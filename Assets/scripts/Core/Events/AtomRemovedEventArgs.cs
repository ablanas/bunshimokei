using Bunshimokei.Core.ValueObjects;
using System;

namespace Bunshimokei.Core.Events;


public sealed class AtomRemovedEventArgs : EventArgs
{
    public AtomId AtomId { get; }


    public AtomRemovedEventArgs(
        AtomId atomId)
    {
        AtomId = atomId;
    }
}