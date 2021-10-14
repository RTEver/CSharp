using System;

using NeuronNetworks.Layers;
using NeuronNetworks.Neurons;

namespace NeuronNetworks.Topologies
{
    public abstract class Topology : Object
    {
        private readonly Int32 inputNeuronCount;

        private readonly Int32[] hiddenNeuronCount;

        private readonly Int32 outputNeuronCount;

        private Func<Single, Single> activationFunction;

        private protected Topology(Func<Single, Single> activationFunction, Int32 inputNeuronCount, Int32 outputNeuronCount, params Int32[] hiddenNeuronCount)
            : base()
        {
            if (activationFunction == null)
            {
                throw new ArgumentNullException("activationFunction");
            }

            if (inputNeuronCount < 1)
            {
                throw new ArgumentOutOfRangeException("inputNeuronCount");
            }

            if (outputNeuronCount < 1)
            {
                throw new ArgumentOutOfRangeException("outputNeuronCount");
            }

            if (hiddenNeuronCount == null)
            {
                throw new ArgumentNullException("hiddenNeuronCount");
            }

            foreach (Int32 neuronCount in hiddenNeuronCount)
            {
                if (neuronCount < 2)
                {
                    throw new ArgumentOutOfRangeException("hiddenNeuronCount", "Neuron count on hidden layer should be more than 1.");
                }
            }

            this.inputNeuronCount = inputNeuronCount;
            this.outputNeuronCount = outputNeuronCount;
            this.hiddenNeuronCount = hiddenNeuronCount;
            this.activationFunction = activationFunction;
        }

        public Int32 InputNeuronCount => inputNeuronCount;

        public Int32[] HiddenNeuronCount => hiddenNeuronCount;

        public Int32 OutputNeuronCount => outputNeuronCount;

        public Func<Single, Single> ActivationFunction => activationFunction;

        public abstract ILayer[] BuildTopology();

        public INeuron[] GenerateInputNeurons()
        {
            var inputNeurons = new NeuronS[inputNeuronCount];

            for (var index = 0; index < inputNeuronCount; ++index)
            {
                inputNeurons[index] = new NeuronS();
            }

            return inputNeurons;
        }
    }
}