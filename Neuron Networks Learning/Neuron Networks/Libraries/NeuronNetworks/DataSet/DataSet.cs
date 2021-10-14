using System;

namespace NeuronNetworks.DataSet
{
    public sealed class DataSet : IDataSet
    {
        public Single[][] InputVectors { get; set; }

        public Single[][] OutputVectors { get; set; }

        public DataSet()
            : base()
        { }
    }
}