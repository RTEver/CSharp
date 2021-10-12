using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeuronNetworks.Synapses;

namespace NeuronNetworks.Neurons
{
    public sealed class NeuronR : Neuron
    {
        public NeuronR([Required] Func<Single, Single> activationFunction,
            Boolean createBias = true, params INeuron[] inputNeurons)
            : base(activationFunction, createBias, inputNeurons)
        { }

        public sealed override Single Compute()
        {
            var result = default(Single);

            foreach (ISynapse synapse in InputSynapses)
            {
                result += synapse.Compute();
            }

            return OutputValue = ActivationFunction.Invoke(result);
        }
    }
}