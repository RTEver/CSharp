using System;
using System.Linq;
using System.Collections.Generic;

using NeuronNetworks.Layers;
using NeuronNetworks.DataSet;
using NeuronNetworks.Topologies;

namespace NeuronNetworks.Networks
{
    public abstract class NeuronNetwork : Object, INeuronNetwork
    {
        public IDataSet TrainSet { get; set; }

        public IDataSet TestSet { get; set; }

        private Topology topology;

        private ILayer layerS;
        private ILayer layerR;

        private ILayer[] layersA;

        public NeuronNetwork(Topology topology)
            : base()
        {
            if (topology == null)
            {
                throw new ArgumentNullException("topology");
            }

            this.topology = topology;

            TopologyBuilder();
        }

        public Topology Topology => topology;

        public ILayer LayerS => layerS;

        public ILayer LayerR => layerR;

        public ILayer[] LayersA => layersA;

        public abstract Single[] Compute(Single[] inputVector);

        public abstract void Train();

        public abstract Single Error { get; }

        private void TopologyBuilder()
        {
            var layers = topology.BuildTopology();

            layerS = layers.First();

            layerR = layers.Last();

            if (layers.Length > 2)
            {
                var hiddenLayer = new List<ILayer>();

                for (var i = 1; i < layers.Length - 1; ++i)
                {
                    hiddenLayer.Add(layers[i]);
                }

                layersA = hiddenLayer.ToArray();
            }
        }
    }
}