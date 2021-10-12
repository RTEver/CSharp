using System;

namespace NeuronNetworks.Synapses
{
    public sealed class BiasSynapse : Synapse
    {
        public BiasSynapse()
            : base(null)
        { }

        public sealed override Single Compute() => Weight;
    }
}