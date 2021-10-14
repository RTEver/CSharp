using System;

using NeuronNetworks.Layers;
using NeuronNetworks.DataSet;
using NeuronNetworks.Topologies;

namespace NeuronNetworks.Networks
{
    public interface INeuronNetwork
    {
        public IDataSet TrainSet { get; set; }

        public IDataSet TestSet { get; set; }

        public Topology Topology { get; }

        public ILayer LayerS { get; }

        public ILayer[] LayersA { get; }

        public ILayer LayerR { get; }

        public void Train();

        public Single[] Compute(Single[] inputVector);
    }
}