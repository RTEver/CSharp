using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetworks.DataSet
{
    public interface IDataSet
    {
        public Single[][] InputVectors { get; set; }

        public Single[][] OutputVectors { get; set; }
    }
}