using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeuronNetworks.Synapses;

namespace NeuronNetworks.Neurons
{
    public interface INeuron
    {
        public Single OutputValue { get; }

        public ISynapse[] InputSynapses { get; }

        public Single Compute();
    }
}