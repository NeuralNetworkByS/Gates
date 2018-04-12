using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.model
{
    public class XorTrResult
    {
        public Neuron hNeuron1;
        public Neuron hNeuron2;
        public Neuron oNeuron;

        public List<String> raport = new List<String>();

        public XorTrResult(Neuron hNeuron1, Neuron hNeuron2, Neuron oNeuron, List<String> raport)
        {
            this.hNeuron1 = hNeuron1;
            this.hNeuron2 = hNeuron2;
            this.oNeuron = oNeuron;
            this.raport = raport;
        }
    }
}
