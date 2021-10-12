using System;

using NeuronNetworks.Neurons;

namespace NeuronNetworks.Synapses
{
    public class Synapse : Object, ISynapse
    {
        private readonly INeuron inputNeuron;

        private Single weight;

        private Single outputValue;

        public Synapse(INeuron inputNeuron)
            : base()
        {
            this.inputNeuron = inputNeuron;

            weight = 0;

            outputValue = 0;
        }

        public INeuron InputNeuron => inputNeuron;

        public Single Weight { get => weight; set => weight = value; }

        public Single OutputValue => outputValue;

        public virtual Single Compute()
        {
            if (inputNeuron == null)
            {
                throw new ArgumentNullException("inputNeuron");
            }

            return outputValue = inputNeuron.Compute() * weight;
        }
    }
}