using Bunshimokei.Core.Models;
using System;

namespace Bunshimokei.Core.Events;

public sealed class BondAddedEventArgs : EventArgs
{
    public BondData Bond { get; }


    public BondAddedEventArgs(
        BondData bond)
    {
        Bond = bond;
    }
}