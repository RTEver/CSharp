using System;
using System.Linq;
using System.Collections.Generic;

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

                    epselons = GetEpselons(TrainSet.OutputVectors[input], actualOutput);

                    var deltas = GetDeltas(epselons);

                    var gradients = GetGradients(deltas);
                }

                if (epselons.Max() <= PrematureExitError)
                {
                    break;
                }
            }
        }

        private Single[][][] GetDeltaWeigths(Single[][] gradients)
        {

        }

        private Single[][] GetGradients(Single[][] deltas)
        {
            var gradients = new Single[deltas.Length][];

            var layers = new List<ILayer>();

            layers.Add(LayerS);

            layers.AddRange(LayersA);

            for (var layerIndex = 0; layerIndex < layers.Count; ++layerIndex)
            {
                var neurons = layers[layerIndex].Neurons;

                var layerGradients = new Single[neurons.Length];

                for (var neuronIndex = 0; neuronIndex < neurons.Length; ++neuronIndex)
                {
                    var gradient = neurons[neuronIndex].OutputValue * deltas[layerIndex][neuronIndex];

                    layerGradients[neuronIndex] = gradient;
                }

                gradients[layerIndex] = layerGradients;
            }

            return gradients;
        }

        private Single[][] GetDeltas(Single[] epselons)
        {
            var layers = new List<ILayer>();

            layers.Add(LayerR);
            
            for (var index = this.LayersA.Length - 1; index >= 0; --index)
            {
                layers.Add(this.LayersA[index]);
            }

            var deltas = new List<Single[]>();

            var outputDelta = new Single[layers[0].Neurons.Length];

            for (var i = 0; i < layers[0].Neurons.Length; ++i)
            {
                var output = layers[0].Neurons[i].OutputValue;

                outputDelta[i] = epselons[i] * ((1 - output) * output);
            }

            deltas.Add(outputDelta);

            for (var layer = 1; layer < layers.Count; ++layer)
            {
                var hiddenLayerDeltas = new Single[layers[layer].Neurons.Length];

                for (var neuron = 0; neuron < layers[layer].Neurons.Length; ++neuron)
                {
                    var summaDeltasAndWeights = default(Single);

                    for (var i = 0; i < deltas[layer - 1].Length; ++i)
                    {
                        summaDeltasAndWeights += deltas[0][i] * layers[0].Neurons[i].InputSynapses[neuron + 1].Weight;
                    }

                    var output = layers[layer].Neurons[neuron].OutputValue;

                    hiddenLayerDeltas[neuron] = (1 - output) * output * summaDeltasAndWeights;
                }

                deltas.Add(hiddenLayerDeltas);
            }

            return deltas.ToArray();
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