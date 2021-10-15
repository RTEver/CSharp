using System;
using System.Linq;

using NeuronNetworks.Neurons;
using NeuronNetworks.Synapses;
using NeuronNetworks.Layers;
using NeuronNetworks.Topologies;
using NeuronNetworks.Networks;
using NeuronNetworks.DataSet;

namespace TestConsoleApplication
{
    public static class Program : Object
    {
        private static void Main(String[] args)
        {
            Test_4_TestRumelhartPerseptronComplete();
        }

        private static void Test_1()
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

        private static void Test_2()
        {
            NeuronS neuronS_1 = new NeuronS();
            NeuronS neuronS_2 = new NeuronS();

            LayerS layerS = new LayerS(neuronS_1, neuronS_2);

            NeuronA neuronA_1 = new NeuronA(ActivationFunction, true, neuronS_1, neuronS_2);
            NeuronA neuronA_2 = new NeuronA(ActivationFunction, true, neuronS_1, neuronS_2);

            LayerA layerA = new LayerA(neuronA_1, neuronA_2);

            NeuronR neuronR_1 = new NeuronR(ActivationFunction, true, neuronA_1, neuronA_2);
            NeuronR neuronR_2 = new NeuronR(ActivationFunction, true, neuronA_1, neuronA_2);

            LayerR layerR = new LayerR(neuronR_1, neuronR_2);

            layerS.ChangeInputs(1, -2);

            ILayer interface_layerR = layerR;

            var results = interface_layerR.Compute();

            Console.WriteLine("-->Start<--\n");

            ShowResults(results);

            ShowWeigth(neuronA_1, neuronA_2, neuronR_1, neuronR_2);

            interface_layerR.ChangeWeights(new Single[][] { new Single[] { 4, 5, 6 }, new Single[] { 9, 7, 8 } });

            Console.WriteLine("-->Change layer R weights<--\n");

            ShowWeigth(neuronA_1, neuronA_2, neuronR_1, neuronR_2);

            results = layerR.Compute();

            Console.WriteLine("-->Compute layer R<--\n");

            ShowResults(results);

            layerA.ChangeWeights(new Single[][] { new Single[] { 55, 11, 22 }, new Single[] { 66, 33, 44 } });

            Console.WriteLine("-->Change layer A weights<--\n");

            ShowWeigth(neuronA_1, neuronA_2, neuronR_1, neuronR_2);

            results = layerR.GetOutputs();

            Console.WriteLine("-->Get outputs layer R<--\n");

            ShowResults(results);

            results = layerA.Compute();

            Console.WriteLine("-->Compute layer A<--\n");

            ShowResults(results);

            results = layerR.GetOutputs();

            Console.WriteLine("-->Get outputs layer R<--\n");

            ShowResults(results);

            results = layerR.Compute();

            Console.WriteLine("-->Compute layer R<--\n");

            ShowResults(results);
        }

        private static void Test_3_TestRumelhartPerseptron()
        {
            var topology = new RumelhartPerseptronTopology(ActivationFunction, 5, 3, new Int32[] { 2, 3, 4 });

            var network = new RumelhartPerseptron(topology);

            var inputNeuronCount = network.LayerS.Neurons.Length;
            var outputNeuronCount = network.LayerR.Neurons.Length;

            Console.WriteLine("Input layer -> Neurons: {0}", inputNeuronCount);
            Console.WriteLine("Output layer -> Neurons: {0}", outputNeuronCount);

            var hiddenNeuronCount = network.LayersA;

            var hiddenLayer = 0;

            foreach (Layer hiddenNeuronCountOnLayer in hiddenNeuronCount)
            {
                var count = hiddenNeuronCountOnLayer.Neurons.Length;

                Console.WriteLine("Hidden layer: {0} -> Neurons: {1}", hiddenLayer++, count);
            }
        }

        private static void Test_4_TestRumelhartPerseptronComplete()
        {
            var trainSet = new DataSet()
            {
                InputVectors = new Single[][]
                {
                    new Single[] { 0, 0 },
                    new Single[] { 1, 0 },
                    new Single[] { 0, 1 },
                    new Single[] { 1, 1 },
                },
                OutputVectors = new Single[][]
                {
                    new Single[] { 0 },
                    new Single[] { 0 },
                    new Single[] { 1 },
                    new Single[] { 1 },
                },
            };

            var topology = new RumelhartPerseptronTopology(ActivationFunction, 2, 1, new Int32[] { 2, 2 });

            var perseptron = new RumelhartPerseptron(topology)
            {
                TrainSet = trainSet,
                Rate = 1,
                Impuls = 0.4f,
                Epochs = 10,
                PrematureExitError = 0,
            };

            perseptron.Train();
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

        private static void ShowResults(Single[] results)
        {
            foreach (Single result in results)
            {
                Console.WriteLine("--> " + result);
            }

            Console.WriteLine();
        }
    }
}