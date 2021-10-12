using System;

namespace NeuronNetworks.Neurons
{
    public sealed class NeuronS : Neuron
    {
        private Single inputValue;

        public NeuronS(Single inputValue = 0, Func<Single, Single> activationFunction = null)
            : base(activationFunction)
        {
            this.inputValue = inputValue;
        }

        public Single InputValue { get => inputValue; set => inputValue = value; }

        public sealed override Single Compute()
        {
            var result = inputValue;

            if (ActivationFunction != null)
            {
                result = ActivationFunction.Invoke(result);
            }

            return OutputValue = result;
        }
    }
}