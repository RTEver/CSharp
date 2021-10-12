using System;

using NeuronNetworks.Neurons;
using NeuronNetworks.Synapses;

namespace TestConsoleApplication
{
    public static class Program : Object
    {
        private static void Main(String[] args)
        {
            NeuronS neuronS_1 = new NeuronS();
            NeuronS neuronS_2 = new NeuronS();

            INeuron interface_neuronS_1 = neuronS_1;
            INeuron interface_neuronS_2 = neuronS_2;

            INeuron neuronA_1 = new NeuronA(ActivationFunction, true, interface_neuronS_1, interface_neuronS_2);
            INeuron neuronA_2 = new NeuronA(ActivationFunction, true, interface_neuronS_1, interface_neuronS_2);

            INeuron neuronR_1 = new NeuronR(ActivationFunction, true, neuronA_1, neuronA_2);
            INeuron neuronR_2 = new NeuronR(ActivationFunction, true, neuronA_1, neuronA_2);

            neuronS_1.InputValue = 1;
            neuronS_2.InputValue = -2;

            neuronA_1.InputSynapses[0].Weight = 1;
            neuronA_2.InputSynapses[0].Weight = 1;
            neuronR_1.InputSynapses[0].Weight = 1;
            neuronR_2.InputSynapses[0].Weight = 1;

            neuronA_1.InputSynapses[1].Weight = 2;
            neuronA_2.InputSynapses[1].Weight = 2;
            neuronR_1.InputSynapses[1].Weight = 2;
            neuronR_2.InputSynapses[1].Weight = 2;

            neuronA_1.InputSynapses[2].Weight = 3;
            neuronA_2.InputSynapses[2].Weight = 3;
            neuronR_1.InputSynapses[2].Weight = 3;
            neuronR_2.InputSynapses[2].Weight = 3;

            var resultR_1 = neuronR_1.Compute();
            var resultR_2 = neuronR_2.Compute();

            ShowWeigth(neuronA_1, neuronA_2, neuronR_1, neuronR_2);

            Console.WriteLine("S1: " + interface_neuronS_1.OutputValue);
            Console.WriteLine("S2: " + interface_neuronS_2.OutputValue);

            Console.WriteLine("A1: " + neuronA_1.OutputValue);
            Console.WriteLine("A2: " + neuronA_2.OutputValue);

            Console.WriteLine("R1: " + neuronR_1.OutputValue);
            Console.WriteLine("R2: " + neuronR_2.OutputValue);

            Console.WriteLine("Result 1: " + resultR_1);
            Console.WriteLine("Result 2: " + resultR_2);
        }

        private static Single ActivationFunction(Single input) => 1.0f / (1 + MathF.Exp(-input));

        private static void ShowWeigth(params INeuron[] neurons)
        {
            foreach (INeuron neuron in neurons)
            {
                var neuronType = (neuron is NeuronA) ? "A" : "R";

                var synapses = neuron.InputSynapses;

                foreach (ISynapse synapse in synapses)
                {
                    var synapseType = (synapse is BiasSynapse) ? "Bias" : "Default";

                    Console.WriteLine("Neuron type: {2}\tSynapse type: {0}\tSynapse weight: {1}", synapseType, synapse.Weight, neuronType);
                }

                Console.WriteLine();
            }
        }
    }
}