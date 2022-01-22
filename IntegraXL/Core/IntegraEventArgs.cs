using IntegraXL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    /// <summary>
    /// Defines a base class for INTEGRA-7 related event arguments.
    /// </summary>
    public class IntegraEventArgs : EventArgs
    {
        public IntegraEventArgs() { }
    }

    /// <summary>
    /// Defines event arguments containing an <see cref="IntegraSystemExclusive"/> message.
    /// </summary>
    public class IntegraSystemExclusiveEventArgs : IntegraEventArgs
    {
        public IntegraSystemExclusiveEventArgs(IntegraSystemExclusive systemExclusive) : base()
        {
            SystemExclusive = systemExclusive;
        }

        public IntegraSystemExclusive SystemExclusive { get; }
    }

    public class IntegraPartChangedEventArgs : IntegraEventArgs
    {
        public IntegraPartChangedEventArgs(Parts part, Parts previous)
        {
            Part     = part;
            Previous = previous;
        }

        public Parts Part { get; }
        public Parts Previous { get; }
    }

    public class IntegraToneChangedEventArgs : IntegraEventArgs
    {
        public IntegraToneChangedEventArgs(IBankSelect tone, Parts part)
        {
            Tone = tone;
            Part = part;
        }

        public IBankSelect Tone { get; }
        public Parts Part { get; }
    }

    public class LongMessageEventArgs : EventArgs
    {
        public LongMessageEventArgs(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; }
    }

    public class IntegraVirtualSlotsEventArgs : IntegraEventArgs
    {
        public IntegraVirtualSlotsEventArgs(VirtualSlotsState state)
        {
            State = state;
        }

        public VirtualSlotsState State { get; }
    }
}
