using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.model
{
    public class Neuron
    {
        public float[] inputs = new float[2];
        public float[] weights = new float[2];
        public float error;

        public float biasWeight;

        private Random r = new Random();

        public float output()
        {
            return  Sigmoid.output(weights[0] * inputs[0] + weights[1] * inputs[1] + biasWeight);
        }

        public void randomizeWeights()
        {
            weights[0] = (float) r.NextDouble();
            weights[1] = (float) r.NextDouble();
            biasWeight = (float) r.NextDouble();
        }

        public void adjustWeights(float learningRate)
        {
            weights[0] += learningRate *  error * inputs[0];
            weights[1] += learningRate * error * inputs[1];
            biasWeight += learningRate * error;
        }
    }
}
