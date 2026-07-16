using Bunshimokei.Core.Models;
using System;

namespace Bunshimokei.Core.Events;

public sealed class AtomAddedEventArgs : EventArgs
{
    public AtomData Atom { get; }


    public AtomAddedEventArgs(
        AtomData atom)
    {
        Atom = atom;
    }
}