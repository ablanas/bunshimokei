using Bunshimokei.Core.ValueObjects;
using System;

namespace Bunshimokei.Core.Events;


public sealed class BondRemovedEventArgs : EventArgs
{
    public BondId BondId { get; }


    public BondRemovedEventArgs(
        BondId bondId)
    {
        BondId = bondId;
    }
}