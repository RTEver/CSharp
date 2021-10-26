using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

using NeuronNetworks.Neurons;
using NeuronNetworks.Synapses;
using NeuronNetworks.Layers;
using NeuronNetworks.Topologies;
using NeuronNetworks.Networks;
using NeuronNetworks.DataSet;

using ImageGenerator;

namespace TestConsoleApplication
{
    public static class Program : Object
    {
        private static void Main(String[] args)
        {
            //var result = GenerateTrainSetAndTestSet();


            //var trainSet = result.Item1;
            //var testSet = result.Item2;

            //var inputs = new Single[4][]
            //{
            //    new Single[] { 0, 0 },
            //    new Single[] { 0, 1 },
            //    new Single[] { 1, 0 },
            //    new Single[] { 1, 1 },
            //};

            //var outputs = new Single[4][]
            //{
            //    new Single[] { 0 },
            //    new Single[] { 0 },
            //    new Single[] { 0 },
            //    new Single[] { 1 },
            //};

            //var trainSet = new DataSet()
            //{
            //    InputVectors = inputs,
            //    OutputVectors = outputs,
            //};

            //Console.WriteLine(trainSet.InputVectors.Length);
            //Console.WriteLine(trainSet.OutputVectors.Length);

            //for (var i = 0; i < trainSet.InputVectors.Length; ++i)
            //{
            //    for (var j = 0; j < trainSet.InputVectors[i].Length; ++j)
            //    {
            //        Console.Write(trainSet.InputVectors[i][j] + ", ");
            //    }

            //    Console.WriteLine(trainSet.OutputVectors[i][0]);
            //}

            var generator = new Generator();

            var trainSet = GetTrainSet();

            var topology = new RumelhartPerseptronTopology(ActivationFunction, 25, 1, new Int32[] { 10, 4, 4 });

            var perseptron = new RumelhartPerseptron(topology)
            {
                TrainSet = trainSet,
                Rate = 0.9f,
                Impuls = 0.9f,
                Epochs = 10000,
                PrematureExitError = 0.09f,
            };

            perseptron.Train();

            var image = Image.FromFile("TestMinus.bmp");

            var bitmap = new Bitmap(image);

            var input = generator.GetPixelVector(bitmap);

            var result = perseptron.Compute(input)[0];

            Console.WriteLine("Ideal: {0}; Actual: {1}", 0, result);

            //for (var i = 0; i < testSet.InputVectors.Length; ++i)
            //{
            //    var actual = perseptron.Compute(testSet.InputVectors[i]);

            //    var epselons = perseptron.GetEpselons(testSet.OutputVectors[i], actual);

            //    var averageEps = default(Single);

            //    foreach (Single eps in epselons)
            //    {
            //        averageEps += eps;
            //    }

            //    averageEps /= epselons.Length;

            //    Console.WriteLine("Test {0} - {1}% error", i + 1, averageEps * 100);
            //}

            //for (var i = 1; i <= 10; ++i)
            //{
            //    for (var j = 1; j <= 10; ++j)
            //    {
            //        var rate = i / 10.0f;

            //        var impuls = j / 10.0f;



            //        perseptron.Train();

            //        Console.WriteLine("Rate: {0}; Imputls: {1}; Error: {2}", rate, impuls, perseptron.Error);
            //    }
            //}

            //var result = perseptron.Compute(new Single[] { 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, });

            //Console.WriteLine("Ideal: 0; Actual: {0}", result[0]);
        }

        private static IDataSet GetTrainSet()
        {
            var generator = new Generator();

            var trainInput = new List<Single[]>();
            var trainOutput = new List<Single[]>();

            var bitmaps = generator.GetBitmaps("Info/Plus");

            foreach (Bitmap bitmap in bitmaps)
            {
                trainInput.Add(generator.GetPixelVector(bitmap));
                trainOutput.Add(new Single[] { 1 });
            }

            bitmaps = generator.GetBitmaps("Info/Minus");

            foreach (Bitmap bitmap in bitmaps)
            {
                trainInput.Add(generator.GetPixelVector(bitmap));
                trainOutput.Add(new Single[] { 0 });
            }

            return new DataSet() { InputVectors = trainInput.ToArray(), OutputVectors = trainOutput.ToArray() };
        }

