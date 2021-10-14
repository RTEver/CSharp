using System;

using NeuronNetworks.Neurons;

namespace NeuronNetworks.Layers
{
    public abstract class Layer : Object, ILayer
    {
        private readonly INeuron[] neurons;

        private protected Layer(params INeuron[] neurons)
            : base()
        {
            if (neurons == null)
            {
                throw new ArgumentNullException("neurons");
            }

            if (neurons.Length == 0)
            {
                throw new ArgumentOutOfRangeException("neurons", "Neuron count should be more than 0.");
            }

            this.neurons = neurons;
        }

        public INeuron[] Neurons => neurons;

        public void ChangeWeights(params Single[][] deltaWeights)
        {
            if (deltaWeights == null)
            {
                throw new ArgumentNullException("deltaWeights");
            }

            if (deltaWeights.Length != neurons.Length)
            {
                throw new ArgumentOutOfRangeException("deltaWeights", "Neuron count does not match.");
            }

            for (var i = 0; i < deltaWeights.Length; ++i)
            {
                if (deltaWeights[i].Length != neurons[i].InputSynapses.Length)
                {
                    throw new ArgumentOutOfRangeException("deltaWeights", "Weight count does not match.");
                }
            }

            for (var neuron = 0; neuron < deltaWeights.Length; ++neuron)
            {
                var synapses = neurons[neuron].InputSynapses;

                for (var synapse = 0; synapse < synapses.Length; ++synapse)
                {
                    synapses[synapse].Weight += deltaWeights[neuron][synapse];
                }
            }
        }

        public Single[] Compute()
        {
            var outputs = new Single[neurons.Length];

            for (var index = 0; index < neurons.Length; ++index)
            {
                outputs[index] = neurons[index].Compute();
            }

            return outputs;
        }

        public Single[] GetOutputs()
        {
            var outputs = new Single[neurons.Length];

            for (var index = 0; index < neurons.Length; ++index)
            {
                outputs[index] = neurons[index].OutputValue;
            }

            return outputs;
        }
    }
}