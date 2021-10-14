using System;

using NeuronNetworks.Neurons;

namespace NeuronNetworks.Layers
{
    public sealed class LayerS : Layer
    {
        public LayerS(params NeuronS[] neurons)
            : base(neurons)
        { }

        public void ChangeInputs(params Single[] inputs)
        {
            if (inputs == null)
            {
                throw new ArgumentNullException("inputs");
            }

            if (inputs.Length != Neurons.Length)
            {
                throw new ArgumentOutOfRangeException("inputs", "Input count does not match.");
            }

            var index = 0;

            foreach (NeuronS neuron in Neurons)
            {
                neuron.InputValue = inputs[index++];
            }
        }
    }
}