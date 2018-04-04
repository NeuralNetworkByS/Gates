using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.model
{
    class TrainingResultMP : TrainingResultP, ITrainingClass
    {
        
        public float w3 = 0.00f;
        public float w4 = 0.00f;
        public float h1 = 0.00f;
        public float h2 = 0.00f;
        public float wh1 = 0.00f;
        public float wh2 = 0.00f;
        public float biasI2 = 1.00f;
        public float biasO = 1.00f;

        public override void initialize()
        {
            Random random = new Random();
            w1 = (float) random.NextDouble();
            w2 = (float) random.NextDouble();
            w3 = (float) random.NextDouble();
            w4 = (float) random.NextDouble();

            h1 = (float) random.NextDouble();
            h2 = (float) random.NextDouble();
            wh1 = (float) random.NextDouble();
            wh2 = (float) random.NextDouble();

        }
    }
}
