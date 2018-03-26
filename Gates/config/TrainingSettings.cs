using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.config
{
    public class TrainingSetings
    {
        public enum GateType { OR, AND, XOR};
        public enum ActiviationFunction { JUMP, SIGMOID};
        public enum NeuronsNumbers { ONE, TWO};

        public GateType gateType;
        public ActiviationFunction activiationFunction;
        public NeuronsNumbers neuronsNumbers;

        public float learningRate;
    }
}
