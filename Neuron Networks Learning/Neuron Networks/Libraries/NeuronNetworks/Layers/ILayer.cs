using System;

using NeuronNetworks.Neurons;

namespace NeuronNetworks.Layers
{
    public interface ILayer
    {
        public INeuron[] Neurons { get; }

        public Single[] GetOutputs();

        public Single[] Compute();

        public void ChangeWeights(params Single[][] deltaWeights);
    }
}