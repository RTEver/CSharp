using System;
using System.Linq;
using System.Collections.Generic;

using NeuronNetworks.Layers;
using NeuronNetworks.Neurons;

namespace NeuronNetworks.Topologies
{
    public sealed class RumelhartPerseptronTopology : Topology
    {
        private Func<Single, Single> activationFunction;

        public RumelhartPerseptronTopology(Func<Single, Single> activationFunction, 
            Int32 inputNeuronCount, Int32 outputNeuronCount, Int32[] hiddenNeuronCount)
            : base(activationFunction, inputNeuronCount, outputNeuronCount, hiddenNeuronCount)
        {
            if (hiddenNeuronCount.Length == 0)
            {
                throw new ArgumentOutOfRangeException("hiddenNeuronCount", "Hidden layer count should be more than 0.");
            }

            this.activationFunction = activationFunction;
        }

        public override ILayer[] BuildTopology()
        {
            var layers = new List<ILayer>();

            var inputNeurons = GenerateInputNeurons();

            var hiddenNeurons = new INeuron[HiddenNeuronCount.Length][];

            for (var hiddenLayerIndex = 0; hiddenLayerIndex < HiddenNeuronCount.Length; ++hiddenLayerIndex)
            {
                var hiddenNeuronsOnLayer = new INeuron[HiddenNeuronCount[hiddenLayerIndex]];

                for (var neuronIndex = 0; neuronIndex < HiddenNeuronCount[hiddenLayerIndex]; ++neuronIndex)
                {
                    if (hiddenLayerIndex == 0)
                    {
                        hiddenNeuronsOnLayer[neuronIndex] = new NeuronA(activationFunction, true, inputNeurons);
                    }
                    else
                    {
                        hiddenNeuronsOnLayer[neuronIndex] = new NeuronA(activationFunction, true, hiddenNeurons[hiddenLayerIndex - 1]);
                    }
                }

                hiddenNeurons[hiddenLayerIndex] = hiddenNeuronsOnLayer;
            }

            var outputNeurons = new INeuron[OutputNeuronCount];

            for (var index = 0; index < OutputNeuronCount; ++index)
            {
                outputNeurons[index] = new NeuronR(activationFunction, true, hiddenNeurons.Last());
            }

            var inputLayer = new LayerS(inputNeurons.Cast<NeuronS>().ToArray());

            var hiddenLayers = new ILayer[hiddenNeurons.Length];

            for (var index = 0; index < hiddenNeurons.Length; ++index)
            {
                var layer = new LayerA(hiddenNeurons[index].Cast<NeuronA>().ToArray());

                //layer.ChangeWeights(GetRandomWeights(layer.Neurons.Length, layer.Neurons[0].InputSynapses.Length));

                hiddenLayers[index] = layer;
            }

            var outputLayer = new LayerR(outputNeurons.Cast<NeuronR>().ToArray());

            //outputLayer.ChangeWeights(GetRandomWeights(outputLayer.Neurons.Length, outputLayer.Neurons[0].InputSynapses.Length));

            layers.Add(inputLayer);
            layers.AddRange(hiddenLayers);
            layers.Add(outputLayer);

            return layers.ToArray();
        }

        private static Single[][] GetRandomWeights(Int32 neurons, Int32 synapses)
        {
            var weights = new Single[neurons][];

            var random = new Random();

            for (var i = 0; i < neurons; ++i)
            {
                for (var j = 0; j < synapses; ++j)
                {
                    weights[i] = new Single[synapses];

                    for (var k = 0; k < synapses; ++k)
                    {
                        weights[i][k] = (Single)random.NextDouble() * 100 + 1;
                    }
                }
            }

            return weights;
        }
    }
}