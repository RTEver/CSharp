using System;
using System.Collections.Generic;

using NeuronNetworks.Synapses;

namespace NeuronNetworks.Neurons
{
    public abstract class Neuron : Object, INeuron
    {
        private readonly ISynapse[] inputSynapses;

        private Single outputValue;

        private readonly Func<Single, Single> activationFunction;

        private protected Neuron(Func<Single, Single> activationFunction)
            : base()
        {
            inputSynapses = null;

            outputValue = 0;

            this.activationFunction = activationFunction;
        }

        private protected Neuron(Func<Single, Single> activationFunction,
            Boolean createBias, params INeuron[] inputNeurons)
            : this(activationFunction)
        {
            if (inputNeurons == null)
            {
                throw new ArgumentNullException("inputNeurons");
            }

            if (inputNeurons.Length == 0)
            {
                throw new ArgumentOutOfRangeException("inputNeurons", "Neuron count should be more than 0.");
            }

            inputSynapses = GenerateSynapses(createBias, inputNeurons);
        }

        public ISynapse[] InputSynapses => inputSynapses;

        public Single OutputValue { get => outputValue; private protected set => outputValue = value; }

        private protected Func<Single, Single> ActivationFunction => activationFunction;

        public abstract Single Compute();

        private protected static ISynapse[] GenerateSynapses(Boolean createBias, params INeuron[] neurons)
        {
            if (neurons == null)
            {
                throw new ArgumentNullException("neurons");
            }

            if (neurons.Length == 0)
            {
                throw new ArgumentOutOfRangeException("neurons", "Neuron count shound be more than 0.");
            }

            var synapses = new List<ISynapse>();

            if (createBias)
            {
                synapses.Add(new BiasSynapse());
            }

            for (var index = 0; index < neurons.Length; ++index)
            {
                var neuron = neurons[index];

                var synapse = new Synapse(neuron);

                synapses.Add(synapse);
            }

            return synapses.ToArray();
        }
    }
}