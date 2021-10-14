using NeuronNetworks.Neurons;

namespace NeuronNetworks.Layers
{
    public sealed class LayerR : Layer
    {
        public LayerR(params NeuronR[] neurons)
            : base(neurons)
        { }
    }
}