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
        private Single[][][] deltaWeigths;

        public Single Rate { get; set; }

        public Single Impuls { get; set; }

        public Single Epochs { get; set; }

        public Single PrematureExitError { get; set; }

        public RumelhartPerseptron(RumelhartPerseptronTopology topology)
            : base(topology)
        {
            var layers = new List<ILayer>();

            layers.AddRange(LayersA);

            layers.Add(LayerR);

            deltaWeigths = new Single[layers.Count][][];

            for (var layerIndex = 0; layerIndex < layers.Count; ++layerIndex)
            {
                var neurons = layers[layerIndex].Neurons;

                deltaWeigths[layerIndex] = new Single[neurons.Length][];

                for (var neuronIndex = 0; neuronIndex < neurons.Length; ++neuronIndex)
                {
                    var synapses = neurons[neuronIndex].InputSynapses;

                    deltaWeigths[layerIndex][neuronIndex] = new Single[synapses.Length];
                }
            }
        }

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
                var epochError = default(Single);

                for (var input = 0; input < TrainSet.InputVectors.Length; ++input)
                {
                    var actualOutput = Compute(TrainSet.InputVectors[input]);

                    var epselons = GetEpselons(TrainSet.OutputVectors[input], actualOutput);

                    var deltas = GetDeltas(epselons);

                    var gradients = GetGradients(deltas);

                    ChangeWeigths(gradients);

                    var epselonsSumma = default(Single);

                    foreach (Single epselon in epselons)
                    {
                        epselonsSumma += MathF.Abs(epselon);
                    }

                    epochError += epselonsSumma / epselons.Length;
                }

                epochError /= TrainSet.OutputVectors.Length;

                Console.WriteLine("Error, %: {0}", epochError);

                if (epochError <= PrematureExitError)
                {
                    break;
                }
            }
        }

        private void ChangeWeigths(Single[][][] gradients)
        {
            var layers = new List<ILayer>();

            layers.AddRange(LayersA);

            layers.Add(LayerR);

            for (var layerIndex = 0; layerIndex < layers.Count; ++layerIndex)
            {
                var currentLayer = layers[layerIndex];

                var neurons = currentLayer.Neurons;

                for (var neuronIndex = 0; neuronIndex < neurons.Length; ++neuronIndex)
                {
                    var currentNeuron = neurons[neuronIndex];

                    var synapses = currentNeuron.InputSynapses;

                    for (var synapseIndex = 0; synapseIndex < synapses.Length; ++synapseIndex)
                    {
                        var deltaWeight = Rate * gradients[layerIndex][neuronIndex][synapseIndex]
                            + Impuls * deltaWeigths[layerIndex][neuronIndex][synapseIndex];

                        deltaWeigths[layerIndex][neuronIndex][synapseIndex] = deltaWeight;
                    }
                }
            }

            for (var layerIndex = 0; layerIndex < layers.Count - 1; ++layerIndex)
            {
                LayersA[layerIndex].ChangeWeights(deltaWeigths[layerIndex]);
            }

            LayerR.ChangeWeights(deltaWeigths[layers.Count - 1]);
        }

        private Single[][][] GetGradients(Single[][] deltas)
        {
            var gradients = new Single[deltas.Length][][];

            var leftToRightDeltas = new Single[deltas.Length][];

            for (var i = 0; i < deltas.Length; ++i)
            {
                leftToRightDeltas[i] = deltas[i];
            }

            var layers = new List<ILayer>();

            layers.AddRange(LayersA);

            layers.Add(LayerR);

            for (var layerIndex = 0; layerIndex < layers.Count; ++layerIndex)
            {
                var neurons = layers[layerIndex].Neurons;

                gradients[layerIndex] = new Single[neurons.Length][];

                for (var neuronIndex = 0; neuronIndex < neurons.Length; ++neuronIndex)
                {
                    var synapses = neurons[neuronIndex].InputSynapses;

                    gradients[layerIndex][neuronIndex] = new Single[synapses.Length];
                }
            }

            for (var layerIndex = 0; layerIndex < gradients.Length; ++layerIndex)
            {
                var currentLayer = layers[layerIndex];

                var neurons = currentLayer.Neurons;

                for (var neuronIndex = 0; neuronIndex < neurons.Length; ++neuronIndex)
                {
                    var currentNeuron = neurons[neuronIndex];

                    var synapses = currentNeuron.InputSynapses;

                    for (var synapseIndex = 0; synapseIndex < synapses.Length; ++synapseIndex)
                    {
                        var gradient = deltas[layerIndex][neuronIndex];

                        if (!(synapses[synapseIndex] is BiasSynapse))
                        {
                            gradient *= synapses[synapseIndex].InputNeuron.OutputValue;
                        }
                        
                        gradients[layerIndex][neuronIndex][synapseIndex] = gradient;
                    }
                }
            }

            return gradients;
        }

        private Single[][] GetDeltas(Single[] epselons)
        {
            var layers = new List<ILayer>();

            layers.Add(LayerR);
            
            for (var index = LayersA.Length - 1; index >= 0; --index)
            {
                layers.Add(LayersA[index]);
            }

            var deltas = new Single[layers.Count][];

            var outputDelta = new Single[LayerR.Neurons.Length];

            for (var i = 0; i < LayerR.Neurons.Length; ++i)
            {
                var output = LayerR.Neurons[i].OutputValue;

                outputDelta[i] = epselons[i] * ((1 - output) * output);
            }

            deltas[0] = outputDelta;

            for (var layerIndex = 1; layerIndex < layers.Count; ++layerIndex)
            {
                var previousLayer = layers[layerIndex - 1];

                var previousNeurons = previousLayer.Neurons;

                var currentLayer = layers[layerIndex];

                var neurons = currentLayer.Neurons;

                var currentLayerDeltas = new Single[neurons.Length];

                for (var neuronIndex = 0; neuronIndex < neurons.Length; ++neuronIndex)
                {
                    var summa = default(Single);

                    for (var previousNeuronIndex = 0; previousNeuronIndex < previousNeurons.Length; ++previousNeuronIndex)
                    {
                        var previousNeuron = previousNeurons[previousNeuronIndex];

                        var previousSynapses = previousNeuron.InputSynapses;

                        summa += deltas[layerIndex - 1][previousNeuronIndex] * previousSynapses[neuronIndex + 1].Weight;
                    }

                    var neuron = neurons[neuronIndex];

                    var output = neuron.OutputValue;

                    var delta = (1 - output) * output * summa;

                    currentLayerDeltas[neuronIndex] = delta;
                }

                deltas[layerIndex] = currentLayerDeltas;
            }

            for (var i = 0; i < deltas.Length; ++i)
            {
                for (var j = 0; j < deltas[i].Length; ++j)
                {
                    Console.Write(deltas[i][j] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            return deltas.ToArray();
        }

        private Single[] GetEpselons(Single[] ideal, Single[] actual)
        {
            if (ideal == null)
            {
                throw new ArgumentNullException("ideal");
            }

            if (actual == null)
            {
                throw new ArgumentNullException("actual");
            }

            var epselons = new Single[ideal.Length];

            for (var index = 0; index < epselons.Length; ++index)
            {
                epselons[index] = ideal[index] - actual[index];
            }

            return epselons;
        }
    }
}