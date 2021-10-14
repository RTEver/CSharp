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
                hiddenLayers[index] = new LayerA(hiddenNeurons[index].Cast<NeuronA>().ToArray());
            }

            var outputLayer = new LayerR(outputNeurons.Cast<NeuronR>().ToArray());

            layers.Add(inputLayer);
            layers.AddRange(hiddenLayers);
            layers.Add(outputLayer);

            return layers.ToArray();
        }
    }
}