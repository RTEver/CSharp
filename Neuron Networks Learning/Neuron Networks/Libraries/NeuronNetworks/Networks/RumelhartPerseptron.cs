using System;

using NeuronNetworks.DataSet;
using NeuronNetworks.Synapses;
using NeuronNetworks.Layers;
using NeuronNetworks.Neurons;
using NeuronNetworks.Topologies;
using NeuronNetworks.LearningAlgorithms;

namespace NeuronNetworks.Networks
{
    public sealed class RumelhartPerseptron : NeuronNetwork
    {
        public Single Rate { get; set; }

        public Single Impult { get; set; }

        public Single Epochs { get; set; }

        public Single PrematureExitError { get; set; }

        public RumelhartPerseptron(RumelhartPerseptronTopology topology)
            : base(topology)
        { }

        public override Single[] Compute(Single[] inputVector)
        {
            ((LayerS)LayerS).ChangeInputs(inputVector);

            var result = LayerR.Compute();

            return result;
        }

        public override void Train()
        {
            for (var epoch = 0; epoch < Epochs; ++epoch)
            {
                var epselons = new Single[LayerR.Neurons.Length];

                for (var input = 0; input < TrainSet.InputVectors.Length; ++input)
                {
                    var actualOutput = Compute(TrainSet.InputVectors[input]);
                }
            }
        }

        private Single[] GetDeltas()
        {

        }

        private Single[] GetEpselons(Single[] ideal, Single[] actual)
        {
            var epselons = new Single[ideal.Length];

            for (var index = 0; index < ideal.Length; ++index)
            {
                epselons[index] = ideal[index] - actual[index];
            }

            return epselons;
        }
    }
}