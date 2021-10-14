using NeuronNetworks.DataSet;

namespace NeuronNetworks.LearningAlgorithms
{
    public interface ILearningAlgorithm
    {
        public void DevideInputData(IDataSet inputData, out IDataSet trainData, out IDataSet testData);

        public void DevideInputData(IDataSet inputData, out IDataSet[] trainData,  IDataSet[] testData);
    }
}