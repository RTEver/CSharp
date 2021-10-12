using System;
using System.ComponentModel.DataAnnotations;

using NeuronNetworks.Synapses;

namespace NeuronNetworks.Neurons
{
    public sealed class NeuronA : Neuron
    {
        public NeuronA([Required] Func<Single, Single> activationFunction,
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