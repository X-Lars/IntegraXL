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
    /// Defines event arguments for <see cref="IntegraSystemExclusive"/> messages.
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
            Part = part;
            Previous = previous;
        }

        public Parts Part { get; }
        public Parts Previous { get; }
    }

    /// <summary>
    /// Event arguments for tone changed events, providing the new tone's <see cref="IBankSelect"/> interface and the associated part.
    /// </summary>
    public class IntegraToneChangedEventArgs : IntegraEventArgs
    {
        /// <summary>
        /// Creates and initializes a new <see cref="IntegraToneChangedEventArgs"/> instance.
        /// </summary>
        /// <param name="tone">The <see cref="IBankSelect"/> interface of the new tone.</param>
        /// <param name="part">The tone's associated part.</param>
        public IntegraToneChangedEventArgs(IBankSelect tone, Parts part)
        {
            Tone = tone;
            Part = part;
        }

        /// <summary>
        /// Gets the <see cref="IBankSelect"/> interface of the new tone.
        /// </summary>
        public IBankSelect Tone { get; }

        /// <summary>
        /// Gets the new tone's associated part.
        /// </summary>
        public Parts Part { get; }
    }

    /// <summary>
    /// Event arguments for <see cref="IParameterProvider{TIndexer}.ParametersChanged"/> event, providing the new <see cref="IntegraParameterProvider{TIndexer}"/> type.
    /// </summary>
    public class IntegraParametersChangedEventArgs : IntegraEventArgs
    {
        /// <summary>
        /// Creates and initializes a new <see cref="IntegraParametersChangedEventArgs"/> instance.
        /// </summary>
        /// <param name="type">The type of <see cref="IntegraParameterProvider{TIndexer}"/>.</param>
        public IntegraParametersChangedEventArgs(Type type)
        {
            Type = type;
        }

        /// <summary>
        /// Gets the new type of <see cref="IntegraParameterProvider{TIndexer}"/>.
        /// </summary>
        public Type Type { get; }
    }

    /// <summary>
    /// Defines event arguments for MIDI long messages.
    /// </summary>
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
        public IntegraVirtualSlotsEventArgs(VirtualSlotsState state, IntegraExpansions[] expansions)
        {
            State = state;

            ExpansionA = expansions[0];
            ExpansionB = expansions[1];
            ExpansionC = expansions[2];
            ExpansionD = expansions[3];
        }

        public VirtualSlotsState State { get; }

        public IntegraExpansions ExpansionA { get; }
        public IntegraExpansions ExpansionB { get; }
        public IntegraExpansions ExpansionC { get; }
        public IntegraExpansions ExpansionD { get; }
    }

    public class IntegraConnectionStatusEventArgs : IntegraEventArgs
    {
        public IntegraConnectionStatusEventArgs(ConnectionStatus status, ConnectionStatus previous)
        {
            Status = status;
            Previous = previous;
        }

        public ConnectionStatus Status { get; }
        public ConnectionStatus Previous { get; }
    }
}
