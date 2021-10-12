using System;

using NeuronNetworks.Neurons;

namespace NeuronNetworks.Synapses
{
    public interface ISynapse
    {
        public INeuron InputNeuron { get; }

        public Single Weight { get; set; }

        public Single OutputValue { get; }

        public Single Compute();
    }
}