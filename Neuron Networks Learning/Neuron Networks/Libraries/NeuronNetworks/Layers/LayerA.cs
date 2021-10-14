using NeuronNetworks.Neurons;

namespace NeuronNetworks.Layers
{
    public sealed class LayerA : Layer
    {
        public LayerA(params NeuronA[] neurons)
            : base(neurons)
        { }
    }
}