        private static void GenerateSet()
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var generator = new Generator();

            var bitmaps = generator.GetBitmaps("Letters");

            for (var i = 0; i < bitmaps.Length; i += 2)
            {
                generator.GenerateCopies($"Copies\\{alphabet[i / 2]}", bitmaps[i], $"{alphabet[i / 2]}");
                generator.GenerateCopies($"Copies\\{alphabet[i / 2]}_small", bitmaps[i + 1], $"{alphabet[i / 2]}_small");
            }
        }

        private static (IDataSet, IDataSet) GenerateTrainSetAndTestSet()
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var generator = new Generator();

            var trainInputs = new List<Single[]>();
            var trainOutputs = new List<Single[]>();

            var testInputs = new List<Single[]>();
            var testOutputs = new List<Single[]>();

            //foreach (Char letter in alphabet)
            //{
            //    //var pathToFolder_1 = $"Copies\\{letter}";
            //    //var pathToFolder_2 = $"Copies\\{letter}_small";

            //    //var bigBitmaps = generator.GetBitmaps(pathToFolder_1);
            //    //var smallBitmaps = generator.GetBitmaps(pathToFolder_2);

            //    //for (var i = 0; i < bigBitmaps.Length; ++i)
            //    //{
            //    //    if (i == 0)
            //    //    {
            //    //        testInputs.Add(generator.GetPixelVector(bigBitmaps[i]));

            //    //        //var outputs = new Single[26];

            //    //        //outputs[alphabet.IndexOf(letter)] = 1;

            //    //        testOutputs.Add(outputs);
            //    //    }
            //    //    else
            //    //    {
            //    //        trainInputs.Add(generator.GetPixelVector(bigBitmaps[i]));

            //    //        //var outputs = new Single[26];

            //    //        //outputs[alphabet.IndexOf(letter)] = 1;

            //    //        trainOutputs.Add(outputs);
            //    //    }
            //    //}

            //    //for (var i = 0; i < smallBitmaps.Length; ++i)
            //    //{
            //    //    if (i == 0)
            //    //    {
            //    //        testInputs.Add(generator.GetPixelVector(smallBitmaps[i]));

            //    //        //var outputs = new Single[26];

            //    //        //outputs[alphabet.IndexOf(letter)] = 1;

            //    //        testOutputs.Add(outputs);
            //    //    }
            //    //    else
            //    //    {
            //    //        trainInputs.Add(generator.GetPixelVector(smallBitmaps[i]));

            //    //        //var outputs = new Single[26];

            //    //        //outputs[alphabet.IndexOf(letter)] = 1;

            //    //        trainOutputs.Add(outputs);
            //    //    }
            //    //}
            //}

            var trainSet = new DataSet()
            {
                InputVectors = trainInputs.ToArray(),
                OutputVectors = trainOutputs.ToArray(),
            };

            var testSet = new DataSet()
            {
                InputVectors = testInputs.ToArray(),
                OutputVectors = testOutputs.ToArray(),
            };

            return (trainSet, testSet);
        }

        private static Single[][] GetInputsFromBitmap(String pathToFolder)
        {
            var generator = new Generator();

            var bitmaps = generator.GetBitmaps(pathToFolder);

            var inputs = new List<Single[]>();

            foreach (Bitmap bitmap in bitmaps)
            {
                inputs.Add(generator.GetPixelVector(bitmap));
            }

            return inputs.ToArray();
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

            var topology = new RumelhartPerseptronTopology(ActivationFunction, 2, 1, new Int32[] { 2 });

            var perseptron = new RumelhartPerseptron(topology)
            {
                TrainSet = trainSet,
                Rate = 0.9f,
                Impuls = 1.0f,
                Epochs = 1000,
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