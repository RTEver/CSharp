using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

using NeuronNetworks.Layers;
using NeuronNetworks.DataSet;
using NeuronNetworks.Neurons;
using NeuronNetworks.Networks;
using NeuronNetworks.Synapses;
using NeuronNetworks.Topologies;

namespace NN_Laboratory_work_4
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var topology = GetTopology();

            var trainSet = GetTrainSet();

            var perseptron = GetPerseptron(topology, trainSet);

            perseptron.Train();

            //var image = GetBitmaps("Info")[0];

            //var input = GetInputFromBitmap(image);

            //for (var i = 0; i < input.Length; i += 3)
            //{
            //    Console.WriteLine("R: {0} G: {1} B: {2}", input[i], input[i + 1], input[i + 2]);
            //}

            // Test
        }

        private static IDataSet GetTrainSet()
        {
            var fiveCentes = GetBitmaps("Info/Squares");
            var oneRubs    = GetBitmaps("Info/Pluses"   );
            var twoRubs    = GetBitmaps("Info/Minuses"   );

            var trainInput  = new List<Single[]>();
            var trainOutput = new List<Single[]>();

            //FillTrainSet(trainInput, trainOutput, fiveCentes, new[] { 1.0f, 1.0f });
            FillTrainSet(trainInput, trainOutput, oneRubs   , new[] { 0.0f });
            FillTrainSet(trainInput, trainOutput, twoRubs   , new[] { 1.0f });

            return new DataSet() { InputVectors = trainInput.ToArray(), OutputVectors = trainOutput.ToArray() };
        }

        private static RumelhartPerseptron GetPerseptron(RumelhartPerseptronTopology topology, IDataSet trainSet)
        {
            return new RumelhartPerseptron(topology)
            {
                TrainSet = trainSet,
                Rate = 0.9f,
                Impuls = 0.9f,
                Epochs = 10000000,
                PrematureExitError = 0.01f,
            };
        }

        private static RumelhartPerseptronTopology GetTopology()
        {
            var inputs  = 48;
            var outputs = 1;

            var hiddenNeurons = new[] { 16, 8, 4 };

            return new RumelhartPerseptronTopology(ActivationFunction, inputs, outputs, hiddenNeurons);
        }

        private static Single ActivationFunction(Single input) => 1.0f / (1 + MathF.Exp(-input));

        private static Bitmap[] GetBitmaps(String pathToFolder)
        {
            var files = Directory.GetFiles(pathToFolder);

            var bitmaps = new Bitmap[files.Length];

            for (var i = 0; i < bitmaps.Length; ++i)
            {
                var image = Image.FromFile(files[i]);

                bitmaps[i] = new Bitmap(image);
            }

            return bitmaps;
        }

        private static void FillTrainSet(List<Single[]> inputs, List<Single[]> outputs, Bitmap[] bitmaps, Single[] output)
        {
            foreach (Bitmap bitmap in bitmaps)
            {
                inputs.Add(GetInputFromBitmap(bitmap));
                outputs.Add(output);
            }
        }

        private static Single[] GetInputFromBitmap(Bitmap bitmap)
        {
            var vector = new List<Single>();

            for (var column = 0; column < bitmap.Width; ++column)
            {
                for (var raw = 0; raw < bitmap.Height; ++raw)
                {
                    var pixel = bitmap.GetPixel(column, raw);

                    vector.Add(pixel.R);
                    vector.Add(pixel.G);
                    vector.Add(pixel.B);
                }
            }

            return vector.ToArray();
        }
    }
